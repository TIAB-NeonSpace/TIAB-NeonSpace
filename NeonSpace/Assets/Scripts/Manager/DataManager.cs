using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CompleteProject;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Purchaser purchaser_;

    [SerializeField] int[] upgradeStartBallCoins;
    [SerializeField] int[] upgradeFireBallCoins;

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if(!PlayerPrefsElite.GetBoolean("firstLogin"))
        {
            PlayerPrefsElite.SetBoolean("firstLogin" , true);
            StartBall = 1;
            FirePower = 1;
        }
    }

    public void SceneLoad() 
    {
        SceneManager.LoadScene(0);
    }

    public void SetCoin(int i)
    {
        PlayerPrefsElite.SetInt("Money", GetCoin() + i);
    }

    public int GetCoin()
    {
        if (PlayerPrefs.HasKey("Money"))
            return PlayerPrefsElite.GetInt("Money");
        else
            return 0;
    }

    public void SetBestScore()
    {
        PlayerPrefsElite.SetInt("Score", CurrentScore);
    }

    public int GetBestScore()
    {
        return PlayerPrefsElite.GetInt("Score");
    }

    //현재 스코어를 get, set
    public int CurrentScore
    {
        get{return PlayerPrefsElite.GetInt("CScore");}
        set{PlayerPrefsElite.SetInt("CScore" , value);}
    }

    //민진 new - 현재 스코어에 따른 코인 수 반환
    public int CurrentScoreCoin()
    {
        int currentScore = CurrentScore;
        int getCoin = currentScore / 100;
        return getCoin; 
    }


    public int CurrentCoin
    {
        get { return PlayerPrefsElite.GetInt("CCoin"); }
        set { PlayerPrefsElite.SetInt("CCoin", value); }
    }


    public void SetHiddenBall()
    {
        PlayerPrefsElite.SetBoolean("HiddenBall", true);
    }

    public bool GetHiddenBall()
    {
        return PlayerPrefsElite.GetBoolean("HiddenBall");
    }

    //----------------------------게임 저장 관련----------------------------------------
    /// <summary>
    /// 이거는 Save파일을 설정하는 함수입니다.
    /// </summary>
    public void SetSaveFile(bool b)
    {
        PlayerPrefsElite.SetBoolean("SaveFile", b);
    }
    /// <summary>
    /// 이거는 Save파일이 있는지 확인하는 함수입니다. ture면 있는 겁니다.
    /// </summary>
    public bool GetSaveFile()
    {
        return PlayerPrefsElite.GetBoolean("SaveFile");
    }

    public void SetBrickPos(int i, Vector2 pos)
    {
        PlayerPrefsElite.SetVector2("BlockPos" + i, pos);
    }

    public Vector2 GetBrickPos(int i)
    {
        return PlayerPrefsElite.GetVector2("BlockPos" + i);
    }

    public void SetSaveBlock(List<int> blocks , int i)
    {
        PlayerPrefsElite.SetIntArray("blocks" + i, blocks.ToArray());
    }

    public List<int> GetSaveBlock(int i)
    {
        List<int> list_ = new List<int>(PlayerPrefsElite.GetIntArray("blocks" + i));
        return list_;
    }

    public void SetSaveItems(List<int> Items, int i)
    {
        PlayerPrefsElite.SetIntArray("Items" + i, Items.ToArray());
    }

    public List<int> GetSaveItems(int i)
    {
        List<int> list_ = new List<int>(PlayerPrefsElite.GetIntArray("Items" + i));
        return list_;
    }

    public void SetSaveSpecial(List<int> Items, int i)
    {
        PlayerPrefsElite.SetIntArray("Specials" + i, Items.ToArray());
    }

    public List<int> GetSaveSpecial(int i)
    {
        List<int> list_ = new List<int>(PlayerPrefsElite.GetIntArray("Specials" + i));
        return list_;
    }

    public void SetSaveBrickCount(int i)
    {
        PlayerPrefsElite.SetInt("SaveBrickCnt", i);
    }

    public int GetSaveBrickCount()
    {
        return PlayerPrefsElite.GetInt("SaveBrickCnt");
    }

    public void SetSaveBallCount(int i)
    {
        PlayerPrefsElite.SetInt("SaveBallCnt", i);
    }

    public int GetSaveBallCount()
    {
        return PlayerPrefsElite.GetInt("SaveBallCnt");
    }

    public void SetSaveBrickPosCnt(int i)
    {
        PlayerPrefsElite.SetInt("PosCnt", i);
    }

   public int GetSaveBrickPosCnt()
    {
        return PlayerPrefsElite.GetInt("PosCnt");
    }

    public void SetSaveBrickBool(bool b)
    {
        PlayerPrefsElite.SetBoolean("PosBool", b);
    }

    public bool GetSaveBrickBool()
    {
        return PlayerPrefsElite.GetBoolean("PosBool");
    }

    public void SetAchievement(int b)
    {
        PlayerPrefsElite.SetBoolean(string.Format("Achievement_{0}", b), true);
    }

    public bool GetAchievement(int b)
    {
        if (PlayerPrefs.HasKey(string.Format("Achievement_{0}", b)))
            return PlayerPrefsElite.GetBoolean(string.Format("Achievement_{0}", b));
        else
            return false;
    }

    public void SetOnemore(bool isBool)
    {
        PlayerPrefsElite.SetBoolean("SetOneMore", isBool);
    }

    public bool GetOneMoreBool()
    {
        return PlayerPrefsElite.GetBoolean("SetOneMore");
    }

    public void SetSaveOnemoreCnt(int i)
    {
        PlayerPrefsElite.SetInt("OneMoreCnt", i);
    }

    public int GetSaveOnemoreCnt()
    {
        return PlayerPrefsElite.GetInt("OneMoreCnt");
    }
    //----------------------------게임 저장 관련----------------------------------------

    public void SetBall(int i)
    {
        PlayerPrefsElite.SetInt("Ball", i);
    }

    public string GetBall()
    {
        string ballStr_ = BallDataManager.instance.BallDataList[BallSprite].bName;
        return ballStr_;
    }

    public int GetBallInt()
    {
        return PlayerPrefsElite.GetInt("Ball");
    }
    
    public void SetChar(int i)
    {
        PlayerPrefsElite.SetInt("Char", i);
    }

    public int GetChar()
    {
        return PlayerPrefsElite.GetInt("Char");
    }

    public void SetSaveMyChar(int i)
    {
    }

    public int GetSaveMyChar(int i)
    {
        return GetSaveMyCharList()[i];
    }
    public List<int> GetSaveMyCharList()
    {
        List<int> list_ = new List<int>(PlayerPrefsElite.GetIntArray("MyCharList"));
        return list_;
    }

    public bool GetSkill()
    {
        return PlayerPrefsElite.GetBoolean("SkillBool");
    }

    public void SetSkill(bool isShow)
    {
        PlayerPrefsElite.SetBoolean("SkillBool" , isShow);
    }

    public void SetSoundState( bool isBool)
    {
        PlayerPrefsElite.SetBoolean("Sound", isBool);
    }

    public bool GetSoundState()
    {
        return PlayerPrefsElite.GetBoolean("Sound");
    }

    public void SetGamePlayCount()
    {
        PlayerPrefsElite.SetInt("PlayCnt", GetGamePlayCount() + 1);
    }

    public int GetGamePlayCount()
    {
        return PlayerPrefsElite.GetInt("PlayCnt");
    }

