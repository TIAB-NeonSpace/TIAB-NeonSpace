using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollListBtn : MonoBehaviour {
    public Vector3 testPos;
    [SerializeField] GameObject SelectImg;
    [SerializeField] GameObject myScrollView;
    [SerializeField] ToggleMenuChecker MenuCheck;
    [SerializeField] int CurIdx;

    void OnClick()
    {
        MenuCheck.curGameViewInx = CurIdx;
        SpringPanel.Begin(myScrollView.gameObject, testPos, 8f);
        SetSelectImg();
        //myScrollView.SetDragAmount(myPos, 0, false);
    }

    public void SetSelectImg()
    {
        SpringPanel.Begin(SelectImg, gameObject.transform.localPosition, 8f);
    }
}
