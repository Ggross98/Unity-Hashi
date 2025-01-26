using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorBlock : MonoBehaviour
{
    [SerializeField] private Block block;
    [SerializeField] private Text count;

    private void Update() {
        count.text = block.currentCount + "";
    }
}
