using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AutoScroller : MonoBehaviour {

    RectTransform rectTransform;
    Text text;

    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Typer typer;

    [SerializeField] int lineCount;
    [SerializeField] float fontSize = 0.1f;
    [SerializeField] Vector2 anchorMin;
    [SerializeField] Vector2 anchorMax;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {

        
        lineCount = text.text.Split('\n').Length;
        //anchorMax = rectTransform.anchorMax;
       // if ((lineCount) * fontSize < 1)
        {
            text.alignment = TextAnchor.UpperLeft;
        }
       // else
        {
           // text.alignment = TextAnchor.LowerLeft;
           // rectTransform.anchorMin = new Vector2(0,);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (lineCount+2) * fontSize);
           // rectTransform.anchorMax = new Vector2(1, (lineCount) * fontSize);
            
        }
        scrollbar.value = 0;
    }
}
