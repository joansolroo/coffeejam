using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFormatter : MonoBehaviour {

    [SerializeField] public string text;
    Text textfield;

    [SerializeField] public string[] keywords;
    [SerializeField] HashSet<string> keywordDictionary = new HashSet<string>();
    private void Start()
    {
        textfield = GetComponent<Text>();
        foreach (string keyword in keywords) keywordDictionary.Add(keyword);
    }

    void Changed()
    {

    }

    void Update()
    {
        string formattedText = "";
        string[] lines = text.Split('\n');
        string specialChars = "={}[](),;:*/+-&|<> ";
        string numericChars = "0123456789";
        foreach (string line in lines)
        {
            {
                List<string> words = new List<string>();
                string word = "";
                bool comment = false;
                for (int c = 0; c < line.Length; ++c)
                {
                    if (line[c] == '/' && line[c + 1] == '/')
                    {
                        if (word != "")
                        {
                            words.Add(word);
                            word = "";
                        }
                        comment = true;
                    }
                    if (comment)
                    {
                        word += line[c];
                    }
                    else
                    {
                        if (specialChars.Contains(line[c].ToString()))
                        {
                            words.Add(word);
                            word = "";
                            words.Add(line[c].ToString());
                        }
                        else
                        {
                            word += line[c];
                        }
                    }
                }
                if (word != "") words.Add(word);

                foreach (string w in words)
                {
                    if (w.Length>1 && w[0] == '/' && w[1] == '/')
                    {

                        formattedText += "<b><color=#" + ColorUtility.ToHtmlStringRGB(new Color(0.1f, 1f, 0.1f)) + ">";
                        formattedText += w;
                        formattedText += "</color></b>";
                    }
                    else if (w.Length > 1 && (w[0] == '\'' && w[w.Length - 1] == '\'' ||
                        w[0] == '\"' && w[w.Length - 1] == '\"'))
                    {
                        formattedText += "<color=#" + ColorUtility.ToHtmlStringRGB(new Color(0.7f, 0.25f, 0)) + ">";
                        formattedText += w;
                        formattedText += "</color>";
                    }
                    else if (keywordDictionary.Contains(w))
                    {
                        formattedText += "<color=#" + ColorUtility.ToHtmlStringRGB(new Color(0.25f, 0.25f, 1)) + ">";
                        formattedText += w;
                        formattedText += "</color>";
                    }
                    else if (specialChars.Contains(w))
                    {
                        formattedText += "<color=#" + ColorUtility.ToHtmlStringRGB(new Color(0.4f, 0.5f, 0.6f)) + ">";
                        formattedText += w;
                        formattedText += "</color>";
                    }
                    else if (numericChars.Contains(w[0].ToString()))
                    {
                        formattedText += "<color=#" + ColorUtility.ToHtmlStringRGB(new Color(0, 1, 1)) + ">";
                        formattedText += w;
                        formattedText += "</color>";
                    }
                    else
                    {
                        formattedText += w;
                    }
                    //formattedText += ' ';

                }
            }
            formattedText += '\n';
        }
        textfield.text = formattedText;
    }
}
