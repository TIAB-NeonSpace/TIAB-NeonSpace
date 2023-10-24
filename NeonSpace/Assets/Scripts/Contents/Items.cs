using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour
{
    public enum ItemInfo
    {
        Ball = 0,
        Width = 1,
        Height = 2,
        Cross = 3,
        Xcross = 4,
        PingPong = 5,
        Coin = 6,
        Balls = 7,
        None = -1
    }
    public ItemInfo itemType;
    [SerializeField]
    UISprite sprite_;//, shadowSprite_ = null;
    [SerializeField]
    int idx_;
    string spriteName_ ;
    bool isFalse;
    [SerializeField]
    bool isToong;

    BrickCount counts_;

    public void SetItemType(ItemInfo type_)
    {
        counts_ = GetComponentInParent<BrickCount>();
        itemType = type_;
        isFalse = false;
        counts_.itemInt[idx_] = (int)type_;
        switch (type_) // 여기는 Sprite만 설정해주세요
        {
            case ItemInfo.Ball:
                spriteName_ = "Sprite_Ball+1";
                break;
            case ItemInfo.Balls:
                spriteName_ = "Sprite_Ball+1";
                break;
            case ItemInfo.Width:
                spriteName_ = "Sprite_Hor";
                break;
            case ItemInfo.Height:
                spriteName_ = "Sprite_Ver";
                break;
            case ItemInfo.Cross:
                spriteName_ = "Sprite_Cross";
                break;
            case ItemInfo.Xcross:
                spriteName_ = "Sprite_XCross";
                break;
            case ItemInfo.PingPong:
                spriteName_ = "Sprite_Bounce";
                break;
            case ItemInfo.Coin:
                spriteName_ = "Icon_Coin";
                break;
        }
        sprite_.spriteName = spriteName_;
    }

    public void FalseMy()
    {
        if (isFalse)
        {
            itemType = ItemInfo.None;
            counts_.itemInt[idx_] = -1;
            gameObject.SetActive(false);
        }
    }

    public void BottomTrue()
    {
        itemType = ItemInfo.None;
        counts_.itemInt[idx_] = -1;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D cd)
    {
        if(cd.gameObject.CompareTag("Ball"))
        {
            isFalse = true;
            switch (itemType)
            {
                case ItemInfo.Ball:
                    SoundManager.instance.ChangeEffects(4);
                    BallManager.instance.PlusBall(1);
                    FalseMy();
                    break;
                case ItemInfo.Balls:
                    BallManager.instance.PlusBall(10);
                    FalseMy();
                    break;
                case ItemInfo.Width:
                    SoundManager.instance.ChangeEffects(2);
                    GameContents.instance.gameitem(gameObject.transform.position ,0);
                    break;
                case ItemInfo.Height:
                    SoundManager.instance.ChangeEffects(2);
                    GameContents.instance.gameitem(gameObject.transform.position, 1);
                    break;
                case ItemInfo.Cross:
                    SoundManager.instance.ChangeEffects(2);
                    GameContents.instance.gameitem(gameObject.transform.position, 2);
                    break;
                case ItemInfo.Xcross:
                    SoundManager.instance.ChangeEffects(2);
                    GameContents.instance.gameitem(gameObject.transform.position, 3);
                    break;
                case ItemInfo.Coin:
                    SoundManager.instance.ChangeEffects(4);
                    DataManager.instance.SetCoin(1);
                    UIManager.instance.SetMoney();
                    gameObject.SetActive(false);
                    break;
                case ItemInfo.PingPong:
                    SoundManager.instance.ChangeEffects(3);
                    cd.gameObject.GetComponent<BallMove>().ball_Pos_ = cd.transform.position;
                    GameContents.instance.pingpongItem(cd.gameObject);
                    break;
            }
        }
    }

}
