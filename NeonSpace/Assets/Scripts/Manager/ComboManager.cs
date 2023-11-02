using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;
    private int comboCnt ;
   

    UILabel comboLabel; 

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        comboCnt = 0;
    }
    public void CountCombo()
    {
            comboCnt ++ ;
            Debug.Log(comboCnt);
    }
    public void ResetCombo()
    {
        comboCnt = 0 ;
        Debug.Log("Init ComboCount : " + comboCnt);
    }
    public void ShowComboCount()
    {
            comboLabel.text = comboCnt.ToString();
    }
   
}
