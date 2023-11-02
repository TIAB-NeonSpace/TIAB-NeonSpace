using UnityEngine;
using System.Collections;

public class checkitem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Brick"))
        {
            //col.gameObject.GetComponent<Brick>().hitCnt -= 1;
            //if (col.gameObject.GetComponent<Brick>().hitCnt <= 0)
            //{
            //    col.gameObject.GetComponent<Brick>().FalseMySelf();
            //}
            //else col.gameObject.GetComponent<Brick>().LabelSetting();

            col.transform.parent.GetComponent<Brick>().HitCnt_Item();
        }
    }
}
