using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//커스텀 선택 버튼에 달릴 스크립트
public class SelectCustomBtn : MonoBehaviour
{
    [SerializeField] int SpriteNum;      //해당 커스텀 스프라이트 num (0~9)

    [SerializeField] UISprite ballSprite;
    [SerializeField] CurrentShipShow planeChange;   //우주선 스프라이트 관련 스크립트

    public void ChangeRocketCustom()
    {
        DataManager.instance.PlaneSprite = SpriteNum;
        planeChange.ChangeShip();

    }

    public void ChangeBallCustom()
    {
        DataManager.instance.BallSprite = SpriteNum;
        ballSprite.spriteName = DataManager.instance.GetBall();
    }
}
