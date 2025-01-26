using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverController : GridPrefabManager<Cover>
{   
    // 行列大小
    private const int MAX_COLUMN = 4, MAX_ROW = 3;
    private const int COVER_WIDTH = 390, COVER_HEIGHT = 290;

    private void Awake() {
        Debug.Log("Cover controller starts!");
        CreateCovers();

        // for(int j = 0; j<3; j++){
        //     for(int i = 0; i<4; i++){
        //         int level = j*4 + i;
        //         grid[i,j].SetImage(SaveController.Instance.GetCover(level));
        //         grid[i,j].index = level;
        //     }
        // }
    }

    private void CreateCovers(){
        Init(MAX_COLUMN, MAX_ROW, new Vector2(COVER_WIDTH, COVER_HEIGHT), new Vector2(10, 10));
        Resize();
        AddAll("topLeft");
    }
    
    public void ShowPageCover(int index0, List<Sprite> covers){

        Debug.Log("index0: " + index0);
        Debug.Log("covers: " + covers.Count);

        for(int j = 0; j<3; j++){
            for(int i = 0; i<4; i++){
                int pos = j*4 + i;
                int level = pos + index0;
                grid[i,j].index = level;

                if(pos < covers.Count){

                    // 显示封面图
                    grid[i,j].SetImage(covers[pos]);
                    
                    grid[i,j].button.interactable = covers[pos] != null;
                    objectGrid[i,j].SetActive(true);

                    // 显示展示数量
                    int completed = SaveController.Instance.GetCompletedCount(level);
                    int all = Settings.LEVEL_COUNT[level];
                    grid[i,j].SetCount(all, completed);
                }
                // 如果未解锁
                else{
                    grid[i,j].SetImage(null);
                    // grid[i,j].button.interactable = false;
                    objectGrid[i,j].SetActive(false);
                }
            }
        }
    }

    // public void ShowNextPage(){}

    // public void ShowLastPage(){}

    



}
