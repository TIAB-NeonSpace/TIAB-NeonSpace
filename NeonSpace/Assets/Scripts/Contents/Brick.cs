using UnityEngine;
using System.Collections;
using UnityEditor;

public class Brick : MonoBehaviour
{
    public int idx_;
    public int hitCnt; // 맞아야할 횟수
    [SerializeField]
    UILabel label_; // 횟수를 보여줄 label입니다^_^
    [SerializeField]
    UISprite[] sprites_; // 색깔을 바꿔주기 위해서 찾아 놓은 Sprite입니다^_^
    [SerializeField]
    TweenAlpha[] tweenA;
    bool isIncre;
    public bool isReset;
    public int brickStateCnt;
    int triRandomPiece, increaseCnt;
    [SerializeField]
    Vector3[] triPos;
    [SerializeField]
    Vector2[] labelPos;
    [SerializeField]
    Vector2[] shadowPos;
    BrickCount count_;
    [SerializeField]
    GameObject[] effects_;
    [SerializeField]
    ParticleSystem comboFx;

    GameObject _obj;
    public void Set(GameObject[] array_Obj)
    {
        for (int i = 0; i < array_Obj.Length; i++)
        {
            _obj = Instantiate(array_Obj[i], this.gameObject.transform);
            _obj.SetActive(false);
        }
    }

    public void LabelSetting() // 현재 나의 label을 셋팅해 줍니다.^_^ 
    {
        label_.text = hitCnt.ToString(); // 이건알죠?
        if (count_ == null) count_ = GetComponentInParent<BrickCount>();
        count_.brickInt[idx_] = hitCnt;
        BrickColor();
    }

    public void SetCollider(GameObject go) // 여기에 맞으면 때려줍니다.
    {
        if (go.CompareTag("Ball")) // 이것도 또한 유니티 내부 함수입니다.
        {
            int scores_;
            hitCnt -= BallManager.instance.ballHit; // 이거는 나의 맞아야할 횟수를 깎아줍니다. 제너릭하다고 봐주세요~
            if (hitCnt < 1) scores_ = hitCnt + BallManager.instance.ballHit;
            else scores_ = BallManager.instance.ballHit;
            DataManager.instance.CurrentScore += scores_;
            UIManager.instance.SetScore();
            tweenA[brickStateCnt].ResetToBeginning(); // 이건 깜박거리는 스프라이트 함수입니다.
            tweenA[brickStateCnt].PlayForward();
            go.GetComponent<BallMove>().ball_Pos_ = Vector2.one;
            go.layer = 8;
            ///<summary>
            /// add combo count
            /// </summary>

            //comboFx.Play();
            UIManager.instance.CountCombo(); // 프리펩화 한 콤보매니저를 하이라키에 넣고 ComboManager.cs 연결하면댐
            UIManager.instance.UpdateComboCount();

            if (hitCnt <= 0) // 암튼 그래~ 장그래~
            {
                hitCnt = 0;
                count_.CheckingAllCelar();
                Invoke("FalseMySelf", 0.05f);
            }
            else
            {
                LabelSetting(); // 라벨을 보여주기 위해서
            }
            SoundManager.instance.ChangeEffects(1, 0.7f);
        }
    }
    public void alphaOn() // 알파를 켜줍니다.
    {
        tweenA[brickStateCnt].ResetToBeginning();
        tweenA[brickStateCnt].PlayForward();
    }

    public void FalseMySelf() // 여기는 꺼주는 역활을 합니다.
    {
        //SoundManager.instance.SetClipAtPoint(0, Vector3.zero);
        if (brickStateCnt == 3)
            BrickManager.instance.isTimer = false;
        isReset = true;
        count_.brickInt[idx_] = 0;
        count_.brickSpecial[idx_] = 0;
        Instantiate(effects_[brickStateCnt], transform.position, new Quaternion());
        gameObject.SetActive(false);
    }

    public void DisableMySelf()
    {
        isReset = true;
        count_.brickInt[idx_] = 0;
        count_.brickSpecial[idx_] = 0;
        gameObject.SetActive(false);
    }

    //매 라운드마다
    public void SetNext() // 이거는 나의 색상을 스테이지 마다 변화를 시켜주기 위해서 존재 합니다.^_^ 뿌잉
    {
        if (isReset)
        {
            ResetBrick();
            isReset = false;
        }
        if (isIncre)
        {
            sprites_[0].color = Color.white;
            sprites_[0].spriteName = "Sprite_Brick07";
        }
        else
        {
            sprites_[0].spriteName = "Sprite_Brick02";
        }
    }

    void BrickColor()
    {
        if (isIncre || hitCnt == 0) return;
        float f = (float)hitCnt / BrickManager.instance.brickCount;
        string c = "";
        if (brickStateCnt == 0)
        {
            if (f > 0.9f) c = "Sprite_Brick05";
            if (f <= 0.9f) c = "Sprite_Brick06";
            if (f <= 0.75f) c = "Sprite_Brick04";
            if (f <= 0.5f) c = "Sprite_Brick03";
            if (f <= 0.25f) c = "Sprite_Brick01";
            if (f <= 0.1f) c = "Sprite_Brick02";
            if (f > 1) c = "Sprite_Brick00";

            sprites_[brickStateCnt].spriteName = c;
        }
        else if (brickStateCnt == 1)
        {
            if (f > 0.9f) c = "Sprite_Tri05";
            if (f <= 0.9f) c = "Sprite_Tri06";
            if (f <= 0.75f) c = "Sprite_Tri04";
            if (f <= 0.5f) c = "Sprite_Tri03";
            if (f <= 0.25f) c = "Sprite_Tri01";
            if (f <= 0.1f) c = "Sprite_Tri02";
            if (f > 1) c = "Sprite_Tri00";

            sprites_[brickStateCnt].spriteName = c;
        }


        //sprites_[brickStateCnt].color = NGUIText.ParseColor(c); // 이거는 ngui함수입니다. 필요하시면 읽어보세요!

    }

