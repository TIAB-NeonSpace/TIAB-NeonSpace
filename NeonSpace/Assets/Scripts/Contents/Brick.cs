using UnityEngine;
using System.Collections;

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
    bool isIncre , isReset;
    int brickStateCnt,triRandomPiece, increaseCnt;
    [SerializeField]
    Vector3[] triPos;
    [SerializeField]
    Vector2[] labelPos;
    [SerializeField]
    Vector2[] shadowPos;
    BrickCount count_;
    [SerializeField]
    GameObject[] effects_;

    public void LabelSetting() // 현재 나의 label을 셋팅해 줍니다.^_^ 
    {
        label_.text = hitCnt.ToString(); // 이건알죠?
        if(count_ == null) count_ = GetComponentInParent<BrickCount>();
        count_.brickInt[idx_] = hitCnt;
        BrickColor();
    }

    public void SetCollider(GameObject go) // 여기에 맞으면 때려줍니다.
    {
        if (go.CompareTag("Ball")) // 이것도 또한 유니티 내부 함수입니다.
        {
            int scores_;
            hitCnt -= BallManager.instance.ballHit; // 이거는 나의 맞아야할 횟수를 깎아줍니다. 제너릭하다고 봐주세요~
            if(hitCnt < 1 ) scores_ = hitCnt + BallManager.instance.ballHit;
            else scores_ = BallManager.instance.ballHit;
            DataManager.instance.CurrentScore += scores_;
            UIManager.instance.SetScore();
            tweenA[brickStateCnt].ResetToBeginning(); // 이건 깜박거리는 스프라이트 함수입니다.
            tweenA[brickStateCnt].PlayForward();
            go.GetComponent<BallMove>().ball_Pos_ = Vector2.one;

            ///<summary>
            /// add combo count
            /// </summary>

            // int comboCount = 0; 
            //ComboManager.instance.CountCombo();

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
            SoundManager.instance.ChangeEffects(1 , 0.7f);
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
        isReset = true;
        count_.brickInt[idx_] = 0;
        count_.brickSpecial[idx_] = 0;
        Instantiate(effects_[brickStateCnt], transform.position, new Quaternion());
        gameObject.SetActive(false);
    }

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
        }
        else
        {
            if (f > 0.9f) c = "Sprite_Tri05";
            if (f <= 0.9f) c = "Sprite_Tri06";
            if (f <= 0.75f) c = "Sprite_Tri04";
            if (f <= 0.5f) c = "Sprite_Tri03";
            if (f <= 0.25f) c = "Sprite_Tri01";
            if (f <= 0.1f) c = "Sprite_Tri02";
            if (f > 1) c = "Sprite_Tri00";
        }
      

        //sprites_[brickStateCnt].color = NGUIText.ParseColor(c); // 이거는 ngui함수입니다. 필요하시면 읽어보세요!
      
        sprites_[brickStateCnt].spriteName = c;
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

    public void SetSpecialBrick(int i)
    {
        count_.brickSpecial[idx_] = i;
        isIncre = false;
        if(i > 1)
        {
            brickStateCnt = 1;
            sprites_[0].gameObject.SetActive(false);
            sprites_[1].gameObject.SetActive(true);
            sprites_[1].gameObject.transform.localEulerAngles = triPos[i-2];
            label_.gameObject.transform.localPosition = labelPos[i - 2];
        }
        switch(i)
        {
            case 0:
                break;
            case 1:
                isIncre = true;
                sprites_[0].color = Color.white;
                sprites_[0].spriteName = "Sprite_Brick07";
                break;
        }
        BrickColor();
    }

    void ResetBrick()
    {
        count_.brickSpecial[idx_] = 0;
        int triRandom = Random.Range(0, 101);
        isIncre = false;
        if (triRandom < 21)
        {
            triRandomPiece = Random.Range(0, 4);
            sprites_[0].gameObject.SetActive(false);
            sprites_[1].gameObject.SetActive(true);
            brickStateCnt = 1;
            sprites_[brickStateCnt].gameObject.transform.localEulerAngles = triPos[triRandomPiece];
            label_.gameObject.transform.localPosition = labelPos[triRandomPiece];
            count_.brickSpecial[idx_] = triRandomPiece + 2;
        }
        else
        {
            increaseCnt = Random.Range(0, 101);
            if (increaseCnt < 11)
            {
                count_.brickSpecial[idx_] = 1;
                isIncre = true;
            }
            brickStateCnt = 0;
            sprites_[0].gameObject.SetActive(true);
            sprites_[1].gameObject.SetActive(false);
            label_.transform.localPosition = Vector2.zero;
        }

    }
}
