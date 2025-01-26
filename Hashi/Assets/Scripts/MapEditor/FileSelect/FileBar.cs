using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileBar : MonoBehaviour
{
    [SerializeField] private Text text;
    private string path = "Default Path";

    public void SetPath(string p){
        path = p;
        text.text = p;
    }

    public string GetPath(){
        return path;
    }
    
}
