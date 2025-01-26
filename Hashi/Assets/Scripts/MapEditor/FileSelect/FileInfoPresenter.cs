using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileInfoPresenter : MonoBehaviour
{
    [SerializeField] private InputField fileName, mapName, mapWidth, mapHeight;

    public string FileName(){
        return fileName.text;
    }

    public string MapName(){
        return mapName.text;
    }

    public int MapWidth(){
        return int.Parse(mapWidth.text);
    }

    public int MapHeight(){
        return int.Parse(mapHeight.text);
    }

    // 展示已有地图的信息
    // 输入框将被锁定
    public void ShowMapInfo(string filePath, MapData md){
        gameObject.SetActive(true);
        
        fileName.text = filePath;
        mapName.text = md.name;
        mapWidth.text = md.Width() +"";
        mapHeight.text = md.Height() +"";

        SetInteractable(false);
    }

    public void SetInteractable(bool b){
        fileName.interactable= b;
        mapName.interactable = b;
        mapWidth.interactable = b;
        mapHeight.interactable = b;
    }

    public void ShowNewMapInfo(){
        gameObject.SetActive(true);

        fileName.text = "New Map.map";
        mapName.text = "New Map";
        mapWidth.text = "7";
        mapHeight.text = "7";

        SetInteractable(true);
    }

    public void ShowEmpty(){
        gameObject.SetActive(false);
    }
}
