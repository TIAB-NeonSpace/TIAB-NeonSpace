using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UFOItem : MonoBehaviour
{
    [SerializeField] PostProcessVolume ppv; //���� ī�޶� �پ��ִ� post process volume ������Ʈ
    [SerializeField] GameObject UFO;

    // Start is called before the first frame update
    void Start()
    {
        //UFOStart();
    }

    //UFO ������ ȹ�� �� ����
    public void UFOStart()
    {
        //1. �� ����
        Time.timeScale = 0;
        //2. ȸ������ ȭ�� ��ȯ
        ppv.weight = 1;
        //3. �����ð� ���
        //4. UFO Ȱ��ȭ(���� �ִϸ��̼� �����) --> UFOEventHandler.cs
        //Invoke("UFOShow", 0.3f);
        UFO.SetActive(true);
    }

    void UFOShow()
    {
        UFO.SetActive(true);
    }

    public void UFOAttack()
    {
        //UFO ���� ����Ʈ ����
        //���� ��� ����
        List<Brick> bricks = new List<Brick>();
        bricks = BrickManager.instance.FindRandActiveBrick(3);
        for(int i=0; i<bricks.Count; i++)
        {
            BrickManager.instance.BrickCntZero(bricks[i]);
        }
    }

    public void UFOEnd()
    {
        //UFO ���� �ִϸ��̼� ���� �� ��Ȱ��ȭ
        UFO.SetActive(false);
        ppv.weight = 0;
        Time.timeScale = 1;
    }
}
