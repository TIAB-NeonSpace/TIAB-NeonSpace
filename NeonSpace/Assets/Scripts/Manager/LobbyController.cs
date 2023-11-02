using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public static LobbyController instance;
    public List<LobbyLabel> lobbyLabel;
    public List<LobbySprite> lobbySprite;
    public GameObject rocketBundle;
    public GameObject rocketbookBundle;
    [SerializeField] GameObject ingameObj;
    [SerializeField] GameObject lobbyObj;
    int action_idx_ = 0;
     [SerializeField] EnumBase.UIState uiState = EnumBase.UIState.Lobby;
    public List<EnumBase.UIState> addState;
    [SerializeField]Animator ingameAnim;
    [SerializeField] TweenbundleManager tweens_;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(!Application.isEditor && !GPGSManager.instance.bLogin()) GPGSManager.instance.LoginGPGS();
        Invoke("LateStart", 0.1f);
    }

    void LateStart()
    {
       
        if (string.IsNullOrEmpty( DataManager.instance.GetBall()))
        {
            DataManager.instance.SetBall(0);
        }
        SetLabel(EnumBase.UIState.BestScore, string.Format("{0:N0}", DataManager.instance.GetBestScore()));
        SetLabel(EnumBase.UIState.Coin, string.Format("{0:N0}", DataManager.instance.GetCoin()));
        SetCharShow();
    }

    public void SetCharShow() // 이건 특수한 사항이기 때문에 이렇게 한다^_^
    {
    }

    public void SetActionPop(bool isShow)
    {
        Debug.Log(213);
        switch (DataManager.instance.GetGamePlayCount())
        {
            case 1:
                action_idx_ = 1;
                //SetTween(EnumBase.LobbyUIState.ActionPop, true);
                DataManager.instance.SetGamePlayCount();
                break;
            case 2:
                if (!isShow)
                {
                    //SetTween(EnumBase.LobbyUIState.ActionPop, false);
                    Application.OpenURL("market://details?id=com.PickmeGames.RoyalBricksBreaker");
                }
                break;
            case 11:
                action_idx_ = 2;
                //SetTween(EnumBase.LobbyUIState.ActionPop, isShow);
                DataManager.instance.SetGamePlayCount();
                GPGSManager.instance.setAchievements(0);
                if (!isShow) DataManager.instance.purchaser_.BuyItem(0);
                break;
            case 22:
                action_idx_ = 3;
                DataManager.instance.SetGamePlayCount();
                if (!isShow)
                {
                    Application.OpenURL("market://details?id=com.DeliciousGames.TouchPang");
                }
                //SetTween(EnumBase.LobbyUIState.ActionPop, isShow);
                break;
            case 23:
                if (!isShow)
                {
                    //SetTween(EnumBase.LobbyUIState.ActionPop, false);
                    if(!Application.isEditor)
                        AdsManager.instance.BannerEnable();
                }
                break;
            case 33:
                action_idx_ = 4;
                DataManager.instance.SetGamePlayCount();
                //SetTween(EnumBase.LobbyUIState.ActionPop, isShow);
                if (!isShow) GPGSManager.instance.showLeaderBoard();
                break;
            case 53:
                GPGSManager.instance.setAchievements(1);
                break;
            default:
                //if(!isShow) SetTween(EnumBase.LobbyUIState.ActionPop, isShow);
                //else
                //{
                //    if (DataManager.instance.GetGamePlayCount() % 3 == 0 && DataManager.instance.GetGamePlayCount() > 0)
                //    {

                //    }    
                //}
                break;
        }

        if (isShow)
        {
            //SetLabel(EnumBase.LobbyUIState.ActionPop, DataManager.instance.GetAction(action_idx_));
            action_idx_ = action_idx_ + 1;
            //SetSprite(EnumBase.LobbyUIState.ActionPop, "Action" + action_idx_);
        }
        action_idx_ = 0;
    }

    //민진 - tween 버튼 처리 메소드
    public void SetTween(EnumBase.UIState state_, bool isBool)
    {
        if (state_ == EnumBase.UIState.Pause) Time.timeScale = isBool?0f:1f;

        //민진 - true일 경우 현재 uiState를 리스트에 저장하고 새 state_를 uiState에 대입
        //블록이 바닥에 닿았을 때 state_ == Result, isBool == true
        if (isBool)
        {
            addState.Add(uiState);
            uiState = state_;
        }
        //민진 - false일 경우 uiState에 가장 최신 uiState를 가져옴, 가져온 State는 리스트에서 삭제
        else
        {
            uiState = addState[addState.Count - 1];
            addState.Remove(uiState);
        }
        if(uiState == EnumBase.UIState.Lobby) rocketBundle.SetActive(true);
        if(uiState == EnumBase.UIState.RocketBook) rocketbookBundle.SetActive(false);
        if(uiState == EnumBase.UIState.CharShow) rocketbookBundle.SetActive(true);
        //만약 uiState == Result라면, 현재 스코어에 따른 추가 코인 획득
        tweens_.ShowBundleObject(state_ , isBool);
    }

    public void SetLabel(EnumBase.UIState state_, string text , bool isShow = true)
    {
        for (int i = 0; i < lobbyLabel.Count; ++i)
        {
            if (lobbyLabel[i].state_ == state_)
            {
                lobbyLabel[i].SetLabel(text , isShow);
            }
        }
    }

    public void SetSprite(EnumBase.UIState state_ , string str , bool isShow = true)
    {
        for (int i = 0; i < lobbySprite.Count; ++i)
        {
            if (lobbySprite[i].state_ == state_)
            {
                lobbySprite[i].SetSprite(str , isShow);
            }
        }
    }

    public void ChangeStateGame(bool isTrue)
    {
        if(isTrue)
        {
            uiState = EnumBase.UIState.InGame;
            ingameObj.SetActive(true);
            lobbyObj.SetActive(false);
        }
        else
        {
            uiState = EnumBase.UIState.Lobby;
            ingameObj.SetActive(false);
            lobbyObj.SetActive(true);
        }
        ingameAnim.SetBool("start", isTrue);
      
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(uiState == EnumBase.UIState.Lobby)
            {
                SetTween(EnumBase.UIState.Exit, true); 
            }
            else if(uiState == EnumBase.UIState.InGame)
            {
                SetTween(EnumBase.UIState.Pause, true);
            }
            else
            {
                SetBack();
            }
        }
    }

    public void SetBack()
    {
        SetTween(uiState, false);
    }

    public void SetExit()
    {
        Application.Quit();
    }

    public EnumBase.UIState getGameState()
    {
        return uiState;
    }
}
