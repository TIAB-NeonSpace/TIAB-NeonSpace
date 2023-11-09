using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEditor.Presets;

// gameObject.transform.childCount << 이것에 용도는 내 자식 오브젝트의 개수를 가져옵니다.
public class BrickCount : MonoBehaviour
{
    [SerializeField] Brick[] bricks_;
    [SerializeField] Items[] items_;
    int rint;
    public List<int> brickInt;          //현재 해당 블록카운트 
    public List<int> itemInt;           //현재 아이템 종류
    public List<int> brickSpecial;
    [SerializeField] int brick_idx_;    //블록 줄 index;
    [SerializeField] TweenPosition tPos;
    int checkingcount;
    int currentMutiple = 5;

    public void Set(GameObject[] array_Obj)
    {
        for (int i = 0; i < bricks_.Length; i++)
        {
            bricks_[i].Set(array_Obj);
        }
    }

    private void OnEnable()
    {
        if (!DataManager.instance.GetSaveFile())
        {
            DataManager.instance.SetBrickPos(brick_idx_, new Vector2(10000, 10000));
            DataManager.instance.SetSaveBlock(brickInt, brick_idx_);
            DataManager.instance.SetSaveItems(itemInt, brick_idx_);
            transform.position = new Vector2(10000, 10000);
        }
    }

    public void SetBrick() // 여기서 블럭을 키는 모든 역활을 합니다.
    {
        //int itemIdx = myItemSlotList.FindIndex(t => t.name == item.name);
        //Extensions.ShuffleCopy(bricks_, items_); //랜덤하게 섞음
        if (BrickManager.instance.brickCount > 99)// 여기서 랜덤값을 뽑아 옵니다.^_^ 
        {
            rint = Random.Range(4, bricks_.Length);
        }
        else rint = Random.Range(1, bricks_.Length);

        FalseBrick();

        int active_Block_Amount = 0;
        int random_Block_Pos = 0;
        while (active_Block_Amount < rint)
        {
            random_Block_Pos = Random.Range(0, bricks_.Length);
            if (bricks_[random_Block_Pos].gameObject.activeSelf)
                continue;
            bricks_[random_Block_Pos].gameObject.SetActive(true); // 여기서 켜줍니다. 누구를? 내자식오브젝트들을 켜줍니다^_^ 
            ++checkingcount;
            if (BrickManager.instance.brickCount % 100 == 0)
            {
                ++currentMutiple;
            }
            if (BrickManager.instance.brickCount >= 10)
            {
                int r_ = Random.Range(0, 10);
                if (r_ < 5) bricks_[random_Block_Pos].hitCnt = BrickManager.instance.brickCount * currentMutiple; // 여기서는 현재 스테이지의 맞아야할 횟수를 5배로 올려주는 겁니다.
                else bricks_[random_Block_Pos].hitCnt = BrickManager.instance.brickCount; // 여기서는 현재 스테이지의 맞아야할 횟수를 정해 주는 겁니다.
            }
            else bricks_[random_Block_Pos].hitCnt = BrickManager.instance.brickCount; // 여기서는 현재 스테이지의 맞아야할 횟수를 정해 주는 겁니다.

            bricks_[random_Block_Pos].LabelSetting(); // 여기서 타이밍 에러가 생기지 않도록 라벨을 보여주는 곳을 때려줍니다.
            bricks_[random_Block_Pos].SetNext(); //색을 담당하는 부분입니다.

            active_Block_Amount++;
        }
        //for (int i = 0; i < rint; ++i) // 여기서 랜덤값만큼 열어 줍니다.
        //{
        //    //int random_Block = Random.Range(1, bricks_.Length);
        //    bricks_[i].gameObject.SetActive(true); // 여기서 켜줍니다. 누구를? 내자식오브젝트들을 켜줍니다^_^ 
        //    ++checkingcount;
        //    if (BrickManager.instance.brickCount % 100 == 0)
        //    {
        //        ++currentMutiple;
        //    }
        //    if (BrickManager.instance.brickCount >= 10)
        //    {
        //        int r_ = Random.Range(0, 10);
        //        if (r_ < 5) bricks_[i].hitCnt = BrickManager.instance.brickCount * currentMutiple; // 여기서는 현재 스테이지의 맞아야할 횟수를 5배로 올려주는 겁니다.
        //        else bricks_[i].hitCnt = BrickManager.instance.brickCount; // 여기서는 현재 스테이지의 맞아야할 횟수를 정해 주는 겁니다.
        //    }
        //    else bricks_[i].hitCnt = BrickManager.instance.brickCount; // 여기서는 현재 스테이지의 맞아야할 횟수를 정해 주는 겁니다.

        //    bricks_[i].LabelSetting(); // 여기서 타이밍 에러가 생기지 않도록 라벨을 보여주는 곳을 때려줍니다.
        //    bricks_[i].SetNext(); //색을 담당하는 부분입니다.
        //}
        SetItem();
        SetSaveBlock();
    }

