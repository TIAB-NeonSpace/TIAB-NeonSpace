using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ŀ���� ���� ��ư�� �޸� ��ũ��Ʈ
public class SelectCustomBtn : MonoBehaviour
{
    [SerializeField] int SpriteNum;      //�ش� Ŀ���� ��������Ʈ num (0~9)

    [SerializeField] UISprite ballSprite;
    [SerializeField] CurrentShipShow planeChange;   //���ּ� ��������Ʈ ���� ��ũ��Ʈ

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
