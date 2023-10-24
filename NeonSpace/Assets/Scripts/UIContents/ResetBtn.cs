using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBtn : MonoBehaviour
{
    void OnClick()
    {
        BallManager.instance.ResetBall();
    }
}