    public void SetSaveBlock()
    {
        DataManager.instance.SetSaveBlock(brickInt, brick_idx_);
        DataManager.instance.SetSaveSpecial(brickSpecial, brick_idx_);
    }

    void SetSaveItem()
    {
        DataManager.instance.SetSaveItems(itemInt, brick_idx_);
    }

    //블록 모양 결정
    public void ShowSaveBrick()
    {
        FalseBrick();   //초기화
        for (int i = 0; i < DataManager.instance.GetSaveBlock(brick_idx_).Count; ++i)
        {
            if (DataManager.instance.GetSaveBlock(brick_idx_)[i] > 0)
            {
                bricks_[i].gameObject.SetActive(true);
                bricks_[i].hitCnt = DataManager.instance.GetSaveBlock(brick_idx_)[i];
                bricks_[i].LabelSetting();
            }
            if (DataManager.instance.GetSaveSpecial(brick_idx_)[i] > 0)
            {
                bricks_[i].SetSpecialBrick(DataManager.instance.GetSaveSpecial(brick_idx_)[i]);
            }
        }
        transform.localPosition = DataManager.instance.GetBrickPos(brick_idx_);
        ShowSaveItem();
    }

    void ShowSaveItem()
    {
        for (int i = 0; i < items_.Length; ++i) // 혹시나 하는 마음에..
        {
            items_[i].gameObject.SetActive(false); // 아이템 오브젝트들을 꺼줍니다. 일단 한번
        }

        for (int i = 0; i < DataManager.instance.GetSaveItems(brick_idx_).Count; ++i) // 혹시나 하는 마음에..
        {
            if (DataManager.instance.GetSaveItems(brick_idx_)[i] > -1)
            {
                items_[i].gameObject.SetActive(true);
                items_[i].SetItemType((Items.ItemInfo)DataManager.instance.GetSaveItems(brick_idx_)[i]);
            }
        }
    }

    void SetItem()
    {
        //for (int i = 0; i < items_.Length; ++i) // 혹시나 하는 마음에..
        //{
        //    items_[i].gameObject.SetActive(false); // 아이템 오브젝트들을 꺼줍니다. 일단 한번
        //}

        //생성 우선순위를 아이템을 먼저로 함

        int itemPos_Ball = RandomItem_Pos();
        if (BrickManager.instance.brickCount == 1)
        {
            items_[itemPos_Ball].gameObject.SetActive(true);
            items_[itemPos_Ball].SetItemType(Items.ItemInfo.Ball); // ball item을 생성 해주는 곳 여기서 처음에 공하나 준다.
        }
        else if (BrickManager.instance.brickCount % 10 == 0)
        {
            items_[itemPos_Ball].gameObject.SetActive(true);
            items_[itemPos_Ball].SetItemType(Items.ItemInfo.Ball); // ball item을 생성 해주는 곳 여기서 5판 마다 공을 한개씩 준다.
        }

        //0~100
        int randomItem = Random.Range(0, 100);
        int itemPos_Other = RandomItem_Pos();
        if (randomItem > 39)
        {
            items_[itemPos_Other].gameObject.SetActive(true);
            items_[itemPos_Other].SetItemType(Items.ItemInfo.Coin);
        }
        else
        {
            for (int i = 0; i < bricks_.Length - 1; ++i)
            {
                if (!bricks_[i].gameObject.activeSelf)
                {
                    int rnd = Random.Range(0, 10);
                    switch (rnd)
                    {
                        case 0:
                            items_[itemPos_Other].gameObject.SetActive(true);
                            items_[itemPos_Other].SetItemType(Items.ItemInfo.Width);
                            break;
                        case 1:
                            items_[itemPos_Other].gameObject.SetActive(true);
                            items_[itemPos_Other].SetItemType(Items.ItemInfo.Height);
                            break;
                        case 2:
                            items_[itemPos_Other].gameObject.SetActive(true);
                            items_[itemPos_Other].SetItemType(Items.ItemInfo.Cross);
                            break;
                        case 3:
                            items_[itemPos_Other].gameObject.SetActive(true);
                            items_[itemPos_Other].SetItemType(Items.ItemInfo.Xcross);
                            break;
                        case 4:
                            items_[itemPos_Other].gameObject.SetActive(true);
                            items_[itemPos_Other].SetItemType(Items.ItemInfo.PingPong);
                            break;
                        case 5:
                            items_[itemPos_Other].gameObject.SetActive(true);
                            items_[itemPos_Other].SetItemType(Items.ItemInfo.UFO);
                            break;
                    }
                }
            }
        }


        SetSaveItem();
    }

