using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
   public int comboCnt = 0;

   UILabel comboLabel; 

   public void CountCombo()
   {
        comboCnt ++ ;
        
   }

   public void ShowComboCount()
   {
        comboLabel.text = comboCnt.ToString();
   }
   

}
