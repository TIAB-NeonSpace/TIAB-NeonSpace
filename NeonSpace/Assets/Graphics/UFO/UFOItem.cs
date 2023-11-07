using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UFOItem : MonoBehaviour
{
    [SerializeField] PostProcessVolume ppv; //메인 카메라에 붙어있는 post process volume 컴포넌트
    [SerializeField] GameObject UFO;

    // Start is called before the first frame update
    void Start()
    {
        //UFOStart();
    }

    //UFO 아이템 획득 시 실행
    public void UFOStart()
    {
        //1. 공 멈춤
        Time.timeScale = 0;
        //2. 회색조로 화면 전환
        ppv.weight = 1;
        //3. 일정시간 대기
        //4. UFO 활성화(등장 애니메이션 실행됨) --> UFOEventHandler.cs
        //Invoke("UFOShow", 0.3f);
        UFO.SetActive(true);
    }

    void UFOShow()
    {
        UFO.SetActive(true);
    }

    public void UFOAttack()
    {
        //UFO 공격 이펙트 실행
        //랜덤 블록 삭제
        List<Brick> bricks = new List<Brick>();
        bricks = BrickManager.instance.FindRandActiveBrick(3);
        for(int i=0; i<bricks.Count; i++)
        {
            BrickManager.instance.BrickCntZero(bricks[i]);
        }
    }

    public void UFOEnd()
    {
        //UFO 퇴장 애니메이션 실행 후 비활성화
        UFO.SetActive(false);
        ppv.weight = 0;
        Time.timeScale = 1;
    }
}
