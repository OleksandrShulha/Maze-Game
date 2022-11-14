using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Hero : MonoBehaviour
{
    private float speed=5;
    Animator animator;
    private int state = 0;
    private  int amountApples = 0; 
    private int amountKey = 0;
    public GameObject appleBulet;
    public GameObject[] amountKeyOnMap;
    public Transform buletStartPosition;
    bool isBulet = false;
    bool isDead = false;
    bool isWin = false;
    private int vectorBulet = 1; //1-right, 2-down, 3-left,4-up
    Door door;

    public bool joystickUp = false;
    public bool joystickDown = false;
    public bool joystickRight = false;
    public bool joystickLeft = false;

    public MusicGame musicGame;
    public SoundGame soundGame;

    bool destrEnemy1LVL = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        door = FindObjectOfType<Door>().GetComponent<Door>();
    }

    void Update()
    {
        AnimationHero();
        AtackHero();

        if (SceneManager.GetActiveScene().buildIndex - 1 == 1 && destrEnemy1LVL==false)
        {
            if(GameObject.Find("Enemy1") == null)
            {
                StartCoroutine(DeastroeEnemy1LVL());
                destrEnemy1LVL = true;
            }
        }
    }


    private void FixedUpdate()
    {
        MoveHero();
    }

    public int GetVectorBulet()
    {
        return vectorBulet;
    }

    public void SetVectorBulet(int vectorBulet)
    {
        this.vectorBulet=vectorBulet;
    }

    void MoveHeroLeftRight(float speed)
    {
        float run = (speed * Time.deltaTime);
        transform.position = new Vector2(transform.position.x + run, Mathf.Round(transform.position.y));
    }

    void MoveHeroUpDown(float speed)
    {
        float run = (speed * Time.deltaTime);
        transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y + run);
    }

    void AnimationHero()
    {
        animator.SetInteger("state", state);
    }

    void MoveHero()
    {

        if (Input.GetKey(KeyCode.RightArrow) || joystickRight == true)
        {
            MoveHeroLeftRight(speed);
            state = 1;
            SetVectorBulet(1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || joystickLeft == true)
        {
            MoveHeroLeftRight(-speed);
            state = 2;
            SetVectorBulet(3);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || joystickUp == true)
        {
            MoveHeroUpDown(speed);
            state = 3;
            SetVectorBulet(4);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || joystickDown == true)
        {
            MoveHeroUpDown(-speed);
            state = 4;
            SetVectorBulet(2);
        }


        else if(!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)
            && joystickUp == false && joystickDown == false && joystickRight == false && joystickLeft == false)
        {
            if (state == 1)
            {
                SetVectorBulet(1);
                state = 11;
            }
            else if (state == 2)
            {
                state = 22;
                SetVectorBulet(3);
            }

            else if (state == 3)
            {
                state = 33;
                SetVectorBulet(4);
            }
            else if (state == 4)
            {
                state = 44;
                SetVectorBulet(2);
            }
        }

    }

    public void SetAmountApples(int amountApplesLoot)
    {
        amountApples += amountApplesLoot;
    }

    public int GetAmountApples()
    {
        return amountApples;
    }

    public void SetAmountKey(int amountKeyLoot)
    {
        amountKey += amountKeyLoot;
    }

    public void AtackHero()
    {
        if(Input.GetKeyDown(KeyCode.Space) && amountApples > 0 && isBulet==false)
        {
            Instantiate(appleBulet, buletStartPosition.position, buletStartPosition.rotation);
            soundGame.PlayAppleShot();
            isBulet = true;
            amountApples -= 1;
        }
    }

    public void AtackHero2()
    {
        if (amountApples > 0 && isBulet == false)
        {
            soundGame.PlayAppleShot();
            Instantiate(appleBulet, buletStartPosition.position, buletStartPosition.rotation);
            isBulet = true;
            amountApples -= 1;
        }
    }

    public void SetIsBulet(bool isBulet)
    {
        this.isBulet = isBulet;
    }

    public void ExamWinLvl()
    {
        if (amountKeyOnMap.Length == amountKey)
        {
            door.SetDoorIsOpen(true);
            soundGame.PlayDoorOpen();
            StartCoroutine(TakeAllKays());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door" && door.GetDoorIsOpen() == true &&
            Mathf.Abs(door.transform.position.y - transform.position.y) <= 0.25f &&
            Mathf.Abs(door.transform.position.x - transform.position.x) <= 0.25f)
        {
            if (PlayerPrefs.HasKey("coin"))
            {
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 5);
            }
            else
                PlayerPrefs.SetInt("coin", 5);


            Time.timeScale = 0f;
            speed = 0;
            isWin = true;
            Win();
            musicGame.StopMusic();
            soundGame.PlayWin();
        }
    }


    public bool GetIsWin()
    {
        return isWin;
    }

    public void HeroDead()
    {
        isDead = true;
        musicGame.StopMusic();
        soundGame.PlayLoose();
    }

    public bool GetIsDead()
    {
        return isDead;
    }



    public bool GetJostickUp()
    {
        return joystickUp;
    }
    public void SetJostickUp(bool joystickUp)
    {
        this.joystickUp = joystickUp;
    }
    public bool GetJostickRight()
    {
        return joystickRight;
    }
    public void SetJostickRight(bool joystickRight)
    {
        this.joystickRight = joystickRight;
    }

    public bool GetJostickLeft()
    {
        return joystickLeft;
    }
    public void SetJostickLeft(bool joystickLeft)
    {
        this.joystickLeft = joystickLeft;
    }

    public bool GetJostickDown()
    {
        return joystickDown;
    }
    public void SetJostickDown(bool joystickDown)
    {
        this.joystickDown = joystickDown;
    }

    public void Win()
    {
        if (!PlayerPrefs.HasKey("lvl") || PlayerPrefs.GetInt("lvl") < SceneManager.GetActiveScene().buildIndex - 1)
        {
            PlayerPrefs.SetInt("lvl", SceneManager.GetActiveScene().buildIndex - 1);
            StartCoroutine(SendGameOpen());
        }
    }

    IEnumerator SendGameOpen()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", PlayerPrefs.GetInt("login"));
        form.AddField("GameOpen", PlayerPrefs.GetInt("GameOpen"));
        form.AddField("LVL", PlayerPrefs.GetInt("lvl"));
        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/vhod.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("complete");
            }
        }
    }

    IEnumerator TakeAllKays()
    {
        if (SceneManager.GetActiveScene().buildIndex - 1 == 1)
        {
            WWWForm form = new WWWForm();
            form.AddField("login", PlayerPrefs.GetInt("login"));
            form.AddField("TakeAllKeas1LVL", 1);

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/TakeAllKeas1LVL.php", form))
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
            form.AddField("TakeAllKeas2LVL", 1);

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/TakeAllKeas2LVL.php", form))
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
            form.AddField("TakeAllKeas3LVL", 1);

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/TakeAllKeas3LVL.php", form))
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
            form.AddField("TakeAllKeas4LVL", 1);

            using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/TakeAllKeas4LVL.php", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
            }
        }

    }

    IEnumerator DeastroeEnemy1LVL()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", PlayerPrefs.GetInt("login"));
        form.AddField("DeastroeEnemy1LVL", 1);
        using (UnityWebRequest www = UnityWebRequest.Post("https://artixdev.com/MazeGame2/DeastroeEnemy1LVL.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("complete");
            }
        }
    }
}
