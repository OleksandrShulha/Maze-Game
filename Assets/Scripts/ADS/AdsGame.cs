using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsGame : MonoBehaviour
{
    //банера
    private BannerView bannerViewBig;
    private BannerView bannerViewSmall;
    private string bannerIDAdMob = "ca-app-pub-3940256099942544/6300978111";


    void Start()
    {
        //MobileAds.Initialize(initStatus => { });

        //запуск банеров
        if (!PlayerPrefs.HasKey("AdsOn") || PlayerPrefs.GetInt("AdsOn") == 1)
        {
            PlayerPrefs.SetInt("AdsOn", 1);
            RequestSmallBanner();
        }


    }


    //Запуск банеров
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

    public void RequestSmallBanner()
    {
        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {
            this.bannerViewSmall = new BannerView(bannerIDAdMob, AdSize.Banner, AdPosition.Top);
            AdRequest request = new AdRequest.Builder().Build();
            this.bannerViewSmall.LoadAd(request);
        }
    }


    //Закрытие банеров
    public void closeBigBaner()
    {
        if (PlayerPrefs.GetInt("AdsOn") == 1)
            this.bannerViewBig.Destroy();
    }

    public void closeSmallBaner()
    {
        if (PlayerPrefs.GetInt("AdsOn") == 1)
            this.bannerViewSmall.Destroy();
    }



    //работа с межстранічной
    //public void StartReklamaPage()
    //{
    //    if (PlayerPrefs.GetInt("AdsOn") == 1)
    //    {

    //        if (!PlayerPrefs.HasKey("NumberReklama"))
    //        {
    //            PlayerPrefs.SetInt("NumberReklama", 1);
    //            //RequestInterstitial();

    //        }
    //        else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 4)
    //        {
    //            //RequestInterstitial();
    //            if (this.interstitial.IsLoaded())
    //            {
    //                this.interstitial.Show();
    //            }
    //            PlayerPrefs.SetInt("NumberReklama", 0);
    //        }

    //        else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 4)
    //        {
    //            PlayerPrefs.SetInt("NumberReklama", PlayerPrefs.GetInt("NumberReklama") + 1);
    //        }
    //    }
    //}
}
