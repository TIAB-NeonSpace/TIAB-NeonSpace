using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBrick : MonoBehaviour
{
    UISprite sprite;
    GameObject timer;

    private void OnEnable()
    {
        sprite.fillAmount = 1f;
    }

    private void Awake()
    {
        sprite = GetComponent<UISprite>();
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
            Invoke("laterdoing", 0.1f);
        }

        else sprite.fillAmount -= 0.334f;


    }
    void laterdoing()
    {
        BallManager.instance.LastSetting();
        transform.parent.parent.GetComponent<Brick>().FalseMySelf();
    }
}
