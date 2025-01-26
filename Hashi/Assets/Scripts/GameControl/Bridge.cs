using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Gui;

public class Bridge : ObjectGenerator<Image>
{
    [SerializeField] private RectTransform rect;
    // [SerializeField] private LeanButton button;

    // 是水平或垂直
    public const int HORIZONTAL = 0, VERTICAL = 1;
    private int type;

    // 表示桥的线段（长方形）的大小
    private Vector2 lineSize;

    private Block[] blocks;
    
    private void Start() {
        // rect = GetComponent<RectTransform>();
        // button = GetComponent<LeanButton>();
    }

    // 判断该桥对象的两端是否与输入相等
    public bool SamePosition(Block a, Block b){
        if(blocks == null) return false;
        return (blocks[0] == a || blocks[1] ==a) && (blocks[0] == b || blocks[1] ==b);
    }

    // 添加一条线
    public void AddLine(){
        if(count >= Settings.MAX_BRIDGE_LINES) return;
        var line = Add();
        line.rectTransform.sizeDelta = lineSize;

        foreach(Block b in blocks){
            b.ChangeCount(1);
        }
    }

    // 移除一条线
    public void RemoveLine(){
        if(count == 0) return;
        DestroyLeft();

        foreach(Block b in blocks){
            b.ChangeCount(-1);
        }
    }

    public Block[] GetBlocks(){
        return blocks;
    }

    public static bool CanInit(Block a, Block b){
        return (a.x == b.x && a.y != b.y) || (a.y == b.y && a.x != b.x);
    }

    public static int Type(Block a, Block b){
        return a.x == b.x ? VERTICAL : HORIZONTAL;
    }

    // 初始化桥的位置
    public void Init(Block a, Block b){
        
        // ab连线必须水平或垂直
        // if(a.x != b.x && a.y != b.y){
        //     Debug.LogError("Cannot place a bridge: blocks on false place");
        // }
        if(!CanInit(a,b))
            return;

        // 确定方向
        type = Type(a,b);
        if(type == VERTICAL){
            var hlg = parent.gameObject.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = Settings.LINE_GAP;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childControlHeight = false;
            hlg.childControlWidth = false;
            hlg.childForceExpandHeight = false;
            hlg.childForceExpandWidth = false;
        }else if(type == HORIZONTAL){
            var vlg = parent.gameObject.AddComponent<VerticalLayoutGroup>();
            vlg.spacing = Settings.LINE_GAP;
            vlg.childAlignment = TextAnchor.MiddleCenter;
            vlg.childControlHeight = false;
            vlg.childControlWidth = false;
            vlg.childForceExpandHeight = false;
            vlg.childForceExpandWidth = false;
        }

        
        if(a.x == b.x){
            blocks = a.y > b.y ? new Block[]{b, a} : new Block[]{a, b};
        }else{
            blocks = a.x > b.x ? new Block[]{b, a} : new Block[]{a, b};
        }
        // blocks = new Block[]{a, b};

        // 确定位置
        // rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Utils.GetBridgePosition(a, b);

        // 确定线段对象的尺寸
        lineSize = Utils.GetBridgeLineSize(a,b);

        // 确定线段父对象的尺寸
        parent.GetComponent<RectTransform>().sizeDelta = Utils.GetBridgeLineParentSize(a,b);

    }
}
