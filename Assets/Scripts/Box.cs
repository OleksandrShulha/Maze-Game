using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
   
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetHero")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetHero")
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

    }
}
