using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Networking;

public class UIControl : MonoBehaviour
{
    Hero hero;
    Door door;
    public bool destroyAll = false;
    public GameObject PauseScreen, UiControl, UIJouStick, DeadScreen, WinScreen;
    bool pauseOnOff = false;
    public Text appleAmauntText, appleText, bombText, doorText;
    public Button applesBtn, doorBtn, bombBtn;



    private InterstitialAd interstitial;
    private string pageIDAdMob = "ca-app-pub-3940256099942544/1033173712";
    private bool isReloadBtn = false, isExitBtn=false, isNextLvlBtn=false; 
    public AudioSource audioSourceMusic;

    private RewardedAd rewardedAd;
    private string rewardedIDAdMob = "ca-app-pub-3940256099942544/5224354917";
    public GameObject PanelBonusReward;



    void Start()
    {
        RequestInterstitial();
        ReguestRewerdedVideo();
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
        door = FindObjectOfType<Door>().GetComponent<Door>();
        pauseOnOff = false;
        Time.timeScale = 1f;
        hero.enabled = true;
        UItextMenu();


    }

    // Update is called once per frame
    void Update()
    {
        appleAmauntText.text = hero.GetAmountApples().ToString();
        if (hero.GetIsDead() == true)
        {
            DeadMenu();
        }
        if (hero.GetIsWin() == true)
        {
            WinMenu();
        }

        ActivationBonusBar();
    }

    void ActivationBonusBar()
    {
        if (!PlayerPrefs.HasKey("bombs") || PlayerPrefs.GetInt("bombs")==0)
        {
            bombBtn.interactable = false;
        }
        else
            bombBtn.interactable = true;


        if (!PlayerPrefs.HasKey("apples") || PlayerPrefs.GetInt("apples") == 0)
        {
            applesBtn.interactable = false;
        }
        else
            applesBtn.interactable = true;

        if (!PlayerPrefs.HasKey("doors") || PlayerPrefs.GetInt("doors") == 0)
        {
            doorBtn.interactable = false;
        }
        else
            doorBtn.interactable = true;
    }

    void UItextMenu()
    {

        if (PlayerPrefs.HasKey("apples"))
        {
            appleText.text = PlayerPrefs.GetInt("apples").ToString();
        }

        if (PlayerPrefs.HasKey("bombs"))
        {
            bombText.text = PlayerPrefs.GetInt("bombs").ToString();
        }

        if (PlayerPrefs.HasKey("doors"))
        {
            doorText.text = PlayerPrefs.GetInt("doors").ToString();
        }
    }

    public void WinMenu()
    {
        pauseOnOff = true;
        Time.timeScale = 0f;
        hero.enabled = false;
        WinScreen.SetActive(true);
        UiControl.SetActive(false);
        UIJouStick.SetActive(false);
    }

    public void DeadMenu()
    {
        pauseOnOff = true;
        Time.timeScale = 0f;
        hero.enabled = false;
        DeadScreen.SetActive(true);
        UiControl.SetActive(false);
        UIJouStick.SetActive(false);
    }

    public void SetBullet()
    {
        if (pauseOnOff == false && PlayerPrefs.HasKey("apples"))
        {
            if (PlayerPrefs.GetInt("apples") >= 1)
            {
                PlayerPrefs.SetInt("apples", PlayerPrefs.GetInt("apples") - 1);
                appleText.text = PlayerPrefs.GetInt("apples").ToString();
                hero.SetAmountApples(2);
            }
        }

    }

    public void OpenDoor()
    {
        if (pauseOnOff == false && PlayerPrefs.HasKey("doors"))
        {
            if (PlayerPrefs.GetInt("doors") >= 1)
            {
                PlayerPrefs.SetInt("doors", PlayerPrefs.GetInt("doors") - 1);
                doorText.text = PlayerPrefs.GetInt("doors").ToString();
                door.SetDoorIsOpen(true);
            }
        }

    }

    public void SetDestroyAllEnemy(bool destroyAll)
    {
        if (pauseOnOff == false && PlayerPrefs.HasKey("bombs"))
        {
            if(PlayerPrefs.GetInt("bombs") >= 1)
            {
                PlayerPrefs.SetInt("bombs", PlayerPrefs.GetInt("bombs")-1);
                bombText.text = PlayerPrefs.GetInt("bombs").ToString();
                this.destroyAll = destroyAll;
            }
        }

    }

    public bool GetDestroyAll()
    {
        return destroyAll;
    }

