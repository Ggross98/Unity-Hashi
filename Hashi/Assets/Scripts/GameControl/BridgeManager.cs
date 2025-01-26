using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : ObjectGenerator<Bridge>
{

    public void TryCreateBridge(Block a, Block b){
        if(!Bridge.CanInit(a,b)) return;

        Bridge target = null;
        foreach(Bridge bridge in ts){
            if(bridge.SamePosition(a,b)){
                target = bridge;
                break;
            }
        }
        if(target == null){
            if(!Cross(a,b)){
                target = Add();
                target.Init(a,b);
                target.AddLine();
            }
        }else{
            target.AddLine();
        }
    }

    // 以a、b为两端的桥是否与已存在的桥交叉
    public bool Cross(Block a, Block b){
        Block[] blocks;
        if(a.x == b.x){
            blocks = a.y > b.y ? new Block[]{b, a} : new Block[]{a, b};
        }else{
            blocks = a.x > b.x ? new Block[]{b, a} : new Block[]{a, b};
        }
        a = blocks[0];
        b = blocks[1];
        int type = Bridge.Type(a,b);

        foreach(var bridge in ts){
            var bridgeBlocks = bridge.GetBlocks();
            var c = bridgeBlocks[0];
            var d = bridgeBlocks[1];
            var bridgeType = Bridge.Type(c,d);

            if( type == Bridge.HORIZONTAL && bridgeType == Bridge.HORIZONTAL){
                // Debug.Log(0);
                if(a.y == c.y ){
                    if((a.x < c.x && c.x < b.x) || (c.x < a.x && a.x < d.x))
                        return true;
                }
            }
            else if( type == Bridge.HORIZONTAL && bridgeType == Bridge.VERTICAL){
                // Debug.Log(1);
                if(a.x < c.x && c.x < b.x && c.y < a.y && a.y < d.y){
                    return true;
                }
            }
            else if( type == Bridge.VERTICAL && bridgeType == Bridge.HORIZONTAL){
                // Debug.Log(2);
                if(c.x < a.x && a.x < d.x && a.y < c.y && c.y < b.y){
                    return true;
                }
            }
            else if( type == Bridge.VERTICAL && bridgeType == Bridge.VERTICAL){
                // Debug.Log(3);
                if(a.x == c.x ){
                    if((a.y < c.y && c.y < b.y) || (c.y < a.y && a.y < d.y))
                        return true;
                }
            }


        }
        return false;
    }

    public void ClickBridge(Bridge bridge, int click){
        switch(click){
            case Utils.LEFT_CLICK:
                bridge.AddLine();
                break;
            case Utils.RIGHT_CLICK:
                bridge.RemoveLine();
                if(bridge.IsEmpty()){
                    Destroy(bridge);
                }
                break;
        }
    }
}
