using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet2 : MonoBehaviour
{
    float speed = 40f;
    Vector3 dir;
    Hero hero;

    // Start is called before the first frame update

    private void Awake()
    {
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
    }
    void Start()
    {
        StartDirectionBulet();
    }

    private void FixedUpdate()
    {
        MoveBulet();
    }

    void MoveBulet()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime * speed);
    }

    void StartDirectionBulet()
    {
        if (Mathf.Abs(transform.position.y - hero.transform.position.y) < 1f)
        {
            if (transform.position.x > hero.transform.position.x)
            {
                dir = new Vector3(-speed, 0.0f, 0.0f);
            }

            if (transform.position.x < hero.transform.position.x)
            {
                dir = new Vector3(speed, 0.0f, 0.0f);
            }
        }
        if (Mathf.Abs(transform.position.x - hero.transform.position.x) < 1f)
        {
            if (transform.position.y > hero.transform.position.y)
            {
                dir = new Vector3(0.0f, -speed, 0.0f);
            }

            if (transform.position.y < hero.transform.position.y)
            {
                dir = new Vector3(0.0f, speed, 0.0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
            FindObjectOfType<Hero>().GetComponent<Hero>().HeroDead();
        Destroy(gameObject);
    }
}
