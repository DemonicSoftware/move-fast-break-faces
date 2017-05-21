using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int damage = 1;
    //public float speed = 100;
   // public float range = 1;
    public Vector2 direction = new Vector2(1, 0);
    //private Vector2 movement;
    //private Rigidbody2D rb2d;
    public bool isEnemy = false;
    //public int time = 1;

    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //movement = new Vector2(direction.x, direction.y);
        // rb2d = GetComponent<Rigidbody2D>();

        // Limited time to live to avoid any leak
        //Destroy(gameObject, time); //sec
        //gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        //if (range > 0) {
        // rb2d.AddForce(movement * speed);
        //rb2d.position = rb2d.position + movement;
        // range -= 1;
        // }
        //else {
        //Destroy(gameObject);
        //gameObject.SetActive(false);
        //}

    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
