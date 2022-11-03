using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] panelLvl;
    int i = 0;
    public GameObject PanelChooseLVL, OsnovneMenu, ShoopMenu, PanelShoop, PanelItem, SettingMenu, AboutUsMenu, BGImage, LogoImage;
    public Text coinText, appleText, bombText, doorText;
    int coins = 0, apples = 0, bombs = 0, doors=0;
    public Button[] lvls;

    public Toggle musicOn, soundOn;
    public Slider musicSlider, soundSlider;
    public AudioSource audioSourceMusic;

    private void Awake()
    {

    SetSettingsParametrsOnStart();
    }

    void LogoLoading()
    {
        OsnovneMenu.SetActive(true);
        BGImage.SetActive(true);
        LogoImage.SetActive(false);
    }

    void Start()
    {
        UItextMenu();
        SetLvl();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        audioSourceMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
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

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
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
}
