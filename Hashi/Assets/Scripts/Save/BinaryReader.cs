using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryReader
{
    // 将对象保存至二进制文件
    public static void SaveBySerialization<T>(string path, T obj){

        if(obj == null){
            return;
        }

        Debug.Log(path);
        // 删除已有存档
        if(File.Exists(path)){
            File.Delete(path);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(path, FileMode.CreateNew);
        bf.Serialize(fs, obj);
        fs.Close();
    }

    // 从二进制文件读取文件
    // 若不存在，生成新对象
    public static T LoadByDeserialization<T>(string path) where T: class{
        if(File.Exists(path)){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(path,FileMode.Open);
            var t = bf.Deserialize(fs) as T;
            fs.Close();

            return t;
        }   
        else{
            // currentSave = new SaveData("player0", new List<int>());
            // Debug.Log("Save not loaded. Create a new save file.");
            return default(T);
        }
    }
}
