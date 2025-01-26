using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectGenerator<T> : MonoBehaviour where T : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;

    protected List<GameObject> objects;
    protected List<T> ts;
    protected int count;

    protected void Awake() {
        if(prefab == null || parent == null){
            Debug.LogError("Prefab or objects' parent not found!");
            return;
        }

        if(prefab.GetComponent<T>() == null){
            Debug.LogError("The prefab does not contain T unit!");
            return;
        }

        objects = new List<GameObject>();
        ts = new List<T>();
        count = 0;
    }

    public void Clear(){
        for(int i = count - 1; i >= 0; i--){
            var obj = objects[0];
            Destroy(obj);
            objects.RemoveAt(0);
        }
        objects = new List<GameObject>();
        ts = new List<T>();
        count = 0;
    }

    public void Generate(int c){
        Clear();
        for(int i = 0; i < c; i++){
            Add();
        }
    }

    public T Add(bool active = true){
        var obj = Instantiate(prefab, parent);
        obj.SetActive(active);
        objects.Add(obj);
        T t = obj.GetComponent<T>();
        ts.Add(t);
        count++;

        return t;
    }

    public T PopLeft(){
        if(ts.Count < 1){
            return null;
        }else{
            T t = ts[0];
            ts.RemoveAt(0);
            objects.RemoveAt(0);
            count--;
            return t;
        }
    }

    public void DestroyLeft(){
        if(ts.Count < 1){
            return;
        }else{
            GameObject obj = objects[0];
            ts.RemoveAt(0);
            objects.RemoveAt(0);
            Destroy(obj);
            count--;
        }
    }

    public void Destroy(T t){
        if(t == default(T)) return;
        if(ts.Contains(t)){
            var obj = t.gameObject;
            ts.Remove(t);
            objects.Remove(obj);
            Destroy(obj);
            count--;
        }
    }

    public bool IsEmpty(){
        return count == 0;
    }

    public T Get(int index){
        if(index < 0 || index >= count) return default(T);
        else return ts[index];
    }

    public GameObject GetGameObject(int index){
        if(index < 0 || index >= count) return null;
        else return objects[index];
    }



}
