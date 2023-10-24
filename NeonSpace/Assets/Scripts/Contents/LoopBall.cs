using UnityEngine;
using System.Collections;

public class LoopBall : MonoBehaviour
{
    GameObject go;
    [SerializeField]
    bool isTop;
    int idx;
    void OnCollisionEnter2D(Collision2D cd)
    {
        if(cd.gameObject.CompareTag("Ball"))
        {
            if(!isTop)
            {
                Vector3 incomingVector = cd.transform.position - cd.gameObject.GetComponent<BallMove>().ball_Pos_;
                //Vector3 normalVector = cd.contacts[0].normal;
                //Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector);
                if (Mathf.Abs( incomingVector.y )<= 0.02f)
                {
                   
                    ++idx;
                    if (idx >= 7)
                    {
                        BrickManager.instance.SetPosPingpong(cd.transform.localPosition.y);
                        idx = 0;
                    }
                }
            }
            cd.gameObject.GetComponent<BallMove>().ball_Pos_ = cd.transform.position;
        }
    }
}
