using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsMenu : MonoBehaviour
{
    private BannerView bannerViewBig;
    private string bannerIDAdMob = "ca-app-pub-3940256099942544/6300978111";


    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        if (!PlayerPrefs.HasKey("AdsOn"))
            PlayerPrefs.SetInt("AdsOn", 1);
    }

    //Запуск банера
    public void RequestBigBanner()
    {
        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {
            AdSize adSize = new AdSize(300, 250);
            this.bannerViewBig = new BannerView(bannerIDAdMob, adSize, AdPosition.Top);
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
