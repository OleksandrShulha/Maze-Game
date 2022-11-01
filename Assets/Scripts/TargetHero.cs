using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TargetHero : MonoBehaviour
{
    Hero hero;
    public SoundGame soundGame;


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
            soundGame.PlaykayTakeSound();
            collision.gameObject.SetActive(false);
            hero.SetAmountKey(1);
            hero.ExamWinLvl();
        }

    }
}
