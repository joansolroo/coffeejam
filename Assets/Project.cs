using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(CharacterProfile))]
[RequireComponent(typeof(Typer))]
public class Project : MonoBehaviour {

    [SerializeField] int rangeMin = 3000;
    [SerializeField] int flowMin = 500;
    [SerializeField] int flowMax = 2500;
    [SerializeField] int rangeMax = 3000;
    [SerializeField] TextAsset[] projectFiles;

    [SerializeField] Slider progressFile;
    [SerializeField] Slider progressProject;
    [SerializeField] Button submitButton;

    [SerializeField] TextAsset currentFile;
    public string currentFileText;
    public int currentFileLineCount;

    [SerializeField] Slider CPSSlider;
    Typer typer;
    CharacterProfile profile;
    // Use this for initialization
    void Start () {

        typer = GetComponent<Typer>();
        profile = GetComponent<CharacterProfile>();

        LoadFile(currentFile);
    }
	
    void LoadFile(TextAsset file)
    {
        currentFileText = currentFile.text;
        currentFileLineCount = currentFileText.Split('\n').Length;
    }
	// Update is called once per frame
	void LateUpdate () {
        
        submitButton.interactable = progressFile.value == 1;

        CPSSlider.value = Mathf.Min(1, typer.CPM / rangeMax);
       

        if (typer.CPM < flowMin)
        {
            CPSSlider.fillRect.GetComponent<Image>().color = Color.yellow;
            profile.failRatio = 0.1f;
        }
        else if (typer.CPM > flowMax) {
            CPSSlider.fillRect.GetComponent<Image>().color = Color.red;
            profile.failRatio = 0.25f;
        }
        else
        {
            CPSSlider.fillRect.GetComponent<Image>().color = Color.green;
            profile.failRatio = 0;
        }
    }

    public void SetProgress(float p)
    {
        progressFile.value = p;
    }
}
