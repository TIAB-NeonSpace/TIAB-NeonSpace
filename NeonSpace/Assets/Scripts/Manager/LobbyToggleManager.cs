using UnityEngine;
using System.Collections;

public class LobbyToggleManager : MonoBehaviour
{
    public static LobbyToggleManager instance;

    [SerializeField]
    UISprite ballSprite;
    [SerializeField]
    UIToggle soundToggle_;

    public int buyCharIdx;
    float times_;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartNext();
    }

    void StartNext()
    {
        if (DataManager.instance.GetSaveFile())
        {
            LobbyController.instance.ChangeStateGame(true);
        }
       
    }

    public void SettingSound()
    {
        soundToggle_.value = SoundManager.instance.BGMSoundState;
    }

    public void SetSoundToggle()
    {
        SoundManager.instance.toggleBGM(soundToggle_.value);
        SoundManager.instance.toggleFXSound(soundToggle_.value);
    }

    public void SetBallToggle()
    {
        //int idx = DataManager.instance.GetBallInt();
        LobbyController.instance.SetSprite(EnumBase.UIState.RocketBook, DataManager.instance.GetBall());
    }

}
