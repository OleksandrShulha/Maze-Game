using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBulet : MonoBehaviour
{

    public float speed = 15f;
    int directionBulet;
    Vector3 dir;
    Hero hero;

    private void Start()
    {
        StartDirectionBulet();
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
    }

    private void Update()
    {
        MoveBulet();
    }




    void MoveBulet()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime * speed);
    }

    void StartDirectionBulet()
    {
        directionBulet = FindObjectOfType<Hero>().GetComponent<Hero>().GetVectorBulet();
        if (directionBulet == 1)
        {
            dir = new Vector3(speed, 0.0f, 0.0f);
        }
        else if (directionBulet == 3)
        {
            dir = new Vector3(-speed, 0.0f, 0.0f);
        }
        else if (directionBulet == 4)
        {
            dir = new Vector3(0.0f, speed, 0.0f);
        }
        else if (directionBulet == 2)
        {
            dir = new Vector3(0.0f, -speed, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "TargetHero" && collision.gameObject.tag != "Hero")
        {
            hero.SetIsBulet(false);
            Destroy(gameObject);
        }

    }

}

