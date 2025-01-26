using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public string name;
    public int[,] data;
    // public int width, height;

    public MapData(int[,] d, string n = "Default Name"){

        name = n;

        // width = d.GetLength(1);
        // height = d.GetLength(0);
        
        data = d;
        // data = new int[width,height];
        // for(int x = 0; x<width; x++){
        //     for(int y = 0; y<height; y++){
        //         data[x,y] = d[height-1-y,x];
        //     }
        // }
    }

    

    public int Width(){
        if(data!=null){
            return data.GetLength(0);
        }else{
            return 0;
        }
    }

    public int Height(){
        if(data!=null){
            return data.GetLength(1);
        }else{
            return 0;
        }
    }

    public static MapData DEFAULT_MAP = new MapData(
        new int[,]{
            {4, 3, 0, 0, 5, 0, 3, 3},
            {2, 0, 0, 0, 0, 0, 0, 3},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 2, 0, 5, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 2, 0, 0, 0},
            {0, 0, 1, 0, 4, 0, 0, 3},
        }
    );

    public static MapData LARGE_MAP = new MapData(
        new int[,]{
            {4, 3, 0, 0, 0, 0, 5, 0, 3, 3},
            {2, 0, 0, 0, 0, 0, 0, 0, 0, 3},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 2, 0, 0, 0, 5, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 0},
            {0, 0, 1, 0, 0, 0, 4, 0, 0, 3},
        }
    );

    public static MapData TEST_MAP_BLANK = new MapData(
        new int[,]{
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
        }
    );

    public static MapData TEST_MAP_2 = new MapData(
        new int[,]{
            {2, 0, 0, 0, 0, 0, 0, 2},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {2, 0, 0, 0, 0, 0, 0, 2},
        }
    );


}
