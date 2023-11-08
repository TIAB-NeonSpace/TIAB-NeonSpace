using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickManager : MonoBehaviour
{
    public static BrickManager instance;
    [SerializeField] List<BrickCount> bundleList;
    [SerializeField] public List<BrickCount> bundleObjList;
    public int brickCount, moreCnt_ , increseCnt = 2;
    [SerializeField] int counts_;
    bool isOver;
    public bool isOneMore;
    public BrickCount lastMoreCheck;
    [SerializeField] PingpongItem[] item_;

    public GameObject[] obj_Special_Brick;

    void Awake()
    {
        instance = this;
        for (int i = 0; i < bundleList.Count; i++)
        {
            bundleList[i].Set(obj_Special_Brick);
        }
    }

    private void OnEnable()
    {
        Invoke("DelayStart", 0.1f);  
    }

    void DelayStart()
    {
        SetUpCount(DataManager.instance.GetSaveFile());
    }

    public void Init()
    {
        brickCount = 0;
        counts_ = 0;
    }

    public void SetUpCount(bool isBool = false)
    {
        if(isBool)
        {
            brickCount = DataManager.instance.GetSaveBrickCount();
            BallManager.instance.SetSaveFileBall();
            counts_ = DataManager.instance.GetSaveBrickPosCnt();
            int forCnt = 0;
            isOver = DataManager.instance.GetSaveBrickBool();
            isOneMore = DataManager.instance.GetOneMoreBool();
            moreCnt_ = DataManager.instance.GetSaveOnemoreCnt();
            if (DataManager.instance.GetSaveBrickBool())
            {
                forCnt = 9;
            }
            else
            {
                forCnt = counts_;
            }
            for (int i = 0; i < forCnt; ++i)
            {
                bundleObjList.Add(bundleList[i]);
                bundleList[i].ShowSaveBrick();
            }
        }
        else
        {
            if (counts_ > 8)
            {
                counts_ = 0;
                isOver = true;
            }
            brickCount += 1;
            bundleList[counts_].SetBrick();
            bundleList[counts_].MovingPos(420, 326);

            for (int i = 0; i < bundleObjList.Count; ++i)
            {
                if (bundleObjList[i] != bundleList[counts_])
                {
                    bundleObjList[i].MovingPos(bundleObjList[i].transform.localPosition.y, bundleObjList[i].transform.localPosition.y - 95);
                }
            }
            if (!isOver)
            {
                bundleObjList.Add(bundleList[counts_]);
            }
            ++counts_;
            if (brickCount % 50 == 0) ++increseCnt;
            DataManager.instance.SetSaveBrickPosCnt(counts_);
            DataManager.instance.SetSaveBrickBool(isOver);
            FalsePingpongItem();
            if (DataManager.instance.CurrentScore > DataManager.instance.GetBestScore())
            {
                /*switch(brickCount) // 여기는 나중에 구글 연결되면 풀어주면 됩니다.
                {
                    // case 100:
                    //     if(DataManager.instance.GetSaveAchive(0) == 0)
                    //     {
                    //         GPGSManager.instance.setAchievements(2);
                    //         DataManager.instance.SetSaveAchive(0);
                    //     }
                    //     break;
                    // case 200:
                    //     if (DataManager.instance.GetSaveAchive(1) == 0)
                    //     {
                    //         GPGSManager.instance.setAchievements(3);
                    //         DataManager.instance.SetSaveAchive(1);
                    //     }
                    //     break;
                    // case 300:
                    //     if (DataManager.instance.GetSaveAchive(2) == 0)
                    //     {
                    //         GPGSManager.instance.setAchievements(4);
                    //         DataManager.instance.SetSaveAchive(2);
                    //     }
                    //     break;
                }*/
                DataManager.instance.SetBestScore();
                UIManager.instance.SetBestScore();
            }
        }
        //UIManager.instance.SetScore(brickCount.ToString());
    }

    int bgCnt , bgChange;
    void FalsePingpongItem()
    {
        for(int i = 0; i < item_.Length; ++i)
        {
            item_[i].SetPos(false, new Vector2(10000, 0));
        }
    }
    public void SetPosPingpong(float pos)
    {
        item_[0].SetPos(true, new Vector2(0, pos));
    }

    public void SetSaveSkillBrick()
    {
        for(int i = 0; i < bundleList.Count; ++i)
        {
            bundleList[i].SetSaveBlock();
        }
    }

    public void OneMoreChance()
    {
        if (!isOneMore)
        {
            AdsManager.instance.ShowRewardedAd(1);
        }
    }

    public void OnemoreSet()
    {
        for (int i = 0; i < bundleList.Count; ++i)
        {
            if (bundleList[i] == lastMoreCheck) bundleList[i].OnMoreCheck();
        }
        lastMoreCheck = null;
        DataManager.instance.SetSaveFile(true);
        isOneMore = true;
        DataManager.instance.SetOnemore(isOneMore);
        UIManager.instance.ResultOneMoreBtnDelete();
        LobbyController.instance.SetTween(EnumBase.UIState.Result , false);
    }

    //매개변수 값 만큼의 랜덤한 활성화 벽돌을 배열로 반환
    public List<Brick> FindRandActiveBrick(int n)
    {
        int cnt = n;
        List<Brick> activeBricks = new List<Brick>();
        List<Brick> tmpList = new List<Brick>();

        int repeat = counts_;
        if (isOver)
        {
            repeat = 8;
        }
        for(int i= 0; i<=repeat; i++)
        {
            tmpList = bundleList[i].RandActiveBrick(cnt);
            if (tmpList.Count > 0)
            {
                activeBricks.AddRange(tmpList);
                cnt -= activeBricks.Count;
                if (activeBricks.Count >= n)
                {
                    break;
                }

            }

        }
        return activeBricks;
    }
    //민진 new - 블록의 카운트를 0으로 만드는 메소드
    public void BrickCntZero(Brick brick)
    {
        brick.MakeZero();
    }
}