    private int RandomItem_Pos()
    {
        int randomItem_Pos;
        while (true)
        {
            randomItem_Pos = Random.Range(0, items_.Length);
            if (items_[randomItem_Pos].gameObject.activeSelf)
                continue;
            break;
        }

        if (bricks_[randomItem_Pos].gameObject.activeSelf)
        {
            bricks_[randomItem_Pos].DisableMySelf();
        }

        return randomItem_Pos;
    }

    public void MovingPos(float startF, float endF) //트윈 포지션을 사용해서 다음 포지션으로 이동을 시킨다.
    {
        //if(tPos == null) tPos = GetComponent<TweenPosition>();
        tPos.from.y = startF;
        tPos.to.y = endF;
        tPos.ResetToBeginning();
        tPos.PlayForward();
        FalseItemAll();
        DataManager.instance.SetBrickPos(brick_idx_, new Vector2(0, endF));
        SetSaveBlock();
        SetSaveItem();
    }

    public void FalseItemAll()
    {
        for (int i = 0; i < items_.Length; ++i)
        {
            items_[i].FalseMy();
            bricks_[i].IncreseBrick();
        }
    }
    public void FalseBrick()
    {
        for (int i = 0; i < bricks_.Length; ++i) // 혹시나 하는 마음에..
        {
            bricks_[i].hitCnt = 0;
            bricks_[i].LabelSetting();
            bricks_[i].gameObject.SetActive(false); // 내 자식 오브젝트들을 꺼줍니다. 일단 한번
        }

        for (int i = 0; i < items_.Length; ++i) // 혹시나 하는 마음에..
        {
            items_[i].gameObject.SetActive(false); // 아이템 오브젝트들을 꺼줍니다. 일단 한번
        }
    }

    public void OnMoreCheck()
    {
        for (int i = 0; i < bricks_.Length; ++i) // 혹시나 하는 마음에..
        {
            bricks_[i].hitCnt = 0;
            bricks_[i].LabelSetting();
            bricks_[i].gameObject.SetActive(false); // 내 자식 오브젝트들을 꺼줍니다. 일단 한번
        }

        SetSaveBlock();
    }

    public void CheckingAllCelar()
    {
        --checkingcount;
    }

    //민진-new. bricks_[random]이 활성화되어있다면 반환
    public List<Brick> RandActiveBrick(int n)
    {
        int cnt = n;
        List<Brick> activeBricks = new List<Brick>();

        for(int i=0; i<bricks_.Length; i++)
        {
            if (cnt <= 0)
            {
                break;
            }

            if (bricks_[i].gameObject.activeSelf)
            {
                cnt--;
                activeBricks.Add(bricks_[i]);
            }
        }

        return activeBricks;

    }
}


//현재 블록 생성과 아이템생성을 아예 다르게 처리하고있음
//아이템 생성조건을 토의후 수정하기