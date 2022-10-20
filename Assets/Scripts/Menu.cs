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
    public GameObject PanelChooseLVL, OsnovneMenu, ShoopMenu, PanelShoop, PanelItem;
    public Text coinText, appleText, bombText, doorText;
    int coins = 0, apples = 0, bombs = 0, doors=0;
    public Button[] lvls;

    void Start()
    {
        UItextMenu();
        SetLvl();
    }

    // Update is called once per frame
    void Update()
    {


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
