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
        Tutorial,
        Reset
    }

    public enum ButtonState
    {
        None,
        TweenBtn,
        CharBtn,
        BallBtn,
        LeaderboardBtn,
        AchieveBtn,
        AdsBtn,
        ResetBtn
    }

    public enum Special_Brick
    {
        none = 0,
        sheld = 1,
        timer = 2,
        reflect = 3,
        direct = 4,
    }
}
