using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryController : MonoBehaviour
{
    [SerializeField] private CGController cgController;
    [SerializeField] private CoverController coverController;

    private List<List<Sprite>> cgLists;
    // private List<Sprite> coverList;
    private int pageSize = 12;
    private int maxPage, currentPage;

    private void Awake() {

        // 从存档加载CG解锁信息
        Unlock();

        // 计算页数
        maxPage = (pageSize - 1 + Settings.LEVELS) / pageSize;
    }

    private void Start() {
        // 显示第一页
        currentPage = 0;
        ShowPage(currentPage);
    }

    private void Unlock(){
        cgLists = new List<List<Sprite>>();
        var completedLevels = SaveController.Instance.GetCurrentSave().completedLevels;

        for(int i = 0; i<Settings.LEVELS; i++){
            var allSprites = SaveController.Instance.GetCGList(i);
            var unlockedSprites = new List<Sprite>();

            int completed = completedLevels[i];
            for(int j = 0; j<completed; j++){
                unlockedSprites.Add(allSprites[j]);
            }

            cgLists.Add(unlockedSprites);
        }
        
    }

    private void ShowPage(int page){

        int index0 = page * pageSize;
        int index1 = Mathf.Min(Settings.LEVELS, index0 + pageSize);
        var covers = new List<Sprite>();
        
        for(int i = index0; i<index1; i++){
            // 如果尚未解锁该大关，不显示封面
            if(cgLists[i].Count == 0) covers.Add(null);
            // 否则显示封面图
            else covers.Add(SaveController.Instance.GetCover(i));
        }
        coverController.ShowPageCover(index0, covers);

    }

    public void ShowNextPage(){
        currentPage ++;
        if(currentPage >= maxPage){
            currentPage = 0;
        }
        ShowPage(currentPage);
    }

    public void ShowLastPage(){
        currentPage --;
        if(currentPage < 0){
            currentPage = maxPage - 1;
        }
        ShowPage(currentPage);
    }
    
    public void ClickCover(Cover c){
        
        cgController.SetCGList(cgLists[c.index]);
        cgController.Show();
    }

    
}
