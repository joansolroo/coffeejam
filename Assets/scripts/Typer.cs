using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Typer : MonoBehaviour {

    [SerializeField] int charPerKey = 1;
    [SerializeField] float critRatio = 0.05f;
    [SerializeField] float failRatio = 0.05f;

    [SerializeField] TextAsset file;
    [SerializeField] Text textfield;
    int currentCharacter = 0;
    string text;
	// Use this for initialization
	void Start () {
        textfield.text = "";
        text = file.text;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (currentCharacter < text.Length)
            {
                if(Random.value < failRatio)
                {
                    Debug.Log("fail");
                }
                else
                {
                    if (Random.value < critRatio)
                    {
                        Debug.Log("CRIT!");

                        for (int c = 0; c < charPerKey; ++c)
                        {
                            textfield.text += text[currentCharacter++];
                        }
                    }
                    for (int c = 0; c < charPerKey; ++c)
                    {
                        textfield.text += text[currentCharacter++];
                    }

                }
                
            }
            else
            {
                Debug.Log("EOF!");
            }
        };
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
