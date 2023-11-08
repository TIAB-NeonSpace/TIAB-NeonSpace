using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTextField : MonoBehaviour
{
    public static ComboTextField instance;
    // public Text comboText;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // comboText = GameObject.Find("ComboTextField").GetComponent<Text>();
    }

    // public void ShowComboCount()
    // {
    //     gameObject.SetActive(true);
    //     comboText.text = "sda";
    // }

    // public void HideComboCount()
    // {
    //     gameObject.SetActive(false);
    // }
   
}
