using UnityEngine;
using System.Collections;

public class LobbyTween : MonoBehaviour
{
    public EnumBase.UIState state_;
    Animator anim_;
    public bool isAnimTween;
    TweenScale ts_;
    void Start()
    {
        anim_ = GetComponent<Animator>();
        if(anim_ != null) isAnimTween = true;
    }

    private void OnEnable() {
        if(ts_ == null) ts_ = GetComponent<TweenScale>();
        if(ts_!= null)
        {
            ts_.ResetToBeginning();
            ts_.PlayForward();
        }
    }

    public void SetAnimPlay(bool isTrue)
    {
        if(anim_ != null)
        {
            if(isTrue) anim_.SetTrigger("On");
            else anim_.SetTrigger("Off");
        }
        else 
        {
            
        }
    }
    
}
