using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsMenu : MonoBehaviour
{
    private BannerView bannerViewBig;
    private string bannerIDAdMob = "ca-app-pub-3993614711620555/7935676284";


    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        if (!PlayerPrefs.HasKey("AdsOn"))
            PlayerPrefs.SetInt("AdsOn", 1);
        RequestBigBanner();
    }

    //Запуск банера
    public void RequestBigBanner()
    {
        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {
            //AdSize adSize = new AdSize(300, 250);
            this.bannerViewBig = new BannerView(bannerIDAdMob, AdSize.Banner, AdPosition.Top);
            AdRequest request = new AdRequest.Builder().Build();
            this.bannerViewBig.LoadAd(request);
        }
    }

    //Закрытие банера
    public void closeBigBaner()
    {
        if (PlayerPrefs.GetInt("AdsOn") == 1)
            this.bannerViewBig.Destroy();
    }
}
