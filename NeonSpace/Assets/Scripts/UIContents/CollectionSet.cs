using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//민진 new - collection onEnable시 실행. Collection 팝업창에서 잠금처리를 합니다.
public class CollectionSet : MonoBehaviour
{
    [SerializeField]
    GameObject[] RocketList = new GameObject[10]; //collection의 로켓 리스트 객체
    [SerializeField]
    GameObject[] BallList = new GameObject[10]; //collection의 공 리스트 객체

    GameObject[] RocketBlocker = new GameObject[10];         //blocker
    GameObject[] RocketSelectBlocker = new GameObject[10];   //선택 버튼 blocker

    GameObject[] BallBlocker = new GameObject[10];
    GameObject[] BallSelectBlocker = new GameObject[10];


    void Awake()
    {
        //자식 오브젝트에서 Blocker를 가져옵니다
        for(int i=0; i<RocketList.Length; i++)
        {
            RocketBlocker[i] = RocketList[i].transform.Find("Blocker").gameObject;
            RocketSelectBlocker[i] = RocketList[i].transform.Find("SelectBtnBlocker").gameObject;
        }
        for (int i = 0; i < BallList.Length; i++)
        {
            BallBlocker[i] = BallList[i].transform.Find("Blocker").gameObject;
            BallSelectBlocker[i] = BallList[i].transform.Find("SelectBtnBlocker").gameObject;

        }

        RocketChk();
        BallChk();
    }

    private void OnEnable()
    {
        RocketChk();
        BallChk();
    }


    //로켓 잠금체크 메소드
    public void RocketChk()
    {
        int chkNum = 1;
        //플레이어의 업그레이드 체크
        for (int i=0; i<RocketList.Length; i++)
        {
            //현재 power가 chkNum보다 크다면, --> 해금된 것
            if(DataManager.instance.FirePower >= chkNum)
            {
                //blocker 비활성화. null ReferenceException 오류가 뜨지만 작동에는 문제가 없다. 좀 더 살펴볼 예정
                RocketBlocker[i].SetActive(false);


                //플레이어의 현재 우주선 체크
                if (DataManager.instance.PlaneSprite != i)
                {
                    //blocker 비활성화
                    RocketSelectBlocker[i].SetActive(false);
                }
                else
                {
                    RocketSelectBlocker[i].SetActive(true);
                }
            }
            else
            {
                RocketSelectBlocker[i].SetActive(false);
                RocketBlocker[i].SetActive(true);
            }
            chkNum += 10;
        }
    }
    //공 잠금체크 메소드
    public void BallChk()
    {
        int chkNum = 1;
        //플레이어의 업그레이드 체크
        for (int i = 0; i < BallList.Length; i++)
        {
            //현재 start ball이 chkNum보다 크다면, --> 해금된 것
            if (DataManager.instance.StartBall >= chkNum)
            {
                //blocker 비활성화. null ReferenceException 오류가 뜨지만 작동에는 문제가 없다. 좀 더 살펴볼 예정
                BallBlocker[i].SetActive(false);


                //플레이어의 현재 공 스프라이트 체크
                if (DataManager.instance.BallSprite != i)
                {
                    //blocker 비활성화
                    BallSelectBlocker[i].SetActive(false);
                }
                else
                {
                    BallSelectBlocker[i].SetActive(true);
                }
            }
            else
            {
                BallSelectBlocker[i].SetActive(false);
                BallBlocker[i].SetActive(true);
            }
            chkNum += 10;
        }
    }
}
