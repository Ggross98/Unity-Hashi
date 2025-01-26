using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;


    public void Refresh(bool isMuted) {
        int index = isMuted ? 0 : 1;
        image.sprite = sprites[index];
    }
}
