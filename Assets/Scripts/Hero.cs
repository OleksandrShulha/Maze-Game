using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int vectorBulet = 1; //1-right, 2-down, 3-left,4-up
    Door door;

    public bool joystickUp = false;
    public bool joystickDown = false;
    public bool joystickRight = false;
    public bool joystickLeft = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        door = FindObjectOfType<Door>().GetComponent<Door>();
    }

    void Update()
    {
        AnimationHero();
        AtackHero();
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
        if(Input.GetKeyDown(KeyCode.Space) && amountApples > 0 && isBulet==false){
            Instantiate(appleBulet, buletStartPosition.position, buletStartPosition.rotation);
            isBulet = true;
            amountApples -= 1;
        }
    }

    public void AtackHero2()
    {
        if (amountApples > 0 && isBulet == false)
        {
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
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door" && door.GetDoorIsOpen() == true &&
            Mathf.Abs(door.transform.position.y - transform.position.y) <= 0.25f &&
            Mathf.Abs(door.transform.position.x - transform.position.x) <= 0.25f)
        {
            Debug.Log("Победа!!!");
            Time.timeScale = 0f;
            speed = 0;

        }
    }

    public void HeroDead()
    {
        Debug.Log("Ded");
        //Time.timeScale = 0f;
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
}
