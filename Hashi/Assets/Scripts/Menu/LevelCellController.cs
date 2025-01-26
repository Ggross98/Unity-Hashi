using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCellController : PrefabManager<LevelCell>
{
    // [SerializeField] private AudioClip[] bgm;
    [SerializeField] private Color clearColor, unlockedColor, highlightedColor;
    // [SerializeField] private Text modeText;

    // private AccountController accountController;

    // 默认解锁的关卡数量
    private int unlockLevelCount = 1;
    private int maxLevelCount = 5;


    void Start()
    {

        // 读取存档文件

        // accountController = AccountController.Instance;
        // if(SoundController.Instance.GetBGMCount() <= 0){
        //     SoundController.Instance.SetBGM(bgm);
        // }
        
        // 测试读取json格式地图文件

        // Debug.Log("Custom maps: " + MapJsonReader.Instance.DIYMapCount());
        // var customMapData = MapJsonReader.Instance.GetDIYMapAtIndex(0);
        // Debug.Log(customMapData.atomList.Count);

        // CreateLevelCells();
        // SetLevelCells(3,1);
        // ShowMode();
    }

    public void CreateLevelCells()
    {
        // 关卡总数
        int count = maxLevelCount;
        // 提前解锁关卡数量
        int unlock = unlockLevelCount;

        // 从存档获得已完成关卡
        // List<int> clearList = SaveController.Instance.GetCurrentSave().clearLevels;
        List<int> clearList = new List<int>(){};

        for (int i = 0; i < count; i++)
        {
            var cell = Add();
            cell.SetText((i + 1) + "");
            // cell.SetController(this);
        }

        // // diy关卡
        // levelCells[1] = new List<LevelCell>();
        // var customCount = MapJsonReader.Instance.MapCount("Custom");
        // for(int i = 0; i< customCount; i++){
        //     var obj = Instantiate(cellPrefab, cellParent[1]);
        //     var cell = obj.GetComponent<LevelCell>();
        //     cell.SetText((i + 1) + "");
        //     cell.SetController(this);

        //     cell.SetNormalColor(clearColor);
        // }

    }

    public void SetLevelCells(int levels, int completed){

        int unlock = unlockLevelCount;
        for(int i = 0; i<levels; i++){
            var cell = GetAtIndex(i);

            // 设置按钮颜色
            cell.SetHighlightedColor(highlightedColor);
            cell.SetInteractable(true);

            if (i < completed) {
                cell.SetNormalColor(clearColor);
            }
            else if (unlock > 0)
            {
                cell.SetNormalColor(unlockedColor);
                unlock--;
            }
            else
            {   
                cell.SetInteractable(false);
            }


            var obj = GetGameObjectAtIndex(i);
            obj.SetActive(true);
        }

        for(int i = levels; i<maxLevelCount; i++){
            var obj = GetGameObjectAtIndex(i);
            obj.SetActive(false);
        }
    }

    // public void Exit()
    // {
    //     Application.Quit();
    // }

    // public void ShowMode(){
        // var mode = accountController.playMode;
        // if (mode == 1)
        // {
        //     modeText.text = "Custom";
        //     cellParent[0].gameObject.SetActive(false);
        //     cellParent[1].gameObject.SetActive(true);
        // }
        // else
        // {
        //     modeText.text = "Campaign";
        //     cellParent[0].gameObject.SetActive(true);
        //     cellParent[1].gameObject.SetActive(false);
        // }
    // }

    // public void ChangeMode()
    // {
        // var mode = accountController.playMode;

        // if (mode == 0)
        // {
        //     modeText.text = "Custom";
        //     cellParent[0].gameObject.SetActive(false);
        //     cellParent[1].gameObject.SetActive(true);
        // }
        // else
        // {
        //     modeText.text = "Campaign";
        //     cellParent[0].gameObject.SetActive(true);
        //     cellParent[1].gameObject.SetActive(false);
        // }

        // accountController.playMode = 1 - accountController.playMode;
        // ShowMode();
    // }


}
