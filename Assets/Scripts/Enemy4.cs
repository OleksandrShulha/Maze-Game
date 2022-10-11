using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    Animator animator;
    private int state = 1;
    private int HP = 2;
    Rigidbody2D rb;
    Vector3 startPosition;
    Hero hero;
    public GameObject targetBullet;

    public GameObject fireBulet2;

    private int vectorBulet; //1-right, 2-down, 3-left,4-up
    UIControl destroyAll;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
        destroyAll = FindObjectOfType<UIControl>().GetComponent<UIControl>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationEnemy();
        if (HP == 2 && Time.timeScale > 0f)
        {
            AtackEnemy();
        }
        if (destroyAll.GetDestroyAll() == true)
        {
            DestroyAll();
        }
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
            transform.GetChild(0).gameObject.SetActive(true);
            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(WaitStunEffect());
        }
        else if (HP == 0)
        {
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
        state = 1;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void WaitDeathEnemy()
    {
        state = 1;
        HP = 2;
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = startPosition;
    }


    void AtackEnemy()
    {
        if (Mathf.Abs(transform.position.y - hero.transform.position.y) < 1f)
        {
            if (transform.position.x > hero.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
                vectorBulet = 3;
                //if (GameObject.Find("FireBullet2(Clone)") == null)
                //{
                //    Instantiate(fireBulet2, targetBullet.transform.position, transform.rotation);
                //}
                Instantiate(fireBulet2, targetBullet.transform.position, transform.rotation);
            }

            if (transform.position.x < hero.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                vectorBulet = 1;
                //if (GameObject.Find("FireBullet2(Clone)") == null)
                //{
                //    Instantiate(fireBulet2, targetBullet.transform.position, transform.rotation);
                //}
                Instantiate(fireBulet2, targetBullet.transform.position, transform.rotation);
            }
        }
        if (Mathf.Abs(transform.position.x - hero.transform.position.x) < 1f )
        {
            if (transform.position.y > hero.transform.position.y)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                vectorBulet = 2;
                //if (GameObject.Find("FireBullet2(Clone)") == null)
                //{
                //    Instantiate(fireBulet2, targetBullet.transform.position, transform.rotation);
                //}
                Instantiate(fireBulet2, targetBullet.transform.position, transform.rotation);
            }

            if (transform.position.y < hero.transform.position.y)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                vectorBulet = 4;
                //if (GameObject.Find("FireBullet2(Clone)") == null)
                //{
                //    Instantiate(fireBulet2, targetBullet.transform.position, transform.rotation);
                //}
                Instantiate(fireBulet2, targetBullet.transform.position, transform.rotation);
            }
        }
    }

    public int GetVectorBullet()
    {
        return vectorBulet;
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
