using UnityEngine;
using System.Collections;

public class EnumBase : MonoBehaviour
{
    public enum UIState
    {
        None,
        SocialF,
        RocketBook,
        CharShow,
        Play,
        BallUp,
        Lobby,
        Back,
        BallSelect,
        Coin,
        HiddenBall,
        Exit,
        CharBuy,
        ChargeMoney,
        Promotion,
        IncorrectCoupon,
        CorrectCoupon,
        ActionPop,
        Rating,
        Result,
        Pause,
        InGame,
        BestScore,
        Plane,
        Tutorial
    }

    public enum ButtonState
    {
        None,
        TweenBtn,
        CharBtn,
        BallBtn,
        LeaderboardBtn,
        AchieveBtn,
        AdsBtn
    }
}
