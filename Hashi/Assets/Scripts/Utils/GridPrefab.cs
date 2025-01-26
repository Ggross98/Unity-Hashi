using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridPrefab : MonoBehaviour
{
    public int x, y;

    public void SetPosition(int _x, int _y){
        this.x = _x;
        this.y = _y;
    }
}
