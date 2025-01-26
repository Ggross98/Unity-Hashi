using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : GridPrefabManager<Block>
{
    // private int[,] mapData;
    // private int mapWidth, mapHeight;

    public static int[,] DIRECTIONS = {
        {-1,0},{1,0},{0,1},{0,-1}
    };

    public void InitMap(MapData map){
        var mapData = map.data;
        var w = map.Width();
        var h = map.Height();
        Init(w, h, new Vector2(Settings.BLOCK_SIZE, Settings.BLOCK_SIZE), new Vector2(Settings.BLOCK_GAP, Settings.BLOCK_GAP));
    }

    public void CreateAllBlocks(MapData map){
        var mapData = map.data;
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                var m = mapData[x,y];
                if(m!=0){
                    CreateBlock(x,y,m);
                    // Block b = Add(x,y, "bottomLeft");
                    // b.Init(x,y,m);
                    // b.SetState(Block.STATE_NORMAL);
                }else{
                    
                }
            }
        }

    }

    public void MarkBlock(Block b){
        // Debug.Log("Clicked");
        if(b.state == Block.STATE_NORMAL){
            b.SetState(Block.STATE_MARKED);
        }else if(b.state == Block.STATE_MARKED){
            b.SetState(Block.STATE_NORMAL);
        }
    }

    public bool InRange(int x, int y){
        return !(x < 0 || y < 0 || x >= width || y >= height);
    }

    public void CreateBlock(int x, int y, int m = 0){

        if(GetAt(x,y) == null){
            Block b = Add(x,y, "bottomLeft");
            b.Init(x,y,m);
            b.SetState(Block.STATE_NORMAL);
        }
    }

    // public void DeleteBlock(int x, int y){
    //     var b = GetBlockAtPosition(x,y);
    //     Destroy(b);
    // }


    public bool CheckWin(){
            
        for(int i = 0; i<width; i++){
            for(int j = 0; j<height; j++){
                var b = GetAt(i,j);
                if(b!= null && !b.Complete()){
                    return false;
                }
            }
        }
        return true;
    }

    // 寻找指定方块某个方向上的最近方块
    public Block FindBlock(Block a, int dir){

        if(GetAt(a.x, a.y) == null)
            return null;

        int dx = DIRECTIONS[dir,0];
        int dy = DIRECTIONS[dir,1];
        int x = a.x + dx;
        int y = a.y + dy;

        while((0<=x && x<width) &&(0<=y && y<height)){

            // Debug.Log(new Vector2Int(x,y));

            var b = GetAt(x,y);
            if(b != null){
                return b;
            }

            x+=dx;
            y+=dy;
        }

        return null;
    }

    public List<NodeJsonObject> GetNJOs(){
        var njoList = new List<NodeJsonObject>();
        for(int i = 0; i<width; i++){
            for(int j = 0; j<height; j++){
                var block = GetAt(i,j);
                if(block != null){
                    if(block.currentCount > 0){
                        var njo = new NodeJsonObject(i,j,block.currentCount);
                        njoList.Add(njo);
                    }
                }
            }
        }
        return njoList;
    }

    // 寻找某个位置的方块
    // public Block GetBlockAtPosition(int x, int y){
    //     foreach(Block b in ts){
    //         if(b.x == x && b.y == y){
    //             return b;
    //         }
    //     }
    //     return null;
    // }
}
