using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Animator animator;
    private int state = 0;
    private int HP = 2;
    Rigidbody2D rb;
    Vector3 startPosition;
    Coroutine coroutineStun;
    UIControl destroyAll;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        destroyAll = FindObjectOfType<UIControl>().GetComponent<UIControl>();
    }

    void Update()
    {
        AnimationEnemy();
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
        if (collision.gameObject.tag == "TargetHero" && HP==1)
        {
            StopCoroutine(coroutineStun);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetHero" && HP == 1)
        {
            StopCoroutine(coroutineStun);
        }

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
            transform.GetChild(0).gameObject.SetActive(true);
            rb.bodyType = RigidbodyType2D.Dynamic;
            coroutineStun = StartCoroutine(WaitStunEffect());
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
        state = 0;
        transform.GetChild(0).gameObject.SetActive(false);
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    void WaitDeathEnemy()
    {
        HP = 2;
        GetComponent<Collider2D>().enabled = true;
        state = 0;
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
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
