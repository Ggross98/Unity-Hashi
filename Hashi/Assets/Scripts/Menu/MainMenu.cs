using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    [SerializeField] private AudioClip[] bgm;

    private void Start() {
        SoundController.Instance.SetBGM(bgm);
    }

    public void SwitchScene(string name){
        SceneManager.LoadScene(name);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
