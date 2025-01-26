using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Text text;
    [SerializeField] private Image icon;

    [SerializeField] private Button button;

    // public int index = -1;

    void Awake()
    {
        // button = GetComponent<Button>();
    }

    public void SetText(string t){
        text.text = t;
    }

    public void SetHighlightedColor(Color c){
        var colorBlock = button.colors;
        colorBlock.highlightedColor = c;
        button.colors = colorBlock;
    }

    public void SetNormalColor(Color c){
        var colorBlock = button.colors;
        colorBlock.normalColor = c;
        button.colors = colorBlock;
    }

    public void SetInteractable(bool b){
        button.interactable = b;
    }

    public int Index(){
        return int.Parse(text.text);
    }
}
