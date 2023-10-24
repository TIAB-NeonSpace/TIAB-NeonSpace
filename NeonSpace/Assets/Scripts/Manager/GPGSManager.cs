//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        initPlayGame();
    }

    public  void initPlayGame()
    {
       // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       ////// enables saving game progress.
       ////.EnableSavedGames()
       ////// registers a callback to handle game invitations received while the game is not running.
       ////.WithInvitationDelegate(< callback method >)
       ////// registers a callback for turn based match notifications received while the
       ////// game is not running.
       ////.WithMatchDelegate(< callback method >)
       ////// requests the email address of the player be available.
       ////// Will bring up a prompt for consent.
       ////.RequestEmail()
       ////// requests a server auth code be generated so it can be passed to an
       //////  associated back end server application and exchanged for an OAuth token.
       ////.RequestServerAuthCode(false)
       ////// requests an ID token be generated.  This OAuth token can be used to
       //////  identify the player to other services such as Firebase.
       ////.RequestIdToken()
       //.Build();

       // PlayGamesPlatform.InitializeInstance(config);
       // // recommended for debugging:
       // PlayGamesPlatform.DebugLogEnabled = true;
       // // Activate the Google Play Games platform
       // PlayGamesPlatform.Activate();
    }

    public bool bLogin()
    {
        return Social.localUser.authenticated;
    }
    // GPGS를 로그인 합니다.
    public void LoginGPGS()
    {
        
        //Debug.Log("LoginGPGS");
        //// 로그인이 안되어 있으면
        //// request auth
        Social.localUser.Authenticate((bool _success) =>
        {
            // if login success
            if (_success)
            {
                //((PlayGamesLocalUser)Social.localUser).GetStats((rc, stats) =>
                //{
                //    -1 means cached stats, 0 is succeess
                //    see  CommonStatusCodes for all values.
                //    if (rc <= 0 && stats.HasDaysSinceLastPlayed())
                //        {
                //        }
                //});
                //showLeaderBoard();
                //OptionPupManager.instance.GoogleToggle();
            }
            //else
            //{

            //}
        });
    }

    public void showLeaderBoard()
    {
        if (!bLogin()) LoginGPGS();
        Social.ShowLeaderboardUI();
    }
    public void showAchievements()
    {
        if (!bLogin()) LoginGPGS();
        Social.ShowAchievementsUI();
    }

    public void setScoreLeaderBoard()
    {
        int myScore = DataManager.instance.GetBestScore(); // 수정
        Social.ReportScore(myScore, NeonSpace.leaderboard_bestscore, (bool success) =>
        {
            //handle success or failure
        });
    }

    public void setAchievements(int idx = 0)
    {
        if (DataManager.instance.GetAchievement(idx)) return;

        string achIDx = "";
        switch (idx)
        {
            case 0: achIDx = NeonSpace.achievement_play_10_times_beginner; break;
            case 1: achIDx = NeonSpace.achievement_play_50_times_intermediate; break;
            case 2: achIDx = NeonSpace.achievement_100_score_beginner; break;
            case 3: achIDx = NeonSpace.achievement_200_score_intermediate; break;
            case 4: achIDx = NeonSpace.achievement_300_score_supervisor; break;
        }
        Social.ReportProgress(achIDx, 100.0f, (bool success) => {
            // handle success or failure
            DataManager.instance.SetAchievement(idx);
        });
    }
}
