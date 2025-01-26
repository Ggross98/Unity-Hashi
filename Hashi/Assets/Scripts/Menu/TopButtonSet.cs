using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopButtonSet : MonoBehaviour
{
    public void QuitGame(){
        Application.Quit();
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void ClickMuteButton(MuteButton mb){
        SoundController.Instance.ChangeMute();
        mb.Refresh(SoundController.Instance.isMuted);
    }

}
