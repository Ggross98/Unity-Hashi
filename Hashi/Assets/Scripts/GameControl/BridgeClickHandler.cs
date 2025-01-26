using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BridgeClickHandler : MonoBehaviour, IPointerClickHandler
{

    private UnityEvent leftClick, rightClick;
    // public BridgeManager bridgeManager;
    [SerializeField] private GameMain game;
    public Bridge bridge;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(eventData.button){
            case PointerEventData.InputButton.Left:
                leftClick.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                rightClick.Invoke();
                break;
        }
    }

    void Awake()
    {
        leftClick = new UnityEvent();
        rightClick = new UnityEvent();
        leftClick.AddListener(new UnityAction(LeftClick));
        rightClick.AddListener(new UnityAction(RightClick));
    }

    private void LeftClick(){
        // bridgeManager.ClickBridge(bridge, Utils.LEFT_CLICK);
        game.ClickBridge(bridge, Utils.LEFT_CLICK);
    }

    private void RightClick(){
        // bridgeManager.ClickBridge(bridge, Utils.RIGHT_CLICK);
        game.ClickBridge(bridge, Utils.RIGHT_CLICK);
    }
}
