using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    Animator animator;
    private int state = 2;
    private int HP = 2;
    Rigidbody2D rb;
    Vector3 startPosition;
    Door door;
    Hero hero;
    public GameObject targetBullet;

    public GameObject fireBulet;

    public bool isDown = false;
    public bool isUp = false;
    public bool isRight = false;
    public bool isLeft = false;
    private int vectorBulet; //1-right, 2-down, 3-left,4-up
    UIControl destroyAll;

    public SoundGame soundGame;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        door = FindObjectOfType<Door>().GetComponent<Door>();
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
        SetVectorBulelt();
        destroyAll = FindObjectOfType<UIControl>().GetComponent<UIControl>();
    }

    void Update()
    {
        AnimationEnemy();
        if (DoorIsOpen() && HP == 2 && Time.timeScale > 0f)
        {
            state = 1;
            transform.GetChild(1).gameObject.SetActive(false);
            AtackEnemy();
        }
        if (destroyAll.GetDestroyAll() == true)
        {
            DestroyAll();
        }
    }

    void SetVectorBulelt()
    {
        if (isDown)
            vectorBulet = 2;
        if (isUp)
            vectorBulet = 4;
        if (isRight)
            vectorBulet = 1;
        if (isLeft)
            vectorBulet = 3;
    }


    void AtackEnemy()
    {
        if (Mathf.Abs(transform.position.y - hero.transform.position.y) < 1f)
        {
            if (vectorBulet == 3 && transform.position.x > hero.transform.position.x)
            {
                if (GameObject.Find("FireBullet(Clone)") == null)
                {
                    Instantiate(fireBulet, targetBullet.transform.position, transform.rotation);
                    soundGame.PlayFireMonstr();
                }
            }

            if (vectorBulet == 1 && transform.position.x < hero.transform.position.x)
            {
                if (GameObject.Find("FireBullet(Clone)") == null)
                {
                    Instantiate(fireBulet, targetBullet.transform.position, transform.rotation);
                    soundGame.PlayFireMonstr();
                }
            }
        }
        if (Mathf.Abs(transform.position.x - hero.transform.position.x) < 1f)
        {
            if (vectorBulet == 2 && transform.position.y > hero.transform.position.y)
            {
                if (GameObject.Find("FireBullet(Clone)") == null)
                {
                    Instantiate(fireBulet, targetBullet.transform.position, transform.rotation);
                    soundGame.PlayFireMonstr();
                }
            }

            if (vectorBulet == 4 && transform.position.y < hero.transform.position.y)
            {
                if (GameObject.Find("FireBullet(Clone)") == null)
                {
                    Instantiate(fireBulet, targetBullet.transform.position, transform.rotation);
                    soundGame.PlayFireMonstr();
                }
            }
        }
    }

    public int GetVectorBullet()
    {
        return vectorBulet;
    }


    void AnimationEnemy()
    {
        animator.SetInteger("State", state);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetHero" && HP == 1)
        {
            StopAllCoroutines();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetHero" && HP == 1)
        {
            StartCoroutine(WaitStunEffect());
        }
    }

    void SetAnim(int HP)
    {
        if (HP == 1)
        {
            state = 2;
            soundGame.PlayStunMonstr();
            transform.GetChild(0).gameObject.SetActive(true);
            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(WaitStunEffect());
        }
        else if (HP == 0)
        {
            soundGame.PlayDeadMonstn();
            GetComponent<Collider2D>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
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
        state = 2;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    void WaitDeathEnemy()
    {
        state = 2;
        HP = 2;
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = startPosition;
    }

    bool DoorIsOpen()
    {
        return door.GetDoorIsOpen();
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
