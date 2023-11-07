using UnityEngine;
using System.Collections;

public class BottomWall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D cd)
    {
        if(cd.gameObject.CompareTag("Ball"))
        {
            BallManager.instance.SetBall(cd.gameObject);
        }
    }
}
