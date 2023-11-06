using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBrick : MonoBehaviour
{
    UISprite sprite;
    GameObject timer;

    private void OnEnable()
    {
        Debug.Log("블록 생성시");
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
            //타이머 이미지랑 블록 자체가 사라짐
            //턴을 한 번더 실행
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
