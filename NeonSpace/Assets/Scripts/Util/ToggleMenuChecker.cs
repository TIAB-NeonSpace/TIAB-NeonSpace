using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenuChecker : MonoBehaviour {
    [SerializeField] UIToggle[] myBtns;
    [SerializeField] ScrollListBtn[] myBtnScrolls;
    [SerializeField] UICenterOnChild myGrid;
    public int curGameViewInx;
    public static ToggleMenuChecker instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        curGameViewInx = 0;
    }

    public void CheckDrag()
    {
        for (int i = 0; i < myBtns.Length; ++i)
        {
            myBtns[i].value = false;
        }
        string str = myGrid.centeredObject.name.Replace("View_","");
        int cIdx = int.Parse(str);
        myBtns[cIdx].value = true;
        myBtnScrolls[cIdx].SetSelectImg();
    }

    public void ButtonClick()
    {
        Debug.Log("ButtonClick?!!");
    }
}
