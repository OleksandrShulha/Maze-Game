using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    Hero hero;
    Door door;
    public bool destroyAll = false;
    public GameObject PauseScreen, UiControl, UIJouStick, DeadScreen, WinScreen;
    bool pauseOnOff = false;
    public Text appleAmauntText, appleText, bombText, doorText;
    public Button applesBtn, doorBtn, bombBtn;


    void Start()
    {
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

    public void GetCoinsForAdsonGames()
    {
        if (PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 5);
        }
        else
            PlayerPrefs.SetInt("coin", 5);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitBtn()
    {
        SceneManager.LoadScene(0);
    }


}
