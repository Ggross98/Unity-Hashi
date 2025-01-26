using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : SingletonMonoBehaviour<SaveController>
{
    private SaveData currentSave;
    private string saveFileName = "save0.sav";
    private string savePath;

    public List<Sprite> coverSprites;
    public List<List<Sprite>> cgSprites;


    private void Awake() {

        DontDestroyOnLoad(gameObject);

        // 加载图片
        LoadImages();

        // 加载存档
        savePath = Application.streamingAssetsPath + "/Save/" + saveFileName;
        Load();
    }

    private void Start() {
        
    }

    /// <summary>
    /// 加载CG和封面图
    /// </summary>
    private void LoadImages(){
        coverSprites = new List<Sprite>();
        cgSprites = new List<List<Sprite>>();

        for(int i = 0; i < Settings.LEVEL_COUNT.Length; i++){
            var count = Settings.LEVEL_COUNT[i];
            cgSprites.Add(new List<Sprite>());

            for(int j = 0; j <= count; j++){

                // 加载图片
                var spriteName = j == 0 ? "thumbnail" : "cg"; // 临时素材
                // var spriteName = i + "-" + j;
                var sprite = Resources.Load<Sprite>("CG/" + spriteName);
                Debug.Log(sprite.bounds.size);

                if(j == 0) coverSprites.Add(sprite);
                else cgSprites[i].Add(sprite);

            }
        }

        // Debug.Log("CG list:" +cgSprites.Count);
    }

    public Sprite GetCover(int level){
        if(level>=0 && level<coverSprites.Count) return coverSprites[level];
        else return null;
    }

    public Sprite GetCG(int level, int index){
        if(level>=0 && level<cgSprites.Count){
            var sprites = cgSprites[level];
            if(index>=0 && index<sprites.Count) return sprites[index];
        }
        return null;
    }

    public List<Sprite> GetCGList(int level){
        if(level>=0 && level < cgSprites.Count){
            var sprites = cgSprites[level];
            return sprites;
        }
        return null;
    }

    public int GetCompletedCount(int level){
        return currentSave.completedLevels[level];
    }

    public void Load(){
        currentSave = BinaryReader.LoadByDeserialization<SaveData>(savePath);
        if(currentSave == null){
            currentSave = new SaveData("Player 0");
            // Debug.Log("New save data created");
        }else{
            // Debug.Log("Save file loaded.");
        }
        // Debug.Log(currentSave.name);
    }

    public void CompleteLevel(int level, int index){
        Debug.Log("Try save level " + level + "-" + "index");
        Debug.Log("Old record: " + currentSave.completedLevels[level]);
        Debug.Log("New record: " + index);
        if(currentSave.completedLevels[level] < index){
            currentSave.completedLevels[level] = index;
        }
    }

    public void Save(){
        BinaryReader.SaveBySerialization<SaveData>(savePath, currentSave);
    }

    public SaveData GetCurrentSave(){
        return currentSave;
    }


}
