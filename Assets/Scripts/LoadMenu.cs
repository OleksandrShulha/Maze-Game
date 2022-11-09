using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoadMenu : MonoBehaviour
{
    //Analotics
    private int login;
    private int GameOpen;
    private int LVL;

    void Start()
    {
        RegisterOnGame();
        Invoke("LoadNextScene", 1.5f);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    void RegisterOnGame()
    {
        if (!PlayerPrefs.HasKey("login"))
        {
            login = Random.Range(1000000, 10000000);
            GameOpen = 1;
            LVL = 0;
            PlayerPrefs.SetInt("login", login);
            PlayerPrefs.SetInt("GameOpen", GameOpen);
            PlayerPrefs.SetInt("LVL", LVL);
        }
        else
        {
            login = PlayerPrefs.GetInt("login");
            GameOpen = PlayerPrefs.GetInt("GameOpen") + 1;
            LVL = PlayerPrefs.GetInt("lvl");
            PlayerPrefs.SetInt("GameOpen", GameOpen);
        }
        StartCoroutine(SendRegGame());
        StartCoroutine(SendGameOpen());
    }

    IEnumerator SendRegGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("GameOpen", GameOpen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/reg.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }

    IEnumerator SendGameOpen()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("GameOpen", GameOpen);
        form.AddField("LVL", LVL);
        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/vhod.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }
}
