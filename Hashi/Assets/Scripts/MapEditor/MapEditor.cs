using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    [SerializeField] private RectTransform editorPanelRect;
    [SerializeField] private Text statusText;

    [SerializeField] private InputField nameInput;
    [SerializeField] private Slider widthInput, heightInput;
    [SerializeField] private Toggle blockToggle, bridgeToggle;

    // Never used
    private string path = "Maps/";

    private int width, height;
    private Vector2 panelSize;
    private string mapName;
    private int[,] mapData;

    public const int OVER = 0, EDITING_BLOCK = 1, EDITING_BRIDGE = 2;
    private int status = OVER;
    private bool editing = false;

    public BlockManager blockManager;
    public BridgeManager bridgeManager;

    private MapData map;

    void Start()
    {
        // InitEditor();

        // Save();
    }

    public void InitEditor(){
        blockManager.Clear();
        blockManager.Init(width, height, new Vector2(Settings.BLOCK_SIZE, Settings.BLOCK_SIZE), new Vector2(Settings.BLOCK_GAP, Settings.BLOCK_GAP));
        
        bridgeManager.Clear();

        // 读取文件
        // var mapData = MapDataController.Instance.currentMapData;
        // if(mapData == null){
        //     mapData = MapData.DEFAULT_MAP;
        // }

        panelSize = Utils.GetGamePanelSize(width, height);

        editorPanelRect.sizeDelta = panelSize;
        editorPanelRect.anchoredPosition = -panelSize / 2;
        editorPanelRect.gameObject.SetActive(true);
        // blockManager.CreateBlocks(mapData);
    }

    public void Create(){
        width = (int)widthInput.value;
        height = (int)heightInput.value;
        mapName = nameInput.text;

        InitEditor();

        // status = EDITING_BLOCK;
    }

    public void Save(){

        Debug.Log("Save process starts!");

        // width = 2;
        // height = 2;
        // name = "test2";
        // mapData = new int[2,2]{
        //     {2,2},
        //     {2,2}
        // };

        // 读取并生成地图数据
        // List<NodeJsonObject> njoList = new List<NodeJsonObject>();
        // for(int i = 0; i<width; i++){
        //     for(int j = 0; j<height; j++){
        //         if(mapData[i,j]!=0){
        //             var njo  = new NodeJsonObject(i,j,mapData[i,j]);
        //             njoList.Add(njo);
        //         }
                
        //     }
        // }

        mapName = nameInput.text;

        var njoList = blockManager.GetNJOs();

        // 将地图数据转为MJO文件
        MapJsonObject mjo = new MapJsonObject(mapName, width, height, njoList);

        // 保存到指定地址
        JsonWriter.WriteDataToJson(mjo, "Maps", mapName, ".map");

        Debug.Log("Saved.");
    }

    private void Update() {

        if(blockToggle.isOn) status = EDITING_BLOCK;
        else if(bridgeToggle.isOn) status = EDITING_BRIDGE;

        if(status == EDITING_BLOCK){
            if(Input.GetMouseButtonDown(0)){

                // 鼠标位置
                var mPos = Input.mousePosition;
                // 转换成editor panel位置
                Vector2 uPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(editorPanelRect, mPos, Camera.main, out uPos);
                // Debug.Log(uPos);

                // 转换成网格坐标
                var gPos = Utils.GetGridPosition(uPos);
                Debug.Log(gPos);

                // 尝试点击该位置
                TryCreateBlock(gPos);

            }
        }
    }

    public void TryCreateBlock(Vector2Int pos){
        int x = pos.x, y = pos.y;
        if(blockManager.InRange(x,y)){
            var b = blockManager.GetAt(x,y);
            if(b == null){
                Debug.Log("Create a block");
                blockManager.CreateBlock(x,y,0);
            }
        }else{
            Debug.Log("Out of range!");
        }
    }   

    public void TryCreateBridge(Block a, int dir){

        if(status != EDITING_BRIDGE) return;
        Debug.Log("Try create a bridge");

        Block b = blockManager.FindBlock(a, dir);
        if(b!=null){
            Debug.Log("Block b found");
            bridgeManager.TryCreateBridge(a,b);
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
}
