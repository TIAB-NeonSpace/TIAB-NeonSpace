using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource[] musicSound;
    [SerializeField]
    AudioSource[] effectSounds;
    [SerializeField]
    AudioClip[] bgm;
    [SerializeField]
    AudioClip[] effect;
    [SerializeField]
    AudioClip[] tapSound;
    public static SoundManager instance;

    [SerializeField]
    bool bSoundEnabled = true;   // 게임 효과음
    [SerializeField]
    bool bMusicEnabled = true;   // 뒤에 음악(BGM)


    public bool FXSoundState
    {
        get { return PlayerPrefsElite.GetBoolean("FXState_"); }
        set { PlayerPrefsElite.SetBoolean("FXState_", value); }
    }

    public bool BGMSoundState
    {
        get { return PlayerPrefsElite.GetBoolean("BGMState_"); }
        set { PlayerPrefsElite.SetBoolean("BGMState_", value); }
    }

    public void toggleFXSound(bool state) // 효과음을 껐다 켰다 할 수 있다
    {
        bSoundEnabled = FXSoundState = state;
        if (state) effectSounds[0].Play();
        else effectSounds[0].Pause();
    }
    public void toggleBGM(bool state)// 배경음을 껐다 켰다 할 수 있다
    {
        bMusicEnabled = BGMSoundState = state;
        if (state) musicSound[0].Play();
        else musicSound[0].Pause();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Invoke("LateStart", 0.2f);
    }

    void LateStart()
    {
        if (!PlayerPrefsElite.GetBoolean("SoundFirst"))
        {
            FXSoundState = true;
            BGMSoundState = true;
            PlayerPrefsElite.SetBoolean("SoundFirst", true);
        }
        toggleBGM(BGMSoundState);
        toggleFXSound(FXSoundState);
        LobbyToggleManager.instance.SettingSound();

    }

    public void OffSound()
    {
        musicSound[0].Stop();
    }

    public void ChangeBGM(int clipNum) // 배경음을 바꾸는 함수
    {
        musicSound[0].clip = bgm[clipNum];
        if (bMusicEnabled == true) musicSound[0].Play();
        else musicSound[0].Stop();
    }

    public void ChangeEM(int clipNum) // 배경음 외에 다른 사운드를 넣고 싶으면 넣어준다.
    {
        musicSound[1].clip = bgm[clipNum];
        if (bSoundEnabled) musicSound[1].Play();
        else musicSound[1].Stop();

    }
    public void BGMPitch(float pitchup) // 배경음에 재생속도를 바꿔준다.
    {
        musicSound[0].pitch = pitchup;
    }

    public void ChangeEffects(int clipNum , float volume = 0.5f) // 효과음을 바꿔준다.
    {
        if (bSoundEnabled) effectSounds[0].PlayOneShot(effect[clipNum] , volume);
    }

    public void StopEffectS(int num)
    {
        if (bSoundEnabled) effectSounds[num].Stop();
    }

    public void TapSound(int clipNum = 0) // 이건 탭사운드이당.
    {
        effectSounds[0].clip = tapSound[clipNum];
        if (bSoundEnabled) effectSounds[0].Play();
    }
}
