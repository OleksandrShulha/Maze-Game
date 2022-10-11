using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] panelLvl;
    int i = 0;
    public GameObject PanelChooseLVL, OsnovneMenu;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


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
}