//---------------------------------------------------- 업그레이드 요소를 여기에 넣어준다.-------------------------------------------

    public int StartBall // 현재 시작할 때 볼을 뜻한다.
    {
        get{return PlayerPrefsElite.GetInt("StartBalls");}
        set{PlayerPrefsElite.SetInt("StartBalls" , value);}
    }
    public int FirePower// 현재 공에 맞으면 블럭이 몇이 깎이는지 보여준다.
    {
        get{return PlayerPrefsElite.GetInt("FirePowers");}
        set{PlayerPrefsElite.SetInt("FirePowers" , value);}
    }


    public int ReturenStartBallCoin
    {
        get {return upgradeStartBallCoins[StartBall - 1];}
    }

      public int ReturenFireBallCoin
    {
        get {return upgradeFireBallCoins[FirePower - 1];}
    }


    public int ReturenStartBallCurCoin
    {
        get { return upgradeStartBallCoins[StartBall - 2]; }
    }
    public int ReturenFireBallCurCoin
    {
        get { return upgradeFireBallCoins[FirePower - 2]; }
    }

    public int BallSprite 
    {
        get {return PlayerPrefsElite.GetInt("BallSprite");}
        set{PlayerPrefsElite.SetInt("BallSprite" , value);}
    }

    public int PlaneSprite
    {
        get {return PlayerPrefsElite.GetInt("PlaneSprite");}
        set{PlayerPrefsElite.SetInt("PlaneSprite" , value);}
    }

}
