using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� new - collection onEnable�� ����. Collection �˾�â���� ���ó���� �մϴ�.
public class CollectionSet : MonoBehaviour
{
    [SerializeField]
    GameObject[] RocketList = new GameObject[10]; //collection�� ���� ����Ʈ ��ü
    //[SerializeField]
    //GameObject[] BallList = new GameObject[10]; //collection�� �� ����Ʈ ��ü

    GameObject[] RocketBlocker = new GameObject[10];         //blocker
    GameObject[] RocketSelectBlocker = new GameObject[10];   //���� ��ư blocker

    GameObject[] BallBlocker = new GameObject[10];
    GameObject[] BallSelectBlocker = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        //�ڽ� ������Ʈ���� Blocker�� �����ɴϴ�
        for(int i=0; i<RocketList.Length; i++)
        {
            RocketBlocker[i] = RocketList[i].transform.Find("Blocker").gameObject;
            RocketSelectBlocker[i] = RocketList[i].transform.Find("SelectBtnBlocker").gameObject;
            
        }
/*        for (int i = 0; i < BallList.Length; i++)
        {
            BallBlocker[i] = RocketList[i].transform.Find("Blocker").gameObject;
            BallSelectBlocker[i] = RocketList[i].transform.Find("SelectBtnBlocker").gameObject;
        }*/
        RocketChk();
    }

    private void OnEnable()
    {
        RocketChk();
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
                //blocker ��Ȱ��ȭ
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
}
