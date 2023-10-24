using UnityEngine;
using System.Collections;

public class LobbyLabel : MonoBehaviour
{
    UILabel label_;
    public EnumBase.UIState state_;

    void Start()
    {
        if(label_ == null)
        {
             LobbyController.instance.lobbyLabel.Add(this);
            label_ = GetComponent<UILabel>();
        }
    }
    public void SetLabel(string txt , bool isShow)
    {
        label_.enabled = isShow;
        label_.text = txt;
    }
}
