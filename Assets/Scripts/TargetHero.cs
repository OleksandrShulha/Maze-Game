using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHero : MonoBehaviour
{
    Hero hero;
    private void Start()
    {
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            collision.gameObject.SetActive(false);
            hero.SetAmountApples(2);
        }

        if (collision.gameObject.tag == "Key")
        {
            collision.gameObject.SetActive(false);
            hero.SetAmountKey(1);
            hero.ExamWinLvl();
        }

    }
}
