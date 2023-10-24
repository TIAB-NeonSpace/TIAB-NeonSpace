using UnityEngine;
using System.Collections;

public class laseritem : MonoBehaviour
{
    int idx, forcex;
    Brick brick_;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Brick"))
        {
            brick_ = col.transform.parent.GetComponent<Brick>();
            brick_.hitCnt -= 1;
            if (brick_.hitCnt <= 0)
            {
                brick_.FalseMySelf();
            }
            else brick_.LabelSetting();
            brick_.alphaOn();
        }
    }
}
