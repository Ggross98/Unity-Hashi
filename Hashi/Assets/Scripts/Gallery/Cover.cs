using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cover : GridPrefab
{

    [SerializeField] private Image frame, image;
    [SerializeField] private Text countText;

    [HideInInspector] public Button button;


    public int index;


    private void Awake() {
        button = GetComponent<Button>();
    }
    
    public void SetImage(Sprite s){
        image.sprite = s;
        image.color = Color.white;
    }

    public void SetCount(int all, int completed){
        countText.text = completed + "/" + all;
    }

    


}
