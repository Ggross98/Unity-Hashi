using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonReader
{

    // 从文件中获得指定类型对象
    public static T GetDataFromJson<T>(string filePath, bool streamingAsset = true)
    {   
        // string prefix = asset ? Application.streamingAssetsPath : System.Environment.CurrentDirectory;
        // string path = prefix + "/" + _path;
        // Debug.Log(path);

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string readdata = reader.ReadToEnd();

                if (readdata.Length > 0)
                {
                    // Debug.Log(readdata);
                    T data = JsonUtility.FromJson<T>(readdata);
                    return data;
                }
            }
        }
        Debug.Log("Not read");
        return default(T);
    }

    // 从路径中获得与正则表达式匹配的对象列表
    public static List<T> GetDataListFromJson<T>(string path, bool asset = true, string extension = "*.map"){

        string prefix = asset ? Application.streamingAssetsPath : System.Environment.CurrentDirectory;
        path = prefix + "/" + path;

        var result = new List<T>();

        DirectoryInfo dirInfo = new DirectoryInfo(path);

        FileInfo[] fileInfoList = dirInfo.GetFiles(extension, SearchOption.AllDirectories);
        foreach(var fileInfo in fileInfoList){
            // Debug.Log(fileInfo.FullName);
            
            T t = GetDataFromJson<T>(fileInfo.FullName);
            if(t != null){
                result.Add(t);
            }
        }

        return result;
    }




}