    public void IncreseBrick()
    {
        if (isIncre && hitCnt > 0)
        {
            hitCnt += BrickManager.instance.increseCnt;
            LabelSetting();
        }
        BrickColor();
    }

    /*SpecialBrick index
     * 0=비활성화
     * 1=기본
     * 2~5=삼각
     * 6=쉴드
     * 7=타이머
     * 8=반사
     * 9=직접
     */
    public void SetSpecialBrick(int i)
    {
        count_.brickSpecial[idx_] = i;
        sprites_[0].gameObject.SetActive(false);
        sprites_[1].gameObject.SetActive(false);
        sprites_[2].gameObject.SetActive(false);
        sprites_[3].gameObject.SetActive(false);
        sprites_[4].gameObject.SetActive(false);
        sprites_[5].gameObject.SetActive(false);
        isIncre = false;

        //if (i > 1 && i < 6)
        //{
        //    brickStateCnt = 1;
        //    sprites_[1].gameObject.SetActive(true);
        //    sprites_[1].gameObject.transform.localEulerAngles = triPos[i - 2];
        //    label_.gameObject.transform.localPosition = labelPos[i - 2];
        //}
        switch (i)
        {
            case 0:
                break;
            case 1:
                isIncre = true;
                sprites_[0].gameObject.SetActive(true);
                sprites_[0].color = Color.white;
                sprites_[0].spriteName = "Sprite_Brick07";
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                brickStateCnt = 1;
                sprites_[1].gameObject.SetActive(true);
                sprites_[1].gameObject.transform.localEulerAngles = triPos[i - 2];
                label_.gameObject.transform.localPosition = labelPos[i - 2];
                break;
            case 6: //쉴드
                brickStateCnt = 2;
                sprites_[2].gameObject.SetActive(true);
                break;
            case 7: //타이머
                brickStateCnt = 3;
                sprites_[3].gameObject.SetActive(true);
                BrickManager.instance.isTimer = true;
                break;
            case 8://반사
                brickStateCnt = 4;
                sprites_[4].gameObject.SetActive(true);
                label_.gameObject.transform.localPosition = labelPos[4];
                break;
            case 9://직접
                brickStateCnt = 5;
                sprites_[5].gameObject.SetActive(true);
                label_.gameObject.transform.localPosition = labelPos[4];
                break;
        }
        BrickColor();
    }

    void ResetBrick()
    {
        count_.brickSpecial[idx_] = 0;
        int Special_Random = Random.Range(0, 101);
        isIncre = false;

        sprites_[0].gameObject.SetActive(false);
        sprites_[1].gameObject.SetActive(false);
        sprites_[2].gameObject.SetActive(false);
        sprites_[3].gameObject.SetActive(false);
        sprites_[4].gameObject.SetActive(false);
        sprites_[5].gameObject.SetActive(false);


        if (Special_Random < 4)
        {
            triRandomPiece = Random.Range(0, 4);
            sprites_[1].gameObject.SetActive(true);
            brickStateCnt = 1;
            sprites_[brickStateCnt].gameObject.transform.localEulerAngles = triPos[triRandomPiece];
            label_.gameObject.transform.localPosition = labelPos[triRandomPiece];
            count_.brickSpecial[idx_] = triRandomPiece + 2;
        }
        else if (Special_Random < 8)
        {
            sprites_[2].gameObject.SetActive(true);
            brickStateCnt = 2;
            label_.transform.localPosition = Vector2.zero;
            count_.brickSpecial[idx_] = 6;
        }
        else if (Special_Random < 12 && !BrickManager.instance.isTimer)
        {
            sprites_[3].gameObject.SetActive(true);
            brickStateCnt = 3;
            label_.transform.localPosition = Vector2.zero;
            count_.brickSpecial[idx_] = 7;
            BrickManager.instance.isTimer = true;
        }
        else if (Special_Random < 16)
        {
            sprites_[4].gameObject.SetActive(true);
            brickStateCnt = 4;
            label_.gameObject.transform.localPosition = labelPos[4];
            count_.brickSpecial[idx_] = 8;
        }
        else if (Special_Random < 20)
        {
            sprites_[5].gameObject.SetActive(true);
            brickStateCnt = 5;
            label_.gameObject.transform.localPosition = labelPos[4];
            count_.brickSpecial[idx_] = 9;
        }


        else // if(triRandom<81)
        {
            increaseCnt = Random.Range(0, 101);
            if (increaseCnt < 11)
            {
                count_.brickSpecial[idx_] = 1;
                isIncre = true;
            }
            brickStateCnt = 0;
            sprites_[0].gameObject.SetActive(true);
            label_.transform.localPosition = Vector2.zero;
        }
        /*else
        {
            brickStateCnt = 2;
            sprites_[0].gameObject.SetActive(false);
            sprites_[1].gameObject.SetActive(false);
            sprites_[2].gameObject.SetActive(true);

        }*/

    }

    public void HitCnt_Item()
    {
        hitCnt--;
        if (hitCnt <= 0)
            FalseMySelf();
        else
            LabelSetting();
    }

    //민진 new - (ufo) 블록의 카운트를 0으로 만듭니다.
    public void MakeZero()
    {
        hitCnt = 0;
        if (count_ == null) count_ = GetComponentInParent<BrickCount>();
        count_.CheckingAllCelar();
        Invoke("FalseMySelf", 0.05f);
    }
}
