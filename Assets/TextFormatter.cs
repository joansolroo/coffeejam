using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFormatter : MonoBehaviour
{

    [SerializeField] public string text;
    Text textfield;

    [SerializeField] public string[] keywords;
    [SerializeField] HashSet<string> keywordDictionary = new HashSet<string>();
    private void Start()
    {
        textfield = GetComponent<Text>();
        foreach (string keyword in keywords) keywordDictionary.Add(keyword);
    }

    public void Changed()
    {
        string formattedText = "";
        string[] lines = text.Split('\n');
        string spaceChars = "=\t ";
        string specialChars = "={}[](),;:*/+-&|<> ";
        string numericChars = "0123456789";
        int l = 0;
        int idx = 0;
        foreach (string line in lines)
        {
            formattedText += "<b><color=#" + ColorUtility.ToHtmlStringRGB(new Color(0.4f, 0.4f, 0.4f)) + ">";
            formattedText += (++l).ToString("000") + ".\t";
            formattedText += "</color></b>";
            {
                List<string> words = new List<string>();
                string word = "";
                bool isComment = false;
                bool isString = false;
                for (int c = 0; c < line.Length; ++c)
                {
                    if (!isComment && line.Length > c + 2 && line[c] == '/' && line[c + 1] == '/')
                    {
                        if (word != "")
                        {
                            words.Add(word);
                            word = "";
                        }
                        isComment = true;
                    }
                    if (isComment)
                    {
                        word += line[c];
                    }
                    else
                    {
                        if (isString || line[c] == '\"')
                        {
                            word += line[c];
                            if (!isString)
                            {
                                isString = true;
                            }
                            else if (line[c] == '\"')
                            {
                                words.Add(word);
                                word = "";

                                isString = false;

                            }
                        }
                        else
                        {
                            if (specialChars.Contains(line[c].ToString()))
                            {
                                words.Add(word);
                                word = "";
                                words.Add(line[c].ToString());
                            }
                            else if (spaceChars.Contains(line[c].ToString()))
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
                }
                if (word != "") words.Add(word);


                foreach (string w in words)
                {
                    //this is debug 
                    /*
                    formattedText += "<b><color=#" + ColorUtility.ToHtmlStringRGB(new Color(idx / 4f, idx / 16f , 1)) + ">";
                    formattedText += w;
                    formattedText += "</color></b>";
                    idx++;
                    formattedText += "<b><color=#" + ColorUtility.ToHtmlStringRGB(new Color(0, 1, 0)) + ">";
                    formattedText += "(" + idx + ")";
                    formattedText += "</color></b>";
                    */

                    if (w.Length > 1 && w[0] == '/' && w[1] == '/')
                    {

                        formattedText += "<b><color=#" + ColorUtility.ToHtmlStringRGB(commentColor) + ">";
                        formattedText += w;
                        formattedText += "</color></b>";
                    }
                    else if (w.Length > 1 && (w[0] == '\'' && w[w.Length - 1] == '\'' ||
                        (w[0] == '\"' && w[w.Length - 1] == '\"')))
                    {
                        formattedText += "<color=#" + ColorUtility.ToHtmlStringRGB(stringColor) + ">";
                        formattedText += w;
                        formattedText += "</color>";
                    }
                    else if (keywordDictionary.Contains(w))
                    {
                        formattedText += "<color=#" + ColorUtility.ToHtmlStringRGB(keywordColor) + ">";
                        formattedText += w;
                        formattedText += "</color>";
                    }
                    else if (specialChars.Contains(w))
                    {
                        formattedText += "<color=#" + ColorUtility.ToHtmlStringRGB(symbolColor) + ">";
                        formattedText += w;
                        formattedText += "</color>";
                    }
                    else if (numericChars.Contains(w[0].ToString()))
                    {
                        formattedText += "<color=#" + ColorUtility.ToHtmlStringRGB(numberColor) + ">";
                        formattedText += w;
                        formattedText += "</color>";
                    }
                    /*else if (TODO DETECT ERROR)
                    {
                        formattedText += "<b><i><color=#" + ColorUtility.ToHtmlStringRGB(errorColor) + ">";
                        formattedText += w;
                        formattedText += "</color></i></b>";
                    }*/
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

    [SerializeField] Color commentColor = new Color(0.1f, 0.8f, 0.1f);
    [SerializeField] Color keywordColor = new Color(0.1f, 0.3f, 0.7f);
    [SerializeField] Color symbolColor = new Color(0.5f, 0.5f, 0.7f);
    [SerializeField] Color stringColor = new Color(0.7f, 0.2f, 0.3f);
    [SerializeField] Color numberColor = new Color(0.1f, 0.7f, 0.8f);
    [SerializeField] Color errorColor = new Color(1f, 0, 0);
    void Update()
    {
       // Changed();
    }
}
