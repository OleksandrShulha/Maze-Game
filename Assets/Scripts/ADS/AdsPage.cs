using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;



public class AdsPage : MonoBehaviour
{
    private InterstitialAd interstitial;
    private string pageIDAdMob = "ca-app-pub-3940256099942544/1033173712";

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        if (PlayerPrefs.GetInt("NumberReklama") == 4)
        {
            StartReklamaPage();
        }
    }


    public void StartReklamaPage()
    {
        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {

            if (!PlayerPrefs.HasKey("NumberReklama"))
            {
                PlayerPrefs.SetInt("NumberReklama", 1);
                RequestInterstitial();

            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 4)
                {
                    RequestInterstitial();
                    if (this.interstitial.IsLoaded())
                    {
                        this.interstitial.Show();
                    }
                    PlayerPrefs.SetInt("NumberReklama", 0);
                }

            else if(PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 4)
                {
                    PlayerPrefs.SetInt("NumberReklama", PlayerPrefs.GetInt("NumberReklama")+1);
                }
        }
    }

    private void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(pageIDAdMob);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
}
