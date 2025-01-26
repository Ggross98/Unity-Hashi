using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public const int LEFT = 0, RIGHT = 1, UP = 2, DOWN = 3;
    public const int LEFT_CLICK = 0, RIGHT_CLICK = 1;

    public static Vector2 GetBlockPosition(Block block){
        int x = block.x;
        int y = block.y;
        return new Vector2(Settings.BLOCK_CELL_SIZE * (x+0.5f), Settings.BLOCK_CELL_SIZE * (y+0.5f));
    }

    public static Vector2Int GetGridPosition(Vector2 pos){
        int x = (int)(pos.x / (float)Settings.BLOCK_CELL_SIZE);
        int y = (int)(pos.y / (float)Settings.BLOCK_CELL_SIZE);
        return new Vector2Int(x,y);
    }

    public static Vector2 GetBridgePosition(Block a, Block b){
        var pa = GetBlockPosition(a);
        var pb = GetBlockPosition(b);
        return new Vector2((pa.x + pb.x)/2f, (pa.y + pb.y)/2f);
    }

    public static Vector2 GetBridgeLineSize(Block a, Block b){
        int x = 0, y = 0;
        if(a.x == b.x){
            x = Settings.LINE_WIDTH;
            y = Mathf.Abs(a.y - b.y) * Settings.BLOCK_CELL_SIZE;
        }else{
            y = Settings.LINE_WIDTH;
            x = Mathf.Abs(a.x - b.x) * Settings.BLOCK_CELL_SIZE;
        }
        return new Vector2(x,y);
    }

    public static Vector2 GetBridgeLineParentSize(Block a, Block b){
        int x = 0, y = 0;
        if(a.x == b.x){
            x = Settings.LINE_WIDTH*2 + Settings.LINE_GAP;
            y = Mathf.Abs(a.y - b.y) * Settings.BLOCK_CELL_SIZE;
        }else{
            y = Settings.LINE_WIDTH*2 + Settings.LINE_GAP;
            x = Mathf.Abs(a.x - b.x) * Settings.BLOCK_CELL_SIZE;
        }
        return new Vector2(x,y);
    }

    public static Vector2 GetGamePanelSize(int width, int height){
        return new Vector2(width * Settings.BLOCK_CELL_SIZE, height * Settings.BLOCK_CELL_SIZE);
    }

    public static int[,] ListToArray(List<int[]> t){
        int m = t.Count;
        int n = t[0].Length;

        int[,] ans = new int[m,n];

        for(int i = 0; i<m; i++){
            for(int j = 0; j<n; j++){
                ans[i,j] = t[i][j];
            }
        }

        return ans;
    }

    public static int[,] CrossTo2DArray(int[][] t){
        int m = t.GetLength(0);
        int n = t.GetLength(1);
        int[,] ans = new int[m,n];

        for(int i = 0; i<m; i++){
            for(int j = 0; j<n; j++){
                ans[i,j] = t[i][j];
            }
        }

        return ans;
    }
}
