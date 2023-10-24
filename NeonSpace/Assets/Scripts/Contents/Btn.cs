using UnityEngine;
using System.Collections;


public class Btn : MonoBehaviour
{
    [SerializeField]
    bool isShow , isExit =false;
    void OnClick()
    {
        if (isExit)
        {
            Time.timeScale = 1;
            DataManager.instance.SetSaveFile(false);
        }
        else
        {
            if (isShow) Time.timeScale = 0;
            else Time.timeScale = 1;
            LobbyController.instance.SetTween(EnumBase.UIState.Pause , isShow);
        }

    }
}
