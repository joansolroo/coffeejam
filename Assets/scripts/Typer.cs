using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Typer : MonoBehaviour
{

    [SerializeField] int charPerKey = 1;
    [SerializeField] float critRatio = 0.05f;
    [SerializeField] float failRatio = 0.05f;

    [SerializeField] int fileCount = 1; //replace with list of files
    [SerializeField] TextAsset file;

    [SerializeField] TextFormatter textfield;
    [SerializeField] Slider progressFile;
    [SerializeField] Slider progressProject;
    [SerializeField] Slider CPSSlider;
    [SerializeField] Button submitButton;

    int currentCharacter = 0;
    string text;

    public static char[] ErrorChars = new char[] { 'б', 'в', 'г', 'д', 'ж', 'з', 'к', 'л', 'м', 'н', 'п', 'ф', 'ц', 'ч', 'ш', 'щ', 'й', 'э', 'ы', 'я', 'ё', 'ю', 'и' };
    public static HashSet<char> ErrorCharsSet = new HashSet<char>();

    public int lineCount
    {
        get
        {
            return text.Split('\n').Length;
        }
    }

    // Use this for initialization
    void Start()
    {
        textfield.text = "";
        text = file.text;

        ErrorCharsSet.Clear();
        foreach (char c in ErrorChars) ErrorCharsSet.Add(c);
    }

    [SerializeField] string input;
    List<float> cpsWindow = new List<float>();
    [SerializeField] float CPM;
    [SerializeField] float WindowSize = 30;
    // Update is called once per frame
    void Update()
    {
        input = Input.inputString;
        int charsPerFrame = 0;

        if (Input.GetKey(KeyCode.Backspace))
        {
            textfield.text = textfield.text.Remove(textfield.text.Length - charPerKey);
            textfield.Changed();
            currentCharacter -= charPerKey;
        }
        else if (Input.anyKeyDown)
        {
            if (currentCharacter < text.Length)
            {
                
                foreach (char i in input)
                {
                    if (Random.value < failRatio)
                    {
                        Debug.Log("fail");
                        for (int c = 0; c < charPerKey; ++c)
                        {
                            if (text[currentCharacter] == ' ' || text[currentCharacter] == '\n' || text[currentCharacter] == '\t')
                            {
                                textfield.text += text[currentCharacter++];
                            }
                            else
                            {
                                textfield.text += ErrorChars[Random.Range(0, ErrorChars.Length)];
                                currentCharacter++;
                            }
                            charsPerFrame++;
                        }
                    }
                    else
                    {
                        if (Random.value < critRatio)
                        {
                            Debug.Log("CRIT!");

                            for (int c = 0; c < charPerKey; ++c)
                            {
                                textfield.text += text[currentCharacter++];
                                charsPerFrame++;
                            }
                        }
                        for (int c = 0; c < charPerKey; ++c)
                        {
                            textfield.text += text[currentCharacter++];
                            charsPerFrame++;
                        }

                    }

                }
            }
            else
            {
                Debug.Log("EOF!");
            }

            textfield.Changed();
        };

        progressFile.value = ((float)textfield.text.Length) / text.Length;
        
        submitButton.interactable = progressFile.value == 1;
        /*if(progressFile.value ==1)
        {
            progressProject.value += 1f / fileCount;
        }*/
        cpsWindow.Add(charsPerFrame / Time.deltaTime);
        if (cpsWindow.Count > WindowSize)
        {
            cpsWindow.RemoveAt(0);
        }
        float cpm = 0;
        foreach (float c in cpsWindow)
        {
            cpm += c;
        }
        CPM = cpm*60 / cpsWindow.Count;
        CPSSlider.value = Mathf.Min(1, CPM / 3000);
        failRatio = CPSSlider.value < 0.5f ? 0 : 0.25f;

        CPSSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, CPSSlider.value);
    }
    /*
    public string Read(string path)
    {
        string txt;
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader();
        txt = reader.ReadToEnd();
        Debug.Log(txt);
        reader.Close();
        return txt;
    }*/
}
