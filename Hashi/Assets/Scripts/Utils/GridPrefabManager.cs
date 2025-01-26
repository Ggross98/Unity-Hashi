using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridPrefabManager<T> : MonoBehaviour where T : GridPrefab
{
    protected T[,] grid;
    protected GameObject[,] objectGrid;
    protected int width, height;
    protected Vector2 size, spacing;

    [SerializeField] protected T prefab;
    [SerializeField] protected RectTransform prefabParent;

    private void Awake() {
        // prefabParent.pivot = new Vector2(0,0);
    }

    public void Init(int w, int h, Vector2 size, Vector2 spacing){
        this.width = w;
        this.height = h;
        this.size = size;
        this.spacing = spacing;

        grid = new T[width,height];
        objectGrid = new GameObject[width, height];
    }

    public void Resize(){
        prefabParent.GetComponent<RectTransform>().sizeDelta = new Vector2(width * (size.x + spacing.x), height * (size.y + spacing.y));
    }

    public T Add(int x, int y, string type = "bottomLeft"){

        if(x<0 || y<0 || x>=width || y>=height) return null;

        var t = Instantiate(prefab, prefabParent);
        var obj = t.gameObject;
        obj.name = "(" + x + ", " + y + ")";

        var rect = obj.GetComponent<RectTransform>();

        switch(type){
            case "bottomLeft":
                // 从左下开始
                rect.localPosition = new Vector3( x*size.x + size.x/2 + spacing.x * x + spacing.x/2, y*size.y + size.y/2 + spacing.y * y + spacing.y/2,0); 
                break;
            case "topLeft":
                // 从左上开始
                rect.localPosition = new Vector3( x*size.x + size.x/2 + spacing.x * x+ spacing.x/2, -(y*size.y + size.y/2 + spacing.y * y+ spacing.y/2),0); 
                break;
        }
        
        grid[x,y] = t;
        objectGrid[x,y] = obj;

        t.SetPosition(x,y);

        return t;
    }

    public void AddAll(string type = "bottomLeft"){
        for(int j = 0; j<height; j++){
            for(int i = 0; i<width; i++){
                Add(i,j, type);
            }
        }
    }

    public T AddNoSave(){
        return Instantiate(prefab, prefabParent);
    }

    public T GetAt(int x, int y){
        if(x<0 || y<0 || x>=width || y>=height) return null;
        return grid[x,y];
    }

    public GameObject GetObjectAt(int x, int y){
        if(x<0 || y<0 || x>=width || y>=height) return null;
        return objectGrid[x,y];
    }

    public void DeleteAt(int x, int y){
        if(IsEmpty(x,y)) return;

        var obj = GetObjectAt(x,y);
        Destroy(obj);

        grid[x,y] = null;
        objectGrid[x,y] = null;
    }

    public bool IsEmpty(int x, int y){
        if(x<0 || y<0 || x >= width || y >= height) return true;
        return grid[x,y] == null;
    }

    public void Clear(){
        for(int i = 0; i<width; i++){
            for(int j  = 0; j<height; j++){
                DeleteAt(i,j);
            }   
        }
    }


}
