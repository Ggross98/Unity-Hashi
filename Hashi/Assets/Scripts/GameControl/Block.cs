using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Gui;

public class Block : GridPrefab
{
    // 在网格中的位置
    // public int x, y;

    public const int STATE_NORMAL = 1, STATE_DISABLED = 0, STATE_MARKED = 2;
    // public Color normalColor, markColor;
    public int state = STATE_DISABLED;

    // public Color normalColor, markedColor;
    public ColorBlock normalColors, markedColors; 

    [SerializeField] private LeanButton button;
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rect;
    [SerializeField] private Text countText;

    public int targetCount, currentCount;

    private void Awake() {



    }

    public void Init(int x, int y, int count = 0){

        // Data
        SetPosition(x,y);

        currentCount = 0;
        SetTargetCount(count);

        // Present
        rect.sizeDelta = new Vector2(Settings.BLOCK_SIZE, Settings.BLOCK_SIZE);
        // rect.anchoredPosition = Utils.GetBlockPosition(this);
        countText.fontSize = Settings.BLOCK_SIZE / 2;
    }

    public void SetState(int i){
        state = i;
        switch(state){
            case STATE_DISABLED:
                button.interactable = false;
                break;
            case STATE_NORMAL:
                button.interactable = true;
                button.colors = normalColors;
                break;
            case STATE_MARKED:
                button.interactable = true;
                button.colors = markedColors;
                break;
        }
    }

    public bool IsEnabled(){
        return state == STATE_NORMAL;
    }


    public void ChangeCount(int delta){
        currentCount += delta;
    }

    public bool Complete(){
        return currentCount == targetCount;
    }

    public void SetTargetCount(int i){
        targetCount = i;
        countText.text = targetCount + "";
    }


}
