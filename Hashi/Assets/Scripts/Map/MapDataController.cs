using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataController : SingletonMonoBehaviour<MapDataController>
{
    private Dictionary<string, Dictionary<string, MapData>> mapListDict;
    private int currentLevel = 0, currentIndex = 0;
    // private string currentMapName;
    private string currentPlayMode;
    public MapData currentMapData;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        mapListDict = new Dictionary<string, Dictionary<string, MapData>>();

        // LoadMapFromJsonFile("CustomMaps/CustomMaps.json", false, "Custom");
        // LoadMapFromJsonFile("Maps.json", true, "Campaign");
    }

    void Start()
    {
        LoadMapListFromJsonFile("Maps", true, "Test");
    }


    public static MapData ReadMapData(MapJsonObject mjo)
    {
        var w = mjo.width;
        var h = mjo.height;
        var name = mjo.name;
        var _data = mjo.data;

        int[,] data = new int[w,h];
        for(int i = 0; i<w; i++){
            for(int j = 0; j<h; j++){
                data[i,j] = 0;
            }
        }

        foreach(var node in _data){
            data[node.x, node.y] = node.count;
        }
        
        // return MapData.TEST_MAP;
        return new MapData(data, name);
    }

    public static MapData ReadMapDataFromJsonFile(string path, bool asset = false){
        var mjo = JsonReader.GetDataFromJson<MapJsonObject>(path, asset);
        return ReadMapData(mjo);

    }

    private void LoadMapListFromJsonFile(string path, bool asset = true, string listName = "Test")
    {
        var mapList = new Dictionary<string, MapData>();

        var mjoList = JsonReader.GetDataListFromJson<MapJsonObject>(path, asset, "*.map");
        // var mjoList = mljo.maps;

        foreach (var mjo in mjoList)
        {
            // Debug.Log(mjo.name);
            mapList[mjo.name] = ReadMapData(mjo);
            // mapList.Add(ReadMapData(mjo));
            // Debug.Log("Create Successfully");
        }



        mapListDict.Add(listName, mapList);
    }

    public string CurrentMapName(){
        return currentLevel + "-" + currentIndex;
    }

    public void SetMap(int level, int index, string mode = "Test"){
        currentLevel = level;
        currentIndex = index;
        currentPlayMode = mode;
    }

    public void SetNextMap(){
        var maxIndex = Settings.LEVEL_COUNT[currentLevel];
        if(currentIndex < maxIndex) {
            currentIndex ++;
            return;
        }

        var maxLevel = Settings.LEVELS;
        if(currentLevel < maxLevel) {
            currentLevel ++;
            currentIndex = 1;
            return;
        }

        
    }

    public int CurrentLevel(){
        return currentLevel;
    }

    public int CurrentIndex(){
        return currentIndex;
    }

    public bool HasNext(){
        var maxIndex = Settings.LEVEL_COUNT[currentLevel];
        if(currentIndex < maxIndex) return true;

        var maxLevel = Settings.LEVELS;
        if(currentLevel < maxLevel) return true;

        return false;

    }



    public MapData GetMapData(){
        Debug.Log("Read map: " + CurrentMapName());
        return GetMapAtIndex(currentPlayMode, CurrentMapName());
    }

    private MapData GetMapAtIndex(string listName, string name)
    {
        var mapList = mapListDict[listName];
        if (!mapList.ContainsKey(name)) return MapData.DEFAULT_MAP;
        Debug.Log("Map "+name+" leaded");
        return mapList[name];
    }
}



