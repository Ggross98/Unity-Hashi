using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapJsonObject
{
    public string name;
    public int width, height;
    public List<NodeJsonObject> data;

    public MapJsonObject(string name, int width, int height, List<NodeJsonObject> data){
        this.name = name;
        this.width = width;
        this.height = height;
        this.data = data;
    }
}

[System.Serializable]
public class NodeJsonObject
{
    public int x, y;
    public int count;

    public NodeJsonObject(int x, int y, int count){
        this.x = x;
        this.y = y;
        this.count = count;
    }

}
