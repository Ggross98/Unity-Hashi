using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapEditorFileController : MonoBehaviour
{
    
    [SerializeField] private FileBarGenerator barGenerator;
    [SerializeField] private FileInfoPresenter infoPresenter;
    [SerializeField] private Button createButton, editButton, deleteButton;
    [SerializeField] private InputField pathField;

    private string filePath;
    private Dictionary<string, MapData> mapDataDictionary;

    private void Start() {
        filePath = Application.dataPath + "/Maps/Custom";
        pathField.text = filePath;
        mapDataDictionary = new Dictionary<string, MapData>();

        Debug.Log(filePath);
        GetMapFiles(filePath);
        Generate();

        CancelSelect();
    }   

    // 读取路径中所有.map文件，并转换成MapData对象
    private void GetMapFiles(string path){

        // var mapDataList = new List<MapData>();

        if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);

                FileInfo[] files = directory.GetFiles();

                for(int i = 0; i < files.Length; i++)
                {
                    string fileName = files[i].Name;

                    if (fileName.EndsWith(".map")){
                        var mapPath = path + "/" + fileName;
                        var md = MapDataController.ReadMapDataFromJsonFile(mapPath);
                        // Debug.Log(md.name + ", " + md.width + ", " + md.height);

                        mapDataDictionary.Add(fileName, md);

                        // mapDataList.Add(md);
                    }
                    else
                        // CreateListItem(fileName);
                        continue;

                }

                // ShowLoadInfo(pathInfo, "Path is loaded.");
                // return 1;
            }
            else
            {
                Debug.Log("No .map file found!");
                // ShowLoadInfo(pathInfo, "Path does not exist!");
                // return 0;
            }

            // return mapDataList;
    }

    // 根据MapData对象生成UI对象
    private void Generate(){
        foreach(var path in mapDataDictionary.Keys){    
            var bar = barGenerator.Add();
            bar.SetPath(path);
        }
    }   

    public void ShowMapInfo(FileBar bar){
        var path = bar.GetPath();
        var md = mapDataDictionary[path];
        infoPresenter.ShowMapInfo(path, md);
        SetButtonInteractive(false, true,true);
    }

    public void CancelSelect(){
        infoPresenter.ShowEmpty();
        SetButtonInteractive(true, false, false);
    }

    // 显示创建文件选项
    public void CreateMapData(){
        infoPresenter.ShowNewMapInfo();
        SetButtonInteractive(false, true, false);
    }

    // 确认新建地图，创建js文件
    public void ConfirmCreateMapData(){
        // infoPresenter.ShowMapInfo();

        // 创建文件

        // 刷新显示
    }

    private void SetButtonInteractive(bool create, bool edit, bool delete){
        createButton.interactable = create;
        editButton.interactable = edit;
        deleteButton.interactable = delete;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(1)){
            CancelSelect();
        }
    }

    public void DoEdit(){
        SceneManager.LoadScene("LevelEditor");
    }


}
