using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewCheck : MonoBehaviour {

    void OnPress(bool isBool)
    {
        if (!isBool)
        {
            ToggleMenuChecker.instance.CheckDrag();
        }
    }
}
