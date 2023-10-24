using UnityEngine;
using System.Collections;

public class HiddenBallTouch : MonoBehaviour
{
    int cnt_;
    void OnClick()
    {
        ++cnt_;
        if(cnt_ > 2 && !DataManager.instance.GetHiddenBall())
        {
            DataManager.instance.SetHiddenBall();
            LobbyController.instance.SetTween(EnumBase.UIState.HiddenBall, true);
            SoundManager.instance.ChangeEffects(7);
            Invoke("FalseTween", 1);
        }
    }

    void FalseTween()
    {
        LobbyController.instance.SetTween(EnumBase.UIState.HiddenBall, false);
    }
}
