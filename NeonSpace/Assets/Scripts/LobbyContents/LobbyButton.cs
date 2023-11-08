using UnityEngine;
using System.Collections;


public class LobbyButton : MonoBehaviour
{
  
    [SerializeField]
    EnumBase.UIState state_;
    [SerializeField] EnumBase.ButtonState btnState_;
    [SerializeField] bool isPlay = true;
    [SerializeField] int idx;

    void OnClick()
    {
        SoundManager.instance.TapSound();
        if(btnState_ == EnumBase.ButtonState.TweenBtn) LobbyController.instance.SetTween(state_, isPlay);
        if(btnState_ == EnumBase.ButtonState.BallBtn)
        {
            return;
        }
        if (btnState_ == EnumBase.ButtonState.ResetBtn) LobbyController.instance.resetBundle.SetActive(true);

        if(btnState_ == EnumBase.ButtonState.CharBtn)
        {
            if (DataManager.instance.GetSaveMyChar(idx) > 0)
            {
                DataManager.instance.SetChar(idx);
                LobbyController.instance.SetCharShow();
            }
            else
            {
                if (DataManager.instance.GetCoin() < 2000)
                {
                    LobbyController.instance.SetTween(EnumBase.UIState.ChargeMoney, true);

                }
                else
                {
                    LobbyToggleManager.instance.buyCharIdx = idx;
                    LobbyController.instance.SetTween(EnumBase.UIState.CharBuy, true);
                }

            }
            return;
        }

        if (btnState_ == EnumBase.ButtonState.LeaderboardBtn)
        {
            GPGSManager.instance.showLeaderBoard();
        }
        if (btnState_ == EnumBase.ButtonState.AchieveBtn)
        {
            GPGSManager.instance.showAchievements();
        }
        if(btnState_ == EnumBase.ButtonState.AdsBtn)
            AdsManager.instance.ShowRewardedAd(0);
        switch (state_)
        {
            case EnumBase.UIState.SocialF:
                Application.OpenURL("https://www.facebook.com/DeliciousGame");
                break;
            case EnumBase.UIState.Rating:
                Application.OpenURL("market://details?id=com.deliciousgames.NeonSpace");
                break;
            case EnumBase.UIState.RocketBook:
                LobbyToggleManager.instance.SetBallToggle();
                break;
            case EnumBase.UIState.CharShow:
                LobbyController.instance.rocketBundle.SetActive(false);
                break;
            case EnumBase.UIState.Play:
                DataManager.instance.SetGamePlayCount();
                DataManager.instance.SetOnemore(false);
                LobbyController.instance.ChangeStateGame(true);
                break;
            case EnumBase.UIState.BallUp:
                break;
            case EnumBase.UIState.Back:
                LobbyController.instance.SetBack();
                break;
            case EnumBase.UIState.Exit:
                if(idx == 0) LobbyController.instance.SetExit();
                LobbyController.instance.SetTween(EnumBase.UIState.Exit, false);
                break;
            case EnumBase.UIState.Reset:
                LobbyController.instance.resetBundle.SetActive(false);
                break;
            case EnumBase.UIState.CharBuy:
                DataManager.instance.SetSaveMyChar(LobbyToggleManager.instance.buyCharIdx);
                LobbyController.instance.SetBack();
                DataManager.instance.SetCoin(-8000);
                LobbyController.instance.SetLabel(EnumBase.UIState.Coin, string.Format("{0:N0}", DataManager.instance.GetCoin()));
                DataManager.instance.SetChar(LobbyToggleManager.instance.buyCharIdx);
                LobbyController.instance.SetCharShow();
                break;
            case EnumBase.UIState.Coin:
                DataManager.instance.purchaser_.BuyItem(idx);
                break;
            case EnumBase.UIState.ActionPop:
                LobbyController.instance.SetActionPop(false);
                break;
            case EnumBase.UIState.Tutorial:
                LobbyController.instance.SetTween(EnumBase.UIState.Tutorial, true);
                break;
        }
    }
}
