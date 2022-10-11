using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButton : MonoBehaviour
{
    // Start is called before the first frame update
    Hero hero;

    void Start()
    {
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hero.GetAmountApples() == 0)
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        }
        else
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
}