    public void fireBtnClick()
    {
        if(pauseOnOff==false)
            hero.AtackHero2();
    }

    public void PauseOn()
    {
        pauseOnOff = true;
        Time.timeScale = 0f;
        hero.enabled = false;
        PauseScreen.SetActive(true);
        UiControl.SetActive(false);
        UIJouStick.SetActive(false);

    }

    public void PauseOff()
    {
        pauseOnOff = false;
        Time.timeScale = 1f;
        hero.enabled = true;
        PauseScreen.SetActive(false);
        UiControl.SetActive(true);
        UIJouStick.SetActive(true);
    }

    public void ReloadLvl()
    {
        isReloadBtn = true;
        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {
            if (!PlayerPrefs.HasKey("NumberReklama"))
            {
                PlayerPrefs.SetInt("NumberReklama", 1);
            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 4)
            {
                PlayerPrefs.SetInt("NumberReklama", 0);
                if (this.interstitial.IsLoaded())
                {
                    if (!PlayerPrefs.HasKey("ADPage"))
                    {
                        PlayerPrefs.SetInt("ADPage", 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ADPage", PlayerPrefs.GetInt("ADPage") + 1);
                    }
                    StartCoroutine(ADPage());
                    audioSourceMusic.Stop();
                    this.interstitial.Show();
                }
                else
                {
                    isReloadBtn = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 4)
            {
                PlayerPrefs.SetInt("NumberReklama", PlayerPrefs.GetInt("NumberReklama") + 1);
                isReloadBtn = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            isReloadBtn = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ExitBtn()
    {
        isExitBtn = true;
        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {
            if (!PlayerPrefs.HasKey("NumberReklama"))
            {
                PlayerPrefs.SetInt("NumberReklama", 1);
            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 4)
            {
                PlayerPrefs.SetInt("NumberReklama", 0);
                if (this.interstitial.IsLoaded())
                {
                    if (!PlayerPrefs.HasKey("ADPage"))
                    {
                        PlayerPrefs.SetInt("ADPage", 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ADPage", PlayerPrefs.GetInt("ADPage") + 1);
                    }
                    StartCoroutine(ADPage());
                    audioSourceMusic.Stop();
                    this.interstitial.Show();
                }
                else
                {
                    isExitBtn = false;
                    SceneManager.LoadScene(1);
                }
            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 4)
            {
                PlayerPrefs.SetInt("NumberReklama", PlayerPrefs.GetInt("NumberReklama") + 1);
                isExitBtn = false;
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            isExitBtn = false;
            SceneManager.LoadScene(1);
        }
    }

    public void LoadNextScene()
    {
        isNextLvlBtn = true;
        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {
            if (!PlayerPrefs.HasKey("NumberReklama"))
            {
                PlayerPrefs.SetInt("NumberReklama", 1);
            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 4)
            {
                PlayerPrefs.SetInt("NumberReklama", 0);
                if (this.interstitial.IsLoaded())
                {
                    if (!PlayerPrefs.HasKey("ADPage"))
                    {
                        PlayerPrefs.SetInt("ADPage", 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ADPage", PlayerPrefs.GetInt("ADPage") + 1);
                    }
                    StartCoroutine(ADPage());
                    audioSourceMusic.Stop();
                    this.interstitial.Show();
                }
                else
                {
                    isNextLvlBtn = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 4)
            {
                PlayerPrefs.SetInt("NumberReklama", PlayerPrefs.GetInt("NumberReklama") + 1);
                isNextLvlBtn = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            isNextLvlBtn = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
        if (isReloadBtn == true)
        {
            isReloadBtn = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (isExitBtn == true)
        {
            isExitBtn = false;
            SceneManager.LoadScene(1);
        }
        if (isNextLvlBtn == true)
        {
            isNextLvlBtn = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            audioSourceMusic.Play();
            audioSourceMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
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
        PanelBonusReward.SetActive(true);
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        PanelBonusReward.SetActive(false);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        PanelBonusReward.SetActive(false);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        PanelBonusReward.SetActive(false);
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        PanelBonusReward.SetActive(false);
        if (PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 5);
        }
        else
            PlayerPrefs.SetInt("coin", 5);
    }

    public void StartRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            PanelBonusReward.SetActive(false);
            audioSourceMusic.Stop();
            this.rewardedAd.Show();
        }
    }

    IEnumerator ADPage()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", PlayerPrefs.GetInt("login"));
        form.AddField("ADPage", PlayerPrefs.GetInt("ADPage"));

        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ADPage.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }
}
