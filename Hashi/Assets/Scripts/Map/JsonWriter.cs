using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonWriter : MonoBehaviour
{
    public static void WriteDataToJson(object obj, string path, string fileName, string suffix = ".json", bool asset = true){

        string filePath = Application.streamingAssetsPath + "/" + path + "/" + fileName + suffix;
        Debug.Log(filePath);

        // 删除已有文件
        if(File.Exists(filePath)){
            File.Delete(filePath);
        }

        // 保存文件
        string json = JsonUtility.ToJson(obj);
        Debug.Log(json);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(json);
        sw.Close();


    }
}
