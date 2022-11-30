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
            transform.position = new Vector2(Mathf.Round(transform.position.x / 0.25f) * 0.25f,
                Mathf.Round(transform.position.y / 0.25f) * 0.25f);

            //Debug.Log("x: "+ transform.position.x);
            //Debug.Log("xOkr: " + Mathf.Round(transform.position.x/0.25f)*0.25f);
            //Debug.Log("y: " + transform.position.y);
            //Debug.Log("yOkr: " + Mathf.Round(transform.position.y/0.25f)*0.25f);
        }

    }
}
