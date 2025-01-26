using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string name;
    public List<int> completedLevels;

    // public SaveData(string name, List<int> levels){
    //     this.name = name;
    //     this.clearLevels = levels;
    // }

    public SaveData(string name){
        this.name = name;

        this.completedLevels = new List<int>();
        for(int i = 0; i<Settings.LEVEL_COUNT.Length; i++){
            this.completedLevels.Add(0);
        }
        // this.completedLevels[0] = 2;
        // this.completedLevels[8] = 1;
    }

}
