using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class Enemy2 : MonoBehaviour
{
    Animator animator;
    private int state = 0;
    public int HP = 2;
    Rigidbody2D rb;
    Vector3 startPosition;
    Coroutine coroutineStun;
    Coroutine goEnemy;
    Door door;
    public float speed = 0.05f;
    Hero hero;

    public Transform downCentr;
    public Transform downRight;
    public Transform downLeft;

    public Transform upCentr;
    public Transform upRight;
    public Transform upLeft;

    public Transform rightCentr;
    public Transform rightDown;
    public Transform rightUp;

    public Transform leftCentr;
    public Transform leftDown;
    public Transform leftUp;

    RaycastHit2D infoDownCenter;
    RaycastHit2D infoDownRight;
    RaycastHit2D infoDownLeft;

    RaycastHit2D infoUpCenter;
    RaycastHit2D infoUpRight;
    RaycastHit2D infoUpLeft;

    RaycastHit2D infoRightCentr;
    RaycastHit2D infoRightDown;
    RaycastHit2D infoRightUp;

    RaycastHit2D infoLeftCentr;
    RaycastHit2D infoLeftDown;
    RaycastHit2D infoLeftUp;





    public bool isGo = true;
    public bool isDown = false;
    public bool isUp = false;
    public bool isRight = true;
    public bool isLeft = false;
    UIControl destroyAll;

    public SoundGame soundGame;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        door = FindObjectOfType<Door>().GetComponent<Door>();
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
        destroyAll = FindObjectOfType<UIControl>().GetComponent<UIControl>();

    }

    void Update()
    {
        AnimationEnemy();
        if (DoorIsOpen() && HP == 2)
            HeroDed();

        if (Input.GetKey(KeyCode.T))
        {
            GetComponent<Collider2D>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            state = 3;
            Invoke("EnemySetActivce", 1f);
        }
        if (destroyAll.GetDestroyAll() == true)
        {
            DestroyAll();
        }
    }

    private void FixedUpdate()
    {
        if (DoorIsOpen() && HP == 2 && destroyAll.GetDestroyAll() == false)
        {
            //rb.bodyType = RigidbodyType2D.Static;
            MoveEnemy();
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    void AnimationEnemy()
    {
        animator.SetInteger("State", state);
    }


    void MoveEnemy()
    {

        while (isGo)
        {
            if (isDown)
            {
                state = 1;
                goEnemy = StartCoroutine(GoEnemy(0, -0.25f));
            }

            else if (isUp)
            {
                state = 5;
                goEnemy = StartCoroutine(GoEnemy(0, 0.25f));
            }
            else if (isRight)
            {
                state = 4;
                goEnemy = StartCoroutine(GoEnemy(0.25f, 0));
            }
            else if (isLeft)
            {
                state = 6;
                goEnemy = StartCoroutine(GoEnemy(-0.25f, 0));
            }
            isGo = false;
        }

        infoDownCenter = Physics2D.Raycast(downCentr.position, Vector2.down, 0.1f, (int)QueryTriggerInteraction.Ignore);
        infoDownRight = Physics2D.Raycast(downRight.position, Vector2.down, 0.1f, (int)QueryTriggerInteraction.Ignore);
        infoDownLeft = Physics2D.Raycast(downLeft.position, Vector2.down, 0.1f, (int)QueryTriggerInteraction.Ignore);

        infoUpCenter = Physics2D.Raycast(upCentr.position, Vector2.up, 0.1f, (int)QueryTriggerInteraction.Ignore);
        infoUpRight = Physics2D.Raycast(upRight.position, Vector2.up, 0.1f, (int)QueryTriggerInteraction.Ignore);
        infoUpLeft = Physics2D.Raycast(upLeft.position, Vector2.up, 0.1f, (int)QueryTriggerInteraction.Ignore);

        infoRightCentr = Physics2D.Raycast(rightCentr.position, Vector2.right, 0.1f, (int)QueryTriggerInteraction.Ignore);
        infoRightDown = Physics2D.Raycast(rightDown.position, Vector2.right, 0.1f, (int)QueryTriggerInteraction.Ignore);
        infoRightUp = Physics2D.Raycast(rightUp.position, Vector2.right, 0.1f, (int)QueryTriggerInteraction.Ignore);

        infoLeftCentr = Physics2D.Raycast(leftCentr.position, Vector2.left, 0.1f, (int)QueryTriggerInteraction.Ignore);
        infoLeftDown = Physics2D.Raycast(leftDown.position, Vector2.left, 0.1f, (int)QueryTriggerInteraction.Ignore);
        infoLeftUp = Physics2D.Raycast(leftUp.position, Vector2.left, 0.1f, (int)QueryTriggerInteraction.Ignore);





        if (isDown)
        {
            //проверить возможность поворотов влево вправо
            if ((!infoRightCentr.collider && !infoRightDown.collider && !infoRightUp.collider &&
                 !infoDownCenter.collider && !infoDownLeft.collider && !infoDownRight.collider &&
                 !infoUpCenter.collider && !infoUpLeft.collider && !infoUpRight.collider &&
                 infoLeftCentr.collider && infoLeftUp.collider && infoLeftDown.collider) ||

                 (!infoLeftCentr.collider && !infoLeftDown.collider && !infoLeftUp.collider &&
                 !infoDownCenter.collider && !infoDownLeft.collider && !infoDownRight.collider &&
                 !infoUpCenter.collider && !infoUpLeft.collider && !infoUpRight.collider &&
                 infoRightCentr.collider && infoRightDown.collider && infoRightUp.collider))
            {
                    StopCoroutine(goEnemy);
                    isDown = false;
                    isGo = false;
                    RorateEnemyLeftRight();
            }


            if (infoDownCenter.collider == true || infoDownLeft.collider ==true || infoDownRight.collider == true)
            {
                isGo = false;
                isDown = false;
                StopCoroutine(goEnemy);
                RotateEnemyAnySide();
                //проверить возможность поворотов в любую сторону
            }
        }
        else if (isUp)
        {
            //проверить возможность поворотов влево вправо
            if ((!infoRightCentr.collider && !infoRightDown.collider && !infoRightUp.collider &&
                 !infoDownCenter.collider && !infoDownLeft.collider && !infoDownRight.collider &&
                 !infoUpCenter.collider && !infoUpLeft.collider && !infoUpRight.collider &&
                 infoLeftCentr.collider && infoLeftUp.collider && infoLeftDown.collider) ||

                 (!infoLeftCentr.collider && !infoLeftDown.collider && !infoLeftUp.collider &&
                 !infoDownCenter.collider && !infoDownLeft.collider && !infoDownRight.collider &&
                 !infoUpCenter.collider && !infoUpLeft.collider && !infoUpRight.collider &&
                 infoRightCentr.collider && infoRightDown.collider && infoRightUp.collider))
            {
                 StopCoroutine(goEnemy);
                 isUp = false;
                 isGo = false;
                 RorateEnemyLeftRight();
            }


            if (infoUpCenter.collider == true || infoUpLeft.collider == true || infoUpRight.collider == true)
            {
                isGo = false;
                isUp = false;
                StopCoroutine(goEnemy);
                RotateEnemyAnySide();
                //проверить возможность поворотов в любую сторону
            }
        }

        else if (isRight)
        {
            ////проверить возможность поворотов вврех вниз
            //if ((!infoRightCentr.collider && !infoRightDown.collider && !infoRightUp.collider &&
            //     infoDownCenter.collider && infoDownLeft.collider && infoDownRight.collider &&
            //     !infoUpCenter.collider && !infoUpLeft.collider && !infoUpRight.collider &&
            //     !infoLeftCentr.collider && !infoLeftUp.collider && !infoLeftDown.collider) ||

            //     (!infoLeftCentr.collider && !infoLeftDown.collider && !infoLeftUp.collider &&
            //     !infoDownCenter.collider && !infoDownLeft.collider && !infoDownRight.collider &&
            //     infoUpCenter.collider && infoUpLeft.collider && infoUpRight.collider &&
            //     !infoRightCentr.collider && !infoRightDown.collider && !infoRightUp.collider))
            //{
            //    if (scetchik == 2 || scetchik == 4)
            //    {
            //        Debug.Log("Можно повернуть");
            //        StopCoroutine(goEnemy);
            //        isDown = false;
            //        isGo = false;
            //        RorateEnemyLeftRight();
            //    }
            //}



            if (infoRightCentr.collider == true || infoRightDown.collider == true || infoRightUp.collider == true)
            {
                isGo = false;
                isRight = false;
                StopCoroutine(goEnemy);
                RotateEnemyAnySide();
                //проверить возможность поворотов в любую сторону
            }
        }

        else if (isLeft)
        {
            ////проверить возможность поворотов вврех вниз
            //if ((!infoRightCentr.collider && !infoRightDown.collider && !infoRightUp.collider &&
            //     infoDownCenter.collider && infoDownLeft.collider && infoDownRight.collider &&
            //     !infoUpCenter.collider && !infoUpLeft.collider && !infoUpRight.collider &&
            //     !infoLeftCentr.collider && !infoLeftUp.collider && !infoLeftDown.collider) ||

            //     (!infoLeftCentr.collider && !infoLeftDown.collider && !infoLeftUp.collider &&
            //     !infoDownCenter.collider && !infoDownLeft.collider && !infoDownRight.collider &&
            //     infoUpCenter.collider && infoUpLeft.collider && infoUpRight.collider &&
            //     !infoRightCentr.collider && !infoRightDown.collider && !infoRightUp.collider))
            //{
            //    if (scetchik == 2 || scetchik == 4)
            //    {
            //        Debug.Log("Можно повернуть");
            //        StopCoroutine(goEnemy);
            //        isDown = false;
            //        isGo = false;
            //        RorateEnemyLeftRight();
            //    }
            //}

            if (infoLeftCentr.collider == true || infoLeftDown.collider == true || infoLeftUp.collider == true)
            {
                isGo = false;
                isLeft = false;
                StopCoroutine(goEnemy);
                RotateEnemyAnySide();
                //проверить возможность поворотов в любую сторону
            }
        }
    }


    IEnumerator GoEnemy(float moveX, float moveY)
    {
        yield return new WaitForSeconds(speed);
        transform.position = new Vector2(transform.position.x+ moveX, transform.position.y + moveY);
        isGo = true;
    }


    private void RotateEnemyAnySide()
    {

        List<string> isSideMove = new List<string>();


        //проверка прохода вниз
        if (!infoDownCenter.collider && !infoDownRight.collider && !infoDownLeft.collider)
        {
            isSideMove.Add("down");
        }

        //проверка прохода вверх
        if (!infoUpCenter.collider && !infoUpRight.collider && !infoUpLeft.collider)
        {
            isSideMove.Add("up");
        }

        //проверка прохода вправо
        if (!infoRightCentr.collider && !infoRightDown.collider && !infoRightUp.collider) 
        {
            isSideMove.Add("right");
        }

        //проверка прохода влево
        if (!infoLeftCentr.collider && !infoLeftDown.collider && !infoLeftUp.collider) 
        {
            isSideMove.Add("left");
        }


        if(isSideMove.Count > 0)
        {

            int a = Random.Range(0, isSideMove.Count);

            if (isSideMove[a] == "up")
            {
                isUp = true;
                isGo = true;
            }
            else if (isSideMove[a] == "down")
            {
                isDown = true;
                isGo = true;
            }
            else if (isSideMove[a] == "left")
            {
                isLeft = true;
                isGo = true;
            }
            else if (isSideMove[a] == "right")
            {
                isRight = true;
                isGo = true;
            }
        }
        else
        {
           //Debug.Log("Стоим на месте");
        }  

    }

    private void RorateEnemyLeftRight()
    {

        List<string> isSideMove = new List<string>();

        if (!infoDownCenter && !infoDownLeft && !infoDownRight)
        {
            isSideMove.Add("isDown");
        }
        if (!infoUpCenter && !infoUpLeft && !infoUpRight)
        {
            isSideMove.Add("isUp");
        }

        if (!infoDownRight && !infoRightDown && !infoRightUp)
        {
            isSideMove.Add("isRight");
        }
        if (!infoLeftCentr && !infoLeftDown && !infoLeftUp)
        {
            isSideMove.Add("isLeft");
        }

        int a = 0; ;
        if (isSideMove.Count > 0)
            a = Random.Range(0, isSideMove.Count);

        if (isSideMove[a] == "isDown")
        {
            transform.position = new Vector2(transform.position.x,transform.position.y - 0.25f);
            isDown = true;
            isGo = true;
        }
        else if (isSideMove[a] == "isUp")
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.25f);
            isUp = true;
            isGo = true;
        }
        else if (isSideMove[a] == "isLeft")
        {
            transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y);
            isLeft = true;
            isGo = true;
        }
        else if (isSideMove[a] == "isRight")
        {
            transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y);
            isRight = true;
            isGo = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bulet")
        {
            HP -= 1;
            SetAnim(HP);
        }
        if (collision.gameObject.tag == "TargetHero" && HP == 1)
        {
            StopAllCoroutines();
        }
    }

    private void HeroDed()
    {
        RaycastHit2D rayLeft = Physics2D.Raycast(leftCentr.position, Vector2.left, 0.7f);
        RaycastHit2D rayLeftDown = Physics2D.Raycast(leftDown.position, Vector2.left, 0.7f);
        RaycastHit2D rayLeftUp = Physics2D.Raycast(leftUp.position, Vector2.left, 0.7f);

        RaycastHit2D rayRight = Physics2D.Raycast(rightCentr.position, Vector2.right, 0.7f);
        RaycastHit2D rayRightDown = Physics2D.Raycast(rightDown.position, Vector2.right, 0.7f);
        RaycastHit2D rayRightUp = Physics2D.Raycast(rightUp.position, Vector2.right, 0.7f);

        RaycastHit2D rayUp = Physics2D.Raycast(upCentr.position, Vector2.up, 0.7f);
        RaycastHit2D rayUpRight = Physics2D.Raycast(upRight.position, Vector2.up, 0.7f);
        RaycastHit2D rayUpLeft = Physics2D.Raycast(upLeft.position, Vector2.up, 0.7f);

        RaycastHit2D rayDown = Physics2D.Raycast(downCentr.position, Vector2.down, 0.7f);
        RaycastHit2D rayDownRight = Physics2D.Raycast(downRight.position, Vector2.down, 0.7f);
        RaycastHit2D rayDownLeft = Physics2D.Raycast(downLeft.position, Vector2.down, 0.7f);

        if (rayLeft)
        {
            if (rayLeft.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }
        if (rayLeftDown)
        {
            if (rayLeftDown.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }
        if (rayLeftUp)
        {
            if (rayLeftUp.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }


        if (rayRight)
        {
            if (rayRight.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }
        if (rayRightDown)
        {
            if (rayRightDown.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }
        if (rayRightUp)
        {
            if (rayRightUp.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }



        if (rayUp)
        {
            if (rayUp.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }
        if (rayUpRight)
        {
            if (rayUpRight.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }
        if (rayUpLeft)
        {
            if (rayUpLeft.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }



        if (rayDown)
        {
            if (rayDown.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }

        if (rayDownRight)
        {
            if (rayDownRight.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }

        if (rayDownLeft)
        {
            if (rayDownLeft.collider.name == "Hero")
            {
                hero.HeroDead();
                hero.gameObject.SetActive(false);
            }
        }

    }

    bool DoorIsOpen()
    {
        return door.GetDoorIsOpen();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetHero" && HP == 1)
        {
            coroutineStun = StartCoroutine(WaitStunEffect());
        }
    }

    void SetAnim(int HP)
    {
        if (HP == 1)
        {
            state = 2;
            soundGame.PlayStunMonstr();
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            rb.bodyType = RigidbodyType2D.Dynamic;
            coroutineStun = StartCoroutine(WaitStunEffect());
        }
        else if (HP == 0)
        {
            soundGame.PlayDeadMonstn();
            GetComponent<Collider2D>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            state = 3;
            Invoke("WaitDeathEnemy", 10f);
        }
    }

    void enebleEnemy()
    {
        gameObject.SetActive(false);
    }

    IEnumerator WaitStunEffect()
    {
        yield return new WaitForSeconds(6f);
        HP = 2;
        state = 0;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void WaitDeathEnemy()
    {
        HP = 2;
        GetComponent<Collider2D>().enabled = true;
        state = 0;
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = startPosition;
    }

    void EnemySetActivce()
    {
        gameObject.SetActive(false);
    }

    public void DestroyAll()
    {
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        state = 3;
        Invoke("EnemySetActivce", 1f);
    }

}
