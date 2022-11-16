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
        StartCoroutine(LoadLVL());
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
        if (!PlayerPrefs.HasKey("RestartLVL"))
        {
            PlayerPrefs.SetInt("RestartLVL", 1);
        }
        else
        {
            PlayerPrefs.SetInt("RestartLVL", PlayerPrefs.GetInt("RestartLVL") + 1);
        }
        StartCoroutine(RestartLVL());
        StartCoroutine(RestartLVLOnLVL());

        isReloadBtn = true;
        if (PlayerPrefs.GetInt("AdsOn") == 1)
        {
            if (!PlayerPrefs.HasKey("NumberReklama"))
            {
                PlayerPrefs.SetInt("NumberReklama", 1);
            }
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 3)
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
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 3)
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
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 3)
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
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 3)
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
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") == 3)
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
            else if (PlayerPrefs.HasKey("NumberReklama") && PlayerPrefs.GetInt("NumberReklama") < 3)
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
        if (!PlayerPrefs.HasKey("WinGameBouns"))
        {
            PlayerPrefs.SetInt("WinGameBouns", 1);
        }
        else
        {
            PlayerPrefs.SetInt("WinGameBouns", PlayerPrefs.GetInt("WinGameBouns") + 1);
        }
        StartCoroutine(WinGameBouns());

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
        if (!PlayerPrefs.HasKey("ClickADWinGame"))
        {
            PlayerPrefs.SetInt("ClickADWinGame", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ClickADWinGame", PlayerPrefs.GetInt("ClickADWinGame") + 1);
        }
        StartCoroutine(ClickADWinGame());

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
    IEnumerator ClickADWinGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", PlayerPrefs.GetInt("login"));
        form.AddField("ClickADWinGame", PlayerPrefs.GetInt("ClickADWinGame"));

        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickADWinGame.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }
    IEnumerator WinGameBouns()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", PlayerPrefs.GetInt("login"));
        form.AddField("WinGameBouns", PlayerPrefs.GetInt("WinGameBouns"));

        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/WinGameBouns.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }
    IEnumerator RestartLVL()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", PlayerPrefs.GetInt("login"));
        form.AddField("RestartLVL", PlayerPrefs.GetInt("RestartLVL"));

        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/RestartLVL.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }

    IEnumerator LoadLVL()
    {
        if (SceneManager.GetActiveScene().buildIndex - 1 == 1)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("Load1LVL", 1);

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/Load1LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 2)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("Load2LVL", 1);

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/Load2LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 3)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("Load3LVL", 1);

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/Load3LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 4)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("Load4LVL", 1);

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/Load4LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }
    }


    IEnumerator RestartLVLOnLVL()
    {
        if (SceneManager.GetActiveScene().buildIndex - 1 == 1)
        {
            if (!PlayerPrefs.HasKey("ClickRestart1LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart1LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart1LVL", PlayerPrefs.GetInt("ClickRestart1LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart1LVL", PlayerPrefs.GetInt("ClickRestart1LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart1LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 2)
        {
            if (!PlayerPrefs.HasKey("ClickRestart2LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart2LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart2LVL", PlayerPrefs.GetInt("ClickRestart2LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart2LVL", PlayerPrefs.GetInt("ClickRestart2LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart2LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 3)
        {
            if (!PlayerPrefs.HasKey("ClickRestart3LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart3LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart3LVL", PlayerPrefs.GetInt("ClickRestart3LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart3LVL", PlayerPrefs.GetInt("ClickRestart3LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart3LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 4)
        {
            if (!PlayerPrefs.HasKey("ClickRestart4LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart4LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart4LVL", PlayerPrefs.GetInt("ClickRestart4LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart4LVL", PlayerPrefs.GetInt("ClickRestart4LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart4LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 5)
        {
            if (!PlayerPrefs.HasKey("ClickRestart5LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart5LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart5LVL", PlayerPrefs.GetInt("ClickRestart5LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart5LVL", PlayerPrefs.GetInt("ClickRestart5LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart5LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 6)
        {
            if (!PlayerPrefs.HasKey("ClickRestart6LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart6LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart6LVL", PlayerPrefs.GetInt("ClickRestart6LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart6LVL", PlayerPrefs.GetInt("ClickRestart6LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart6LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 7)
        {
            if (!PlayerPrefs.HasKey("ClickRestart7LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart7LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart7LVL", PlayerPrefs.GetInt("ClickRestart7LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart7LVL", PlayerPrefs.GetInt("ClickRestart7LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart7LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 8)
        {
            if (!PlayerPrefs.HasKey("ClickRestart8LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart8LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart8LVL", PlayerPrefs.GetInt("ClickRestart8LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart8LVL", PlayerPrefs.GetInt("ClickRestart8LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart8LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex - 1 == 9)
        {
            if (!PlayerPrefs.HasKey("ClickRestart9LVL"))
            {
                PlayerPrefs.SetInt("ClickRestart9LVL", 1);
            }
            else
                PlayerPrefs.SetInt("ClickRestart9LVL", PlayerPrefs.GetInt("ClickRestart9LVL") + 1);

            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("ClickRestart9LVL", PlayerPrefs.GetInt("ClickRestart9LVL"));

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/ClickRestart9LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }
    }
}
