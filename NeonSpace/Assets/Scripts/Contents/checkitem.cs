using UnityEngine;
using System.Collections;

public class laseritem : MonoBehaviour
{
    int idx, forcex;
    Brick brick_;
    Brick shieldbrick_;
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
        if(col.gameObject.CompareTag("ShieldBrick"))
        {
            shieldbrick_ = col.transform.parent.parent.GetComponent<Brick>();
            shieldbrick_.hitCnt -= 1;
            if (shieldbrick_.hitCnt <= 0)
            {
                shieldbrick_.FalseMySelf();
            }
            else shieldbrick_.LabelSetting();
            shieldbrick_.alphaOn();
        }
    }
}
