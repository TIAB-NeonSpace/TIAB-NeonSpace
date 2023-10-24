using UnityEngine;
using System.Collections;

public class SideWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D cd)
    {
        if (cd.gameObject.CompareTag("Brick"))
        {
            if (BrickManager.instance.moreCnt_ < 1)
            {
                UIManager.instance.Continue(true);
            }
            else
            {
                UIManager.instance.Result();
                DataManager.instance.SetSaveOnemoreCnt(0);
                UIManager.instance.ResultBallSet();
                UIManager.instance.ResultCharSet();
            }
            SoundManager.instance.ChangeEffects(5);
            BrickManager.instance.lastMoreCheck = cd.GetComponentInParent<BrickCount>();
        }

        if (cd.gameObject.CompareTag("Item"))
        {
            Items item_ = cd.gameObject.GetComponent<Items>();
            if (item_ == null) return;
            if (item_.itemType == Items.ItemInfo.Ball)
            {
                BallManager.instance.PlusBall(1);
                item_.BottomTrue();
            }
            else
            {
                item_.BottomTrue();
            }
        }
    }
}
