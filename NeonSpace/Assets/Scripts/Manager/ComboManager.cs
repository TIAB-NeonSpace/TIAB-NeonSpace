using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;
    private int comboCnt ;
   
   public Text comboText;


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
        comboText.gameObject.SetActive(true);
        comboText.text = comboCnt.ToString();
    }
    public void UpdateComboCount()
    {
        comboText.text = comboCnt.ToString();
    }
    public void HideComboCount()
    {
        comboText.gameObject.SetActive(false);
    }

}
