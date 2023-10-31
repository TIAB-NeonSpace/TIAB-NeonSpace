using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenbundleManager : MonoBehaviour
{
    [SerializeField] LobbyTween[] bundleObj;

    //해당하는 오브젝트(화면) 활성/비활성화 처리
    public void ShowBundleObject(EnumBase.UIState state_ , bool isTrue)
    {
        for(int i = 0; i < bundleObj.Length; ++i)
            if (bundleObj[i].state_ == state_)
            {
                if(bundleObj[i].isAnimTween)bundleObj[i].SetAnimPlay(isTrue);
                else bundleObj[i].gameObject.SetActive(isTrue);
                
            } 
    }
}
