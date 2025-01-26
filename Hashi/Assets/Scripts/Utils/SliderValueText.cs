using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Slider slider;

    private void Update() {
        text.text = slider.value + "";
    }
}
