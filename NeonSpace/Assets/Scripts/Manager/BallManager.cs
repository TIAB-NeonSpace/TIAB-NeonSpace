using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    [SerializeField] float forceX;
    public float force;
    public List<BallMove> ballList;
    [HideInInspector] public bool isPlayGame;
    [SerializeField] GameObject ballInstant, bundleTrans, firstBall = null;
    public Vector3 ballPos;
    public int ballHit, ballY;
    [SerializeField] public int ballCnt, upCountBall, plusBallCnt, colorIdx;
    //[SerializeField] TimerBrick timerbrick;

    Vector3 pos;
    bool isArrow, isFire;
    float halfWidth, forceY, fireDelay = 0;
    BoxCollider2D box_;
    [SerializeField]
    int fireCnt = 0;

    void Awake()
    {
        instance = this;
        if (bundleTrans == null) bundleTrans = gameObject;
    }


    void Start()
    {
        GameBallset();
        box_ = GetComponent<BoxCollider2D>();
    }

    public void Init()
    {
        for (int i = 0; i < ballList.Count; ++i)
        {
            ballList[i].StopBall();
        }
    }

    void GameBallset()
    {
        GameObject go = Instantiate(ballInstant);
        go.transform.parent = bundleTrans.transform;
        go.transform.localScale = Vector2.one;
        go.transform.localPosition = new Vector2(0, -325);
        if (DataManager.instance.GetSaveFile())
        {
            go.transform.localPosition = new Vector2(0, -325);
            firstBall = go;
        }
        else  AddUpgradeBall();
       
    }

    void AddUpgradeBall()
    {
      
        plusBallCnt = DataManager.instance.StartBall - 1;
        for (int i = 0; i < plusBallCnt; ++i)
        {
            GameObject go = Instantiate(ballInstant);
            go.transform.parent = bundleTrans.transform;
            go.transform.localScale = Vector2.one;
            go.transform.localPosition = new Vector3(0, -325, 0);
        }
        ballCnt += plusBallCnt;
        UIManager.instance.SetBallCount(ballCnt);
        plusBallCnt = 0;
        System.GC.Collect(); // 이건 쓰레기 값을 버리는 코드 입니다. 이때는 잠시 멈출 테니까..
    
    }

    void OnPress(bool isBool)
    {
        if (isPlayGame) return;//|| LobbyController.instance.uiState != EnumBase.UIState.InGame) return;
        isArrow = isBool;

        if (isBool)
        {
            pos = UICamera.lastWorldPosition;//Input.mousePosition;
           
        }
        else
        {
            UIManager.instance.SetTouchSprite(new Vector3(1000, 1000, 0));
            if (forceY >= 100f)
            {
                isFire = true;
                isPlayGame = true;
                box_.enabled = false;
            }
        }
        UIManager.instance.SetSprite(isArrow, ballList[0].transform.localPosition);
    }

    public void ResetBall()
    {
        isFire = false;
        for (int i = 0; i < ballList.Count; ++i)
        {
            if(ballList[i].isMove)
            {
                ballList[i].StopBall(true);
            }
        }
        fireCnt = 0;
        isPlayGame = false;
        Invoke("SetFalseGame", 0.2f);
        LastSetting();
    }


    void FixedUpdate()
    {
        if (isFire)
        {
            /// <summary>
            /// Combo Start
            /// </summary>
            
            ComboManager.instance.ShowComboCount();
            fireDelay += Time.deltaTime * 1;
            if (fireDelay >= 0.1f)
            {
                WaitFires();
                fireDelay = 0;
            }
        }
        if (isPlayGame)
        {
            return;
        }
        if (isArrow)
        {
            Vector3 pos = UICamera.lastWorldPosition;
            Vector3 player_pos = ballList[0].transform.position;
            Vector2 mouse_pos = new Vector2(pos.x - player_pos.x, pos.y - player_pos.y);
            float rad = Mathf.Atan2(mouse_pos.x, mouse_pos.y);
            forceX = Mathf.Clamp((rad * 180) / Mathf.PI , -75,75);
            UIManager.instance.SetTouchSprite(UICamera.lastWorldPosition);
            forceY = Input.mousePosition.y;
            if (forceY >= 100f) UIManager.instance.ArrowSpriteSetting(true, -forceX);
            if (forceY < 100f) UIManager.instance.ArrowSpriteSetting(false, forceX);
            UIManager.instance.SetLineBallPos();
        }

    }

    void WaitFires()
    {
        if (fireCnt < ballList.Count)
        {
            ballList[fireCnt].SetMove(-forceX, force);
            UIManager.instance.SetBallCount(ballCnt - (fireCnt + 1));
            ++fireCnt;
        }
        else
        {
            isFire = false;
            fireCnt = 0;
        }
    }

    public void SetBall(GameObject ball)
    {
        if (firstBall == null) firstBall = ball;
        for (int i = 0; i < ballList.Count; ++i)
        {
            if (ballList[i].gameObject == ball) ballList[i].StopBall();
        }
        ++upCountBall;
        if (upCountBall == ballCnt)
        {
            isPlayGame = false;
            Invoke("SetFalseGame", 0.2f);
            LastSetting();
        }
    }
    void SetFalseGame() // invoke 함수
    {
        box_.enabled = true;
    }

    public void PlusBall(int i)
    {
        plusBallCnt += i;
    }
    public void SetSaveFileBall()
    {
        plusBallCnt = DataManager.instance.GetSaveBallCount() - 1; // datamanager에는 처음 볼까지 포함돼있기 때문에 한개를 빼준다.
        for (int i = 0; i < plusBallCnt; ++i)
        {
            GameObject go = Instantiate(ballInstant);
            go.transform.parent = bundleTrans.transform;
            go.transform.localScale = Vector2.one;
            go.transform.localPosition = new Vector3(0, -325, 0);
        }
        upCountBall = 0;
        ballCnt += plusBallCnt;
        UIManager.instance.SetBallCount(ballCnt);
        //UIManager.instance.SetLabelPos(plusBallCnt);
        firstBall = null;
        plusBallCnt = 0;
        System.GC.Collect();
    }

    public void LastSetting()
    {
        /// 공이 끝난다는것은 콤보가 더 안쌓일테니까 ComboManager.cs 에 있는 콤보 초기화
        //timerbrick.timechecking();
        ComboManager.instance.ResetCombo();
        ComboManager.instance.HideComboCount();

        DataManager.instance.SetSaveFile(true);

        for (int i = 0; i < plusBallCnt; ++i)
        {
            GameObject go = Instantiate(ballInstant);
            go.transform.parent = bundleTrans.transform;
            go.transform.localScale = Vector2.one;
            go.transform.localPosition = new Vector3(0, -325, 0);
        }
        upCountBall = 0;
        ballCnt += plusBallCnt;
        UIManager.instance.SetBallCount(ballCnt);
        //UIManager.instance.SetLabelPos(plusBallCnt);
        firstBall = null;
        plusBallCnt = 0;
        BrickManager.instance.SetUpCount();
        DataManager.instance.SetSaveBrickCount(BrickManager.instance.brickCount);
        DataManager.instance.SetSaveBallCount(ballCnt);
        System.GC.Collect(); // 이건 쓰레기 값을 버리는 코드 입니다. 이때는 잠시 멈출 테니까..
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
            coll.gameObject.layer = 8;
    }
}