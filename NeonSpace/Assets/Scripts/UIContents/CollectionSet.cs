using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� new - collection onEnable�� ����. Collection �˾�â���� ���ó���� �մϴ�.
public class CollectionSet : MonoBehaviour
{
    [SerializeField]
    GameObject[] RocketList = new GameObject[10]; //collection�� ���� ����Ʈ ��ü
    [SerializeField]
    GameObject[] BallList = new GameObject[10]; //collection�� �� ����Ʈ ��ü

    GameObject[] RocketBlocker = new GameObject[10];         //blocker
    GameObject[] RocketSelectBlocker = new GameObject[10];   //���� ��ư blocker

    GameObject[] BallBlocker = new GameObject[10];
    GameObject[] BallSelectBlocker = new GameObject[10];


    void Awake()
    {
        //�ڽ� ������Ʈ���� Blocker�� �����ɴϴ�
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


    //���� ���üũ �޼ҵ�
    public void RocketChk()
    {
        int chkNum = 1;
        //�÷��̾��� ���׷��̵� üũ
        for (int i=0; i<RocketList.Length; i++)
        {
            //���� power�� chkNum���� ũ�ٸ�, --> �رݵ� ��
            if(DataManager.instance.FirePower >= chkNum)
            {
                //blocker ��Ȱ��ȭ. null ReferenceException ������ ������ �۵����� ������ ����. �� �� ���캼 ����
                RocketBlocker[i].SetActive(false);


                //�÷��̾��� ���� ���ּ� üũ
                if (DataManager.instance.PlaneSprite != i)
                {
                    //blocker ��Ȱ��ȭ
                    RocketSelectBlocker[i].SetActive(false);
                }
                else
                {
                    RocketSelectBlocker[i].SetActive(true);
                }
            }
            else
            {
                RocketBlocker[i].SetActive(true);
            }
            chkNum += 10;
        }
    }
    //�� ���üũ �޼ҵ�
    public void BallChk()
    {
        int chkNum = 1;
        //�÷��̾��� ���׷��̵� üũ
        for (int i = 0; i < BallList.Length; i++)
        {
            //���� start ball�� chkNum���� ũ�ٸ�, --> �رݵ� ��
            if (DataManager.instance.StartBall >= chkNum)
            {
                //blocker ��Ȱ��ȭ. null ReferenceException ������ ������ �۵����� ������ ����. �� �� ���캼 ����
                BallBlocker[i].SetActive(false);


                //�÷��̾��� ���� �� ��������Ʈ üũ
                if (DataManager.instance.BallSprite != i)
                {
                    //blocker ��Ȱ��ȭ
                    BallSelectBlocker[i].SetActive(false);
                }
                else
                {
                    BallSelectBlocker[i].SetActive(true);
                }
            }
            else
            {
                BallBlocker[i].SetActive(true);
            }
            chkNum += 10;
        }
    }
}
