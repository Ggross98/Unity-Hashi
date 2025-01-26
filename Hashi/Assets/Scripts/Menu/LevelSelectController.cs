using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectController : MonoBehaviour
{
    [SerializeField] private Image cover;
    [SerializeField] private LevelCellController cellController;
    [SerializeField] private Text levelText;
    private SaveController save;
    private int currentLevel = 0;

    private void Awake() {
        save = SaveController.Instance;

        cellController.CreateLevelCells();
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowLevel(currentLevel);
    }


    private void SetCover(Sprite s){
        cover.sprite = s;
    }

    private void ShowLevel(int level){
        var cover = save.GetCover(level);
        SetCover(cover);

        int levelCount = Settings.LEVEL_COUNT[level];
        int completed = save.GetCompletedCount(level);
        cellController.SetLevelCells(levelCount, completed);

        levelText.text = "Level "+level;
    }

    public void NextLevel(){
        currentLevel ++;
        if(currentLevel >= Settings.LEVEL_COUNT.Length) currentLevel = 0;

        ShowLevel(currentLevel);
    }

    public void LastLevel(){
        currentLevel --;
        if(currentLevel < 0) currentLevel = Settings.LEVEL_COUNT.Length - 1;

        ShowLevel(currentLevel);
    }

    public void ClickCell(LevelCell cell)
    {
        StartGame(cell.Index());
    }

    public void StartGame(int index)
    {
        // string levelName = currentLevel + "-" + index;
        // Debug.Log( "level " + levelName + " start");
        // accountController.currentLevel = index;
        // Debug.Log("Map "+index);
        MapDataController.Instance.SetMap(currentLevel, index);
        SceneManager.LoadScene("Game");
    }

}
