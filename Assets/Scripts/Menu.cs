using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

using GoogleMobileAds.Api;
using System;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] panelLvl;
    int i = 0;
    public GameObject PanelChooseLVL, OsnovneMenu, ShoopMenu, PanelShoop, PanelItem, SettingMenu, AboutUsMenu;
    public Text coinText, appleText, bombText, doorText;
    int coins = 0, apples = 0, bombs = 0, doors=0;
    public Button[] lvls;

    public Button bayAdsOf;

    public Toggle musicOn, soundOn;
    public Slider musicSlider, soundSlider;
    public AudioSource audioSourceMusic;


    private InterstitialAd interstitial;
    private string pageIDAdMob = "ca-app-pub-3940256099942544/1033173712";
    private int numberOfLoadScene;

    private RewardedAd rewardedAd;
    private string rewardedIDAdMob = "ca-app-pub-3940256099942544/5224354917";
    public Button rewardWatch;

    private void Awake()
    {
        SetSettingsParametrsOnStart();
        SetBayParametr();
    }

    void Start()
    {
        UItextMenu();
        SetLvl();
        RequestInterstitial();
        ReguestRewerdedVideo();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        audioSourceMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    void SetBayParametr()
    {
        if (PlayerPrefs.HasKey("AdsOn") && PlayerPrefs.GetInt("AdsOn") == 0)
        {
            bayAdsOf.interactable = false;
        }
    }

    public void BayAdsOff()
    {
        PlayerPrefs.SetInt("AdsOn", 0);
        bayAdsOf.interactable = false;
    }


    void SetSettingsParametrsOnStart()
    {
        if (!PlayerPrefs.HasKey("SoundVolume"))
            PlayerPrefs.SetFloat("SoundVolume", 1f);
        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", 0.35f);

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");


        if (!PlayerPrefs.HasKey("SoundOn"))
            PlayerPrefs.SetInt("SoundOn", 1);
        if (!PlayerPrefs.HasKey("MusicOn"))
            PlayerPrefs.SetInt("MusicOn", 1);

        if (PlayerPrefs.GetInt("MusicOn") == 1)
            musicOn.isOn = true;
        else
            musicOn.isOn = false;

        if (PlayerPrefs.GetInt("SoundOn") == 1)
            soundOn.isOn = true;
        else
            soundOn.isOn = false;
    }

    public void SoundOn()
    {
        if (soundOn.isOn == true)
        {
            PlayerPrefs.SetInt("SoundOn", 1);

        }
        else
        {
            PlayerPrefs.SetInt("SoundOn", 0);
   
        }
    }

    public void MusicOn()
    {
        if (musicOn.isOn == true)
        {
            PlayerPrefs.SetInt("MusicOn", 1);
            audioSourceMusic.Play();
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", 0);
            audioSourceMusic.Stop();
        }
    }

    void SetLvl()
    {
        if (PlayerPrefs.HasKey("lvl"))
            for (int i = 0; i < lvls.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("lvl"))
                    lvls[i].interactable = true;
                else
                {
                    lvls[i].interactable = false;
                    lvls[i].GetComponentInChildren<Text>().text = "";
                }
            }
        else
        {
            for (int i = 1; i < lvls.Length; i++)
            {
                lvls[i].interactable = false;
                lvls[i].GetComponentInChildren<Text>().text = "";
            }
        }
        
    }

    void UItextMenu()
    {
        if (PlayerPrefs.HasKey("coin"))
        {
            coinText.text = PlayerPrefs.GetInt("coin").ToString();
            coins = PlayerPrefs.GetInt("coin");
        }
        else
            coinText.text = coins.ToString();



        if (PlayerPrefs.HasKey("apples"))
        {
            appleText.text = PlayerPrefs.GetInt("apples").ToString();
            apples = PlayerPrefs.GetInt("apples");
        }
        else
            appleText.text = apples.ToString();




        if (PlayerPrefs.HasKey("bombs"))
        {
            bombText.text = PlayerPrefs.GetInt("bombs").ToString();
            bombs = PlayerPrefs.GetInt("bombs");
        }
        else
            bombText.text = bombs.ToString();




        if (PlayerPrefs.HasKey("doors"))
        {
            doorText.text = PlayerPrefs.GetInt("doors").ToString();
            doors = PlayerPrefs.GetInt("doors");
        }
        else
            doorText.text = doors.ToString();
    }



    public void OpenShoopMenu()
    {
        OsnovneMenu.SetActive(false);
        ShoopMenu.SetActive(true);
        PanelShoop.SetActive(false);
        PanelItem.SetActive(true);
    }


    public void OpenAboutUsMenu()
    {
        OsnovneMenu.SetActive(false);
        AboutUsMenu.SetActive(true);
    }

    public void CloseAboutUsMenu()
    {
        OsnovneMenu.SetActive(true);
        AboutUsMenu.SetActive(false);
    }


    public void OpenSettingsMenu()
    {
        OsnovneMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        OsnovneMenu.SetActive(true);
        SettingMenu.SetActive(false);
    }



    public void CloseShoopMenu()
    {
        OsnovneMenu.SetActive(true);
        ShoopMenu.SetActive(false);
    }

    public void OpenPanelItem()
    {
        PanelShoop.SetActive(false);
        PanelItem.SetActive(true);
    }
    public void OpenPanelShoop()
    {
        PanelShoop.SetActive(true);
        PanelItem.SetActive(false);
    }

    public void OpenGuideMenu()
    {
        if (!PlayerPrefs.HasKey("ClickBTNGuide"))
        {
            PlayerPrefs.SetInt("ClickBTNGuide", 1);
        }
        else
            PlayerPrefs.SetInt("ClickBTNGuide", PlayerPrefs.GetInt("ClickBTNGuide") + 1);

        if (!PlayerPrefs.HasKey("FirstTapBTNGuide"))
        {
            PlayerPrefs.SetInt("FirstTapBTNGuide", 1);
        }
        StartCoroutine(CliclGuide());
    }


    public void OpenScene(int index)
    {
        if (!PlayerPrefs.HasKey("StartLVLBtn"))
        {
            PlayerPrefs.SetInt("StartLVLBtn", 1);
        }
        else
            PlayerPrefs.SetInt("StartLVLBtn", PlayerPrefs.GetInt("StartLVLBtn") + 1);
        if (!PlayerPrefs.HasKey("FirstGamePlay"))
        {
            PlayerPrefs.SetInt("FirstGamePlay", 1);
        }

        StartCoroutine(ClickPlay());

        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {

            if (!PlayerPrefs.HasKey("NumberReklama"))
            {
                PlayerPrefs.SetInt("NumberReklama", 1);
                SceneManager.LoadScene(index+1);
            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 4)
            {
                PlayerPrefs.SetInt("NumberReklama", 0);
                if (this.interstitial.IsLoaded())
                {
                    numberOfLoadScene = index+1;
                    PanelChooseLVL.SetActive(false);
                    audioSourceMusic.Stop();
                    this.interstitial.Show();
                }
                else
                    SceneManager.LoadScene(index+1);
            }

            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 4)
            {
                PlayerPrefs.SetInt("NumberReklama", PlayerPrefs.GetInt("NumberReklama") + 1);
                SceneManager.LoadScene(index+1);
            }
        }
        else
            SceneManager.LoadScene(index+1);
    }

    public void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(pageIDAdMob);
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
        SceneManager.LoadScene(numberOfLoadScene);
    }





    public void MenuBtnChooseLVLActivate()
    {
        OsnovneMenu.SetActive(false);
        PanelChooseLVL.SetActive(true);


    }

    public void MenuBtnBackLVLChoose()
    {
        OsnovneMenu.SetActive(true);
        PanelChooseLVL.SetActive(false);
        panelLvl[0].SetActive(true);
        for(int j=1; j<panelLvl.Length; j++)
        {
            panelLvl[j].SetActive(false);
        }
        i = 0;

    }

    public void MenuRightArrowLVLChoose()
    {
        i++;
        if(i <= panelLvl.Length-1)
        {
            panelLvl[i-1].SetActive(false);
            panelLvl[i].SetActive(true);
        }
        else
        {
            i = panelLvl.Length-1;
        }

    }

    public void MenuLeftArrowLVLChoose()
    {
        i--;
        if (i >= 0)
        {
            panelLvl[i + 1].SetActive(false);
            panelLvl[i].SetActive(true);
        }
        else
        {
            i = 0;
        }
    }

    public void GetCoinsForAds(int costCoins)
    {
        coins += costCoins;
        coinText.text = coins.ToString();
        PlayerPrefs.SetInt("coin", coins);
    }

    public void ByApplesForCoins()
    {
        if (coins >= 50)
        {
            apples += 2;
            coins -= 50;
            appleText.text = apples.ToString();
            PlayerPrefs.SetInt("apples", apples);
            coinText.text = coins.ToString();
            PlayerPrefs.SetInt("coin", coins);
        }
    }

    public void ByBombsForCoins()
    {
        if (coins >= 100)
        {
            bombs += 2;
            coins -= 100;
            bombText.text = bombs.ToString();
            PlayerPrefs.SetInt("bombs", bombs);
            coinText.text = coins.ToString();
            PlayerPrefs.SetInt("coin", coins);
        }
    }

    public void ByDoorsForCoins()
    {
        if (coins >= 200)
        {
            doors += 2;
            coins -= 200;
            doorText.text = doors.ToString();
            PlayerPrefs.SetInt("doors", doors);
            coinText.text = coins.ToString();
            PlayerPrefs.SetInt("coin", coins);
        }
    }

    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }


    public void ReguestRewerdedVideo()
    {
        this.rewardedAd = new RewardedAd(rewardedIDAdMob);
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        rewardWatch.interactable = true;
}

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        rewardWatch.interactable = false;
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        rewardWatch.interactable = false;
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        rewardWatch.interactable = false;
        SetSettingsParametrsOnStart();
        MusicOn();
        ReguestRewerdedVideo();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        rewardWatch.interactable = false;
        GetCoinsForAds(20);
    }

    public void StartRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            rewardWatch.interactable = false;
            audioSourceMusic.Stop();
            this.rewardedAd.Show();
        }
    }

    IEnumerator ClickPlay()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", PlayerPrefs.GetInt("login"));
        form.AddField("StartLVLBtn", PlayerPrefs.GetInt("StartLVLBtn"));
        form.AddField("FirstGamePlay", PlayerPrefs.GetInt("FirstGamePlay"));
        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/cliclplay.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }
    IEnumerator CliclGuide()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", PlayerPrefs.GetInt("login"));
        form.AddField("FirstTapBTNGuide", PlayerPrefs.GetInt("FirstTapBTNGuide"));
        form.AddField("ClickBTNGuide", PlayerPrefs.GetInt("ClickBTNGuide"));
        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/cliclguide.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }
}
