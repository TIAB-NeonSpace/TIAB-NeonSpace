using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;
//using GoogleMobileAds.Api;

public enum adsType
{
    getCoin = 0,
    oneMore
}

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    public adsType myAdsType;
    public string gameId = "2725199";
    public string myAdsId = "rewardedVideo";
    public float myFeverNum = 0;
    public string uniqueUserId = "";
    public string appKey = "50b9dab5";
    public string AdmobAppId = "ca-app-pub-4481686654489037~9338379065";
    public string AdmobId = "ca-app-pub-4481686654489037/7491493666"; 
    //private BannerView bannerView;
    void Start()
    {
        if (instance == null) 
        {
            instance = this;
        }
        initAds();
    }

    void initAds()
    {
        //uniqueUserId = SystemInfo.deviceUniqueIdentifier;
        //Dynamic config example
        //Advertisement.Initialize(gameId);
        ////MobileAds.Initialize(AdmobAppId);
        //RequestBanner();
    }

    private void RequestBanner()
    {
        //bannerView = new BannerView(AdmobId, AdSize.SmartBanner, AdPosition.Bottom);
        //AdRequest request = new AdRequest.Builder().Build();
        //bannerView.LoadAd(request);
    }

    public void BannerEnable(bool isFlag = true)
    {
        //if (isFlag)
        //    bannerView.Show();
        //else
        //    bannerView.Hide();
    }

    public void ShowRewardedAd(int idx)
    {
        myAdsType = (adsType)idx;
        ShowUnityAds();
    }

    public bool isUnityVideoAdsReady()
    {
        //if (GameDataManager.instance.getIsNoAds())
            return false;

        //return Advertisement.IsReady(myAdsId);
    }

    public void ShowUnityAds()
    {
        PlayerPrefsElite.SetInt("myAdsType_", (int)myAdsType);

        //if (Advertisement.IsReady(myAdsId))
        //{
        //    var options = new ShowOptions { resultCallback = HandleShowResult };
        //    Advertisement.Show(myAdsId, options);
        //}
    }

    //unity 광고 본 후 결과처리 함수
    //private void HandleShowResult(ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            checkMyUseType();
    //            break;
    //        case ShowResult.Failed:
    //            Debug.LogError("The ad failed to be shown.");
    //            break;
    //    }
    //}

    void checkMyUseType()
    {
        myAdsType = (adsType)PlayerPrefsElite.GetInt("myAdsType_");
        switch (myAdsType)
        {
            case adsType.getCoin:
                DataManager.instance.SetCoin(20);
                LobbyController.instance.SetLabel(EnumBase.UIState.Coin, string.Format("{0:N0}", DataManager.instance.GetCoin()));
                //ChargeManager.intance.CoinEffect();
                break;
            case adsType.oneMore:
                //UIManager.instance.Continue(false);
                BrickManager.instance.OnemoreSet();
                break;
        }
    }
}