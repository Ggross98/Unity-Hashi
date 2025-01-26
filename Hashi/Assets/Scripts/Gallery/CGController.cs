using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGController : MonoBehaviour
{   
    [SerializeField] private GameObject cgPlane;
    [SerializeField] private Image cgPlayer;
    [SerializeField] private RectTransform cgRect;

    // public Sprite test_cg;
    private List<Sprite> cgList;
    private int cgIndex;

    private const int CG_HEIGHT = 1080;

    private void Awake() {
        // cgRect = cgPlayer.GetComponent<RectTransform>();
    }

    private void Start() {

    }

    public void Show(){
        cgPlane.SetActive(true);
    }

    public void Hide(){
        cgPlane.SetActive(false);
    }

    public void SetCGList(List<Sprite> cg){
        cgList = cg;

        cgIndex = 0;
        SetCG(cgList[cgIndex]);
    }

    public void SetCG(Sprite s){
        
        if(s == null){
            cgPlayer.sprite = null;
            return;
        }

        // Debug.Log(s);
        var rate = (float)s.texture.width / (float)s.texture.height;
        var _width = rate * CG_HEIGHT;
        cgRect.sizeDelta = new Vector2(_width, CG_HEIGHT);

        cgPlayer.sprite = s;

    }

    public void ShowNextCG(){

        if(cgList == null || cgList.Count == 0) return;

        cgIndex ++;
        if(cgIndex >= cgList.Count) cgIndex = 0;
        SetCG(cgList[cgIndex]);
    }

    public void ShowLastCG(){
        if(cgList == null || cgList.Count == 0) return;

        cgIndex --;
        if(cgIndex < 0) cgIndex = cgList.Count - 1;
        SetCG(cgList[cgIndex]);
    }

}
