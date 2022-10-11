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
    public GameObject PauseScreen;
    bool pauseOnOff = false;
    public Text appleAmauntText;


    void Start()
    {
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
        door = FindObjectOfType<Door>().GetComponent<Door>();

    }

    // Update is called once per frame
    void Update()
    {
        appleAmauntText.text = hero.GetAmountApples().ToString();
    }

    public void SetBullet()
    {
        if (pauseOnOff == false)
            hero.SetAmountApples(2);
    }

    public void OpenDoor()
    {
        if (pauseOnOff == false)
            door.SetDoorIsOpen(true);
    }


    public void SetDestroyAllEnemy(bool destroyAll)
    {
        if (pauseOnOff == false)
            this.destroyAll = destroyAll;
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
    }

    public void PauseOff()
    {
        pauseOnOff = false;
        Time.timeScale = 1f;
        hero.enabled = true;
        PauseScreen.SetActive(false);

    }

    public void ReloadLvl()
    {
        pauseOnOff = false;
        Time.timeScale = 1f;
        hero.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
