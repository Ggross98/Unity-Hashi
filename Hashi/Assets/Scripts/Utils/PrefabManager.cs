using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 预制体管理类，控制预制体的生成、初始化、调用和销毁
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PrefabManager<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 管理的预制体对象
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// 预制体生成的父节点
    /// </summary>
    public Transform parent;

    protected List<GameObject> objectList;
    protected List<T> tList;
    protected int count;

    protected void Awake() {
        if(prefab == null){
            Debug.LogError("Prefab not found!");
            return;
        }

        if(parent == null){
            Debug.LogError("Prefab parent not found!");
            return;
        }

        if(prefab.GetComponent<T>() == null){
            Debug.LogError("The prefab does not contain T unit!");
            return;
        }

        objectList = new List<GameObject>();
        tList = new List<T>();
        count = 0;
    }

    /// <summary>
    /// 销毁已生成的所有预制体
    /// </summary>
    public void Clear(){
        for(int i = count - 1; i >= 0; i--){
            var obj = objectList[0];
            Destroy(obj);
            objectList.RemoveAt(0);
        }
        objectList = new List<GameObject>();
        tList = new List<T>();
        count = 0;
    }

    /// <summary>
    /// 生成指定数量的预制体。生成前会销毁已生成的对象
    /// </summary>
    /// <param name="c">生成数量</param>
    public void Generate(int c){
        Clear();
        if(c<0)
            c=0;
        for(int i = 0; i < c; i++){
            Add();
        }
    }

    /// <summary>
    /// 生成一个预制体
    /// </summary>
    /// <param name="active">预制体对象是否激活</param>
    /// <returns>预制体脚本</returns>
    public T Add(bool active = true){
        var obj = Instantiate(prefab, parent);
        obj.SetActive(active);
        objectList.Add(obj);
        T t = obj.GetComponent<T>();
        tList.Add(t);
        count++;

        return t;
    }

    /// <summary>
    /// 生成一个预制体，不使用保存系统
    /// </summary>
    /// <param name="active"></param>
    /// <returns></returns>
    public T AddNoSave(bool active = true){
        var obj = Instantiate(prefab, parent);
        obj.SetActive(active);
        T t = obj.GetComponent<T>();
        return t;
    }

    // public T PopLeft(){
    //     if(tList.Count < 1){
    //         return null;
    //     }else{
    //         T t = tList[0];
    //         tList.RemoveAt(0);
    //         objectList.RemoveAt(0);
    //         count--;
    //         return t;
    //     }
    // }

    // public void DestroyLeft(){
    //     if(tList.Count < 1){
    //         return;
    //     }else{
    //         GameObject obj = objectList[0];
    //         tList.RemoveAt(0);
    //         objectList.RemoveAt(0);
    //         Destroy(obj);
    //         count--;
    //     }
    // }

    /// <summary>
    /// 删除指定预制体
    /// </summary>
    /// <param name="t"></param>
    public void Delete(T t){
        if(t == default(T)) return;
        if(tList.Contains(t)){
            var obj = t.gameObject;
            tList.Remove(t);
            objectList.Remove(obj);
            Destroy(obj);
            count--;
        }
    }

    public bool IsEmpty(){
        return count == 0;
    }

    public T GetAtIndex(int index){
        if(index < 0 || index >= count) return default(T);
        else return tList[index];
    }

    public GameObject GetGameObjectAtIndex(int index){
        if(index < 0 || index >= count) return null;
        else return objectList[index];
    }



}
