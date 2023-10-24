using UnityEngine;
using System.Collections;

public class ButtonScripts : MonoBehaviour
{
    void OnPress(bool isShow)
    {

        //if(isShow) BallManager.instance.Init();
        if (isShow)
        {
            Time.timeScale = 3.0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
