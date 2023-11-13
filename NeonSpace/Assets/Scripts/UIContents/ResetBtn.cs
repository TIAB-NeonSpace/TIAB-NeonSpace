using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class ResetBtn : MonoBehaviour
{
    private bool isWait;
    private float waitTimer;

    private void Start()
    {
        isWait = false;
        waitTimer = 0.75f; 
    }

    void OnClick()
    {
        if (!isWait)
        {
            BallManager.instance.ResetBall();

            StartCoroutine(Timer_Wait());
        }
    }

    IEnumerator Timer_Wait()
    {
        isWait = true;
        yield return new WaitForSeconds(waitTimer);
        isWait = false;
    }
}
