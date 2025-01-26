using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Lean.Gui;

public class GameMain : MonoBehaviour
{
    [SerializeField] private RectTransform gamePanelRect;
    [SerializeField] private Text statusText;
    [SerializeField] private GameObject cgObject, levelButtons;
    [SerializeField] private CGController cgController;
    [SerializeField] private LeanButton nextButton;

    public const int PLAYING = 1, OVER = 0;
    private int status = OVER;

    public BlockManager blockManager;
    public BridgeManager bridgeManager;

    void Start()
    {
        // blockManager.CreateBlocks(MapData.TEST_MAP);
        Restart();
    }

    public void Restart(){
        blockManager.Clear();
        bridgeManager.Clear();
        // blockManager.CreateBlocks(MapData.TEST_MAP);
        // blockManager.CreateBlocks(MapDataController.Instance.GetMapAtIndex("Test", 0));

        var mapData = MapDataController.Instance.GetMapData();
        // Debug.Log("width" + mapData.width);
        gamePanelRect.sizeDelta = Utils.GetGamePanelSize(mapData.Width(), mapData.Height());
        blockManager.InitMap(mapData);
        blockManager.CreateAllBlocks(mapData);


        statusText.text = mapData.name;
        status = PLAYING;

        cgObject.SetActive(false);
        levelButtons.SetActive(false);
        // cgController.SetCGList();
    }

    public void NextLevel(){
        MapDataController.Instance.SetNextMap();
        Restart();
    }

    public void Win(){

        statusText.text = "Level Clear!";

        
        status = OVER;

        // 显示CG
        // int currentLevel = MapDataController.Instance.CurrentLevel();
        // int currentIndex = MapDataController.Instance.CurrentIndex();
        // cgObject.SetActive(true);
        // var cg = SaveController.Instance.GetCG(currentLevel, currentIndex-1);
        // cgController.SetCG(cg);

        levelButtons.SetActive(true);
        nextButton.interactable = MapDataController.Instance.HasNext();

        // 存储过关信息
        Debug.Log("Level clear");
        SaveController.Instance.CompleteLevel(MapDataController.Instance.CurrentLevel(), MapDataController.Instance.CurrentIndex());
        SaveController.Instance.Save();
    }

    // public void Quit(){
    //     Application.Quit();
    // }

    public void ClickBlock(Block b){
        // if(status != PLAYING) return;

        blockManager.MarkBlock(b);
    }

    public void TryCreateBridge(Block a, int dir){

        // if(status != PLAYING) return;

        Block b = blockManager.FindBlock(a, dir);
        if(b!=null){
            bridgeManager.TryCreateBridge(a,b);

            // Debug.Log("Check win");
            if(blockManager.CheckWin()){
                Win();
            }
        }
    }

    public void ClickBridge(Bridge bridge, int type){

        bridgeManager.ClickBridge(bridge, type);

        if(blockManager.CheckWin()){
            Win();
        }
    }

    public void TryCreateBridgeToRight(Block b){
        TryCreateBridge(b, Utils.RIGHT);
    }

    public void TryCreateBridgeToLeft(Block b){
        TryCreateBridge(b, Utils.LEFT);
    }

    public void TryCreateBridgeToUp(Block b){
        TryCreateBridge(b, Utils.UP);
    }

    public void TryCreateBridgeToDown(Block b){
        TryCreateBridge(b, Utils.DOWN);
    }


    public void LoadScene(string name){
        SceneManager.LoadScene(name);
    }

    
}
