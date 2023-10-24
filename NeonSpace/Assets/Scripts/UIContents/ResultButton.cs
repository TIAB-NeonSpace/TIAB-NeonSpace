using UnityEngine;
using System.Collections;

public class ResultButton : MonoBehaviour
{
    [SerializeField]
    int idx;
    void OnClick()
    {
        switch(idx)
        {
            case 0:
                UIManager.instance.ResetGame();
                LobbyController.instance.SetTween(EnumBase.UIState.Pause, false);
                LobbyController.instance.ChangeStateGame(false);
                break;
            case 1:
                DataManager.instance.SetOnemore(false);
                UIManager.instance.ResetGame();
                LobbyController.instance.SetTween(EnumBase.UIState.Result, false);
                LobbyController.instance.ChangeStateGame(false);
                break;
            case 2:
                break;
            case 3:
                ChargeManager.intance.BuyTween(true, 100); // 코인 구매
                break;
            case 4:
                ChargeManager.intance.BuyTween(true, 220);
                break;
            case 5:
                ChargeManager.intance.BuyTween(true, 500);
                break;
            case 6:
                ChargeManager.intance.BuyTween(true, 1200); // 코인 구매 
                break;
            case 7: 
                if(Application.platform == RuntimePlatform.Android)
                {
                    
                }
                else AdsManager.instance.ShowRewardedAd(0);// Result Get Coin

                UIManager.instance.ResultBtoff(0);
                break;
            case 8:
                BrickManager.instance.OneMoreChance(); // Continue One More
                break;
            case 9: 
                UIManager.instance.Result(); // Continue End Game
                UIManager.instance.Continue(false);
                UIManager.instance.SetMoney();
                UIManager.instance.ResultBallSet();
                UIManager.instance.ResultCharSet();
                break;
            case 10: 
               
                GaChaManager.instance.gaChaBundle();
                UIManager.instance.ResultBtoff(1);
                break;
            case 11:
                GaChaManager.instance.BallDraw(); // BallDraw 뽑기 버튼
                break;
            case 12: 
                break;
            case 13:
                GaChaManager.instance.DrawBallPOP_(false);
                break;
            case 14:
                ChargeManager.intance.BuySelec(); // 코인 구매 확인 팝업창
                ChargeManager.intance.BuyTween(false);
                break;
            case 15:
                ChargeManager.intance.BuyTween(false);  // 코인 구매 팝업창 닫기
                break;
            case 16:
                GaChaManager.instance.DrawKick(); // BallDraw 킥 버튼
                break;
            case 17:
                UIManager.instance.Continue(false);
                UIManager.instance.Result();
                break;
            case 18:
                break;
            case 19:
                GaChaManager.instance.GetballInfoOff();
                break;
            case 20: 
                GPGSManager.instance.showLeaderBoard(); // 랭킹 버튼
                break;
            case 21:

                break;
            case 22:
                GPGSManager.instance.showAchievements(); // 업적 버튼
                break;
            case 23:
                ChargeManager.intance.ReBuyTween(true, 100); // 코인 구매
                break;
            case 24:
                ChargeManager.intance.ReBuyTween(true, 220);
                break;
            case 25:
                ChargeManager.intance.ReBuyTween(true, 500);
                break;
            case 26:
                ChargeManager.intance.ReBuyTween(true, 1200); // 코인 구매 
                break;
            case 27:
                ChargeManager.intance.ReBuySelec();
                ChargeManager.intance.ReBuyTween(false);
                break;
            case 28:
                ChargeManager.intance.ReBuyTween(false); // 코인 구매 
                break;
            case 29:
                GaChaManager.instance.noballPOP(false);
                break;
        }
        SoundManager.instance.TapSound();
    }

}
