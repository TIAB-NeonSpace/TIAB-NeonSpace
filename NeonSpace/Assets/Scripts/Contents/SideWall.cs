using UnityEngine;
using System.Collections;

public class SideWall : MonoBehaviour
{
    //게임 화면 하단에 위치한 콜라이더 오브젝트
    void Start()
    {
        this.gameObject.layer= 6;
    }

    //해당 오브젝트의 Trigger 처리
    void OnTriggerEnter2D(Collider2D cd)
    {
        //Trigger된 것이 벽돌일 경우,
        if (cd.gameObject.CompareTag("Brick"))
        {

            //moreCnt_가 1보다 작다면 == 블록이 더 내려갈 줄이 없다면
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
            //광고 시청하고 기회 얻기를 위한 처리
            BrickManager.instance.lastMoreCheck = cd.GetComponentInParent<BrickCount>();
        }

        //Trigger된 것이 아이템일 경우
        if (cd.gameObject.CompareTag("Item"))
        {
            Items item_ = cd.gameObject.GetComponent<Items>();
            if (item_ == null) return;

            //아이템 정보가 ball이면(공 추가 아이템), 소지 공 카운트를 + 1 하고 비활성화
            if (item_.itemType == Items.ItemInfo.Ball)
            {
                BallManager.instance.PlusBall(1);
                item_.BottomTrue();
            }
            //아니면 그냥 비활성화
            else
            {
                item_.BottomTrue();
            }
        }
    }
}
