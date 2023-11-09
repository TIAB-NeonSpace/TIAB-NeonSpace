using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    UISprite arrowSprite, dotSprite = null, touchSprite = null , oneMoreSprite = null;
    [SerializeField] LineRenderer LineR;
    [SerializeField] UISprite lineBallImg;
    [SerializeField]
    UILabel ballCount = null, moneyLabel = null, hudMoney = null, moveLabel = null, scoreLabel = null,  endScoreLabel = null, endBestSocre = null, oneMoreLabel = null , comboTextLabel = null;
    [SerializeField]
    TweenAlpha moveLabelAlpha;

    [SerializeField]
    UILabel scoreCoin = null;   //score 당 추가 획득 코인 ui
    [SerializeField]
    GameObject rocketObject;
    public bool isOneMore = false;
    public static UIManager instance;
    [SerializeField]
    float times_, charPos_;

    [SerializeField] UIButton oneMoreBtn;

    private int comboCnt ;  // From ComboManager.cs
    private bool isOver200 = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        SetMoney();
        BallManager.instance.ballHit = DataManager.instance.FirePower;
        if (PlayerPrefsElite.GetInt("First") == 1)
        {
            LobbyController.instance.SetTween(EnumBase.UIState.Tutorial , true);
            PlayerPrefsElite.SetInt("First", 2);
        }
        if(!DataManager.instance.GetSaveFile()) DataManager.instance.CurrentScore = 0;
        SetScore();
        Invoke("SoundSetting" , 0.21f);
    }

    void SoundSetting()
    {
        SoundManager.instance.OffSound();
        ResultOneMoreBtnDelete();
    }

    public void ResultBtoff(int idx)
    {
    }

    public void ArrowSpriteSetting(bool isShow, float eulerZ)
    {
        arrowSprite.enabled = isShow;
        touchSprite.enabled = isShow;
        if (BrickManager.instance.brickCount > 99)
        {
            dotSprite.enabled = false;
        }
        else dotSprite.enabled = isShow;

        arrowSprite.transform.localEulerAngles = new Vector3(0, 0, eulerZ);
        rocketObject.transform.localEulerAngles = new Vector3(0,0, eulerZ);
    }

    public void SetSprite(bool isShow, Vector3 pos)
    {
        arrowSprite.enabled = isShow;
        if (BrickManager.instance.brickCount > 99)
        {
            dotSprite.enabled = false;
        }
        else dotSprite.enabled = isShow;
        LineR.enabled = isShow;
        lineBallImg.enabled = isShow;
        arrowSprite.transform.localPosition = new Vector2(pos.x, arrowSprite.transform.localPosition.y);
    }

    public void SetLineBallPos()
    {
        lineBallImg.transform.localPosition = new Vector3(LineR.GetPosition(2).y, LineR.GetPosition(2).x, 0);
    }

    public void SetTouchSprite(Vector3 pos)
    {
        touchSprite.transform.position = pos;
    }

    public void Continue(bool isTrue)
    {
        if (isTrue)
        {
            DataManager.instance.SetSaveFile(false);
            LobbyController.instance.SetTween(EnumBase.UIState.Result, true);

            //민진 - Application.isEditor: 현재 유니티 에디터에서 실행중일 경우 true
            //하단 업적 관련 코드
            if(!Application.isEditor)
            {
                if (DataManager.instance.CurrentScore >= 100)
                {
                    GPGSManager.instance.setAchievements(2);
                }
                if (DataManager.instance.CurrentScore >= 200)
                {
                    GPGSManager.instance.setAchievements(3);
                }
                if (DataManager.instance.CurrentScore >= 300)
                {
                    GPGSManager.instance.setAchievements(4);
                }
                GPGSManager.instance.setScoreLeaderBoard();
            }
            //contiTween.PlayForward();
            //contiBG.PlayForward();
            //if (contiScale != null)
            //{
            //    contiScale.onFinished.Clear();
            //    contiTween.onFinished.Clear();
            //    contiTween.onFinished.Add(new EventDelegate(contiScale.PlayForward));
            //}
        }
        else
        {
            //contiBG.PlayReverse();
            //if (contiScale != null)
            //{
            //    contiScale.onFinished.Clear();
            //    contiTween.onFinished.Clear();
            //    contiScale.PlayReverse();
            //    contiScale.onFinished.Add(new EventDelegate(contiTween.PlayReverse));
            //}
        }
    }


    public void Result()
    {
        DataManager.instance.SetSaveFile(false);
        //DataManager.instance.SetSkill(false);
        //resultween.PlayForward();
        //resultBG.PlayForward();
        //if (resultScale != null)
        //{
        //    resultScale.onFinished.Clear();
        //    resultween.onFinished.Clear();
        //    resultween.onFinished.Add(new EventDelegate(resultScale.PlayForward));
        //}
        DataManager.instance.SetOnemore(false);
    }

    public void InAppCoin(bool isTrue)
    {
    }

    public void ResetGame()
    {
        GC.Collect();
        Init();
        DataManager.instance.SceneLoad();

        //oneMoreBT.isEnabled = true;
        //getCoinAds.isEnabled = true;
        //getBallBT.isEnabled = true;
    }

    public void SetBgChangeColor(string c)
    {
        //bgSprite.color = NGUIText.ParseColor(c);
    }

    //민진 - 게임 플레이 중 현재 스코어를 화면에 반영
    public void SetScore()
    {
        scoreLabel.text = string.Format("{0:N0}",DataManager.instance.CurrentScore);
        endScoreLabel.text = string.Format("{0:N0}", DataManager.instance.CurrentScore);
        SetScoreCoin();
    }

    public void SetBestScore()
    {
        endBestSocre.enabled = true;
    }

    public void Init()
    {
        DataManager.instance.SetSaveFile(false);
        BrickManager.instance.Init();
        endBestSocre.enabled = false;
    }

    public void OneMoreInfo(bool istrue)
    {

    }

    //민진 new - 게임 종료 시 종료에 따른 추가 코인 획득 ui
    public void SetScoreCoin()
    {
        scoreCoin.text = "+" + string.Format("{0:N0}", DataManager.instance.CurrentScoreCoin());
    }

    //민진 - 획득한 코인 수 세팅
    public void SetMoney()
    {
        moneyLabel.text = DataManager.instance.GetCoin().ToString();
        hudMoney.text = DataManager.instance.GetCoin().ToString();
    }

    public void SetBallCount(int cnt) // UI에서 공의 갯수를 세주는 함수
    {
        if (cnt == 0) ballCount.enabled = false;
        else
        {
            ballCount.enabled = true;
            ballCount.text = string.Format("x {0}", cnt);
        }
    }

    public void SetLabelPos(int cnt)
    {
        if (cnt > 0)
        {
            moveLabel.text = "+ " + cnt;
            moveLabelAlpha.ResetToBeginning();
            moveLabelAlpha.PlayForward();
        }
       
    }

    public void ResultBallSet()
    {
    }

    public void ResultCharSet()
    {
    }

    public void ResultOneMoreBtnDelete()
    {
        if(DataManager.instance.GetOneMoreBool())
        {
            oneMoreBtn.isEnabled = false;
            oneMoreLabel.color = Color.gray;
            oneMoreSprite.color = Color.gray;
        }
    }

    ///<summary>
    /// Move Code From ComboManager.cs To Here
    ///</summary>

    public void CountCombo()
    {
        comboCnt ++ ;
        // Debug.Log(comboCnt);
    }
    public void ResetCombo()
    {
        comboCnt = 0 ;
        comboTextLabel.fontSize = 30;
        // Debug.Log("Init ComboCount : " + comboCnt);
    }

    public void ShowComboCount()
    {
        // Debug.Log("showComboCount Active");
        comboTextLabel.gameObject.SetActive(true);

    }
    public void UpdateComboCount()
    {
        // Debug.Log("UpdateComboCount Active");
        comboTextLabel.text = comboCnt.ToString();
        if(!isOver200&&comboCnt >= 1 && comboCnt %20 ==0) //combo가 20마다 콤보텍스트의 스케일을 키우자 
        {
            comboTextLabel.fontSize  = comboTextLabel.fontSize + 7;
            // Debug.Log("combo over 20 ");
            // Combo가 커질떄마다 무한히 커지면 안되니까 콤보 400까지 커지도록
            if(comboCnt >= 400)
            {
                isOver200 = true;
            }
        }
        
        comboTextLabel.text = comboCnt.ToString();
    }
    public void HideComboCount()
    {
        // Debug.Log("HideComboCount Active");
        comboTextLabel.gameObject.SetActive(false);
    }

}
