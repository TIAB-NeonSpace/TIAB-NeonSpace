using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBrick : MonoBehaviour
{
    UISprite sprite;
    GameObject timer;

    private void OnEnable()
    {
        Debug.Log("��� ������");
        sprite = GetComponent<UISprite>();
        sprite.fillAmount = 1f;
    }

    public void timechecking()
    {
        if (sprite.fillAmount == 0f)
        {
            return;
        }
        if (sprite.fillAmount - 0.334f < 0f)
        {
            sprite.fillAmount = 0f;
            //Ÿ�̸� �̹����� ��� ��ü�� �����
            //���� �� ���� ����
            Invoke("laterdoing", 0.1f);
            timer = GameObject.Find("Timer");
            Destroy(timer);
        }

        else sprite.fillAmount -= 0.334f;


    }
    void laterdoing()
    {
        BallManager.instance.LastSetting();
    }
}
