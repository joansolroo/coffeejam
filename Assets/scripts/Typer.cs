using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(CharacterProfile))]
[RequireComponent(typeof(Project))]
public class Typer : MonoBehaviour
{
    [SerializeField] TextFormatter textfield;

    int currentCharacter = 0;

    public static char[] ErrorChars = new char[] { 'б', 'в', 'г', 'д', 'ж', 'з', 'к', 'л', 'м', 'н', 'п', 'ф', 'ц', 'ч', 'ш', 'щ', 'й', 'э', 'ы', 'я', 'ё', 'ю', 'и' };
    public static HashSet<char> ErrorCharsSet = new HashSet<char>();

    Project project;
    CharacterProfile profile;

    public int lineCount
    {
        get
        {
            return project.currentFileLineCount;
        }
    }

    // Use this for initialization
    void Start()
    {
        project = GetComponent<Project>();
        profile = GetComponent<CharacterProfile>();

        textfield.text = "";

        ErrorCharsSet.Clear();
        foreach (char c in ErrorChars) ErrorCharsSet.Add(c);
    }

    [SerializeField] string input;
    List<float> cpsWindow = new List<float>();
    [SerializeField] public float CPM;
    [SerializeField] float WindowSize = 30;

    // Update is called once per frame
    void Update()
    {
        input = Input.inputString;
        int charsPerFrame = 0;

        if (Input.GetKey(KeyCode.Backspace))
        {
            textfield.text = textfield.text.Remove(textfield.text.Length - profile.charPerKey);
            textfield.Changed();
            currentCharacter -= profile.charPerKey;
        }
        else if (Input.anyKeyDown)
        {
            if (currentCharacter < project.currentFileText.Length)
            {
                
                foreach (char i in input)
                {
                    if (Random.value < profile.failRatio)
                    {
                        Debug.Log("fail");
                        for (int c = 0; c < profile.charPerKey; ++c)
                        {
                            if (project.currentFileText[currentCharacter] == ' ' || project.currentFileText[currentCharacter] == '\n' || project.currentFileText[currentCharacter] == '\t')
                            {
                                textfield.text += project.currentFileText[currentCharacter++];
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
                        if (Random.value < profile.critRatio)
                        {
                            Debug.Log("CRIT!");

                            for (int c = 0; c < profile.charPerKey; ++c)
                            {
                                textfield.text += project.currentFileText[currentCharacter++];
                                charsPerFrame++;
                            }
                        }
                        for (int c = 0; c < profile.charPerKey; ++c)
                        {
                            textfield.text += project.currentFileText[currentCharacter++];
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

        CPM = cpm*60 / WindowSize;

        project.SetProgress(((float)textfield.text.Length) / project.currentFileText.Length);
    }
}
