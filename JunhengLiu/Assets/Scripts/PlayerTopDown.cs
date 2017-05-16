using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDown : MonoBehaviour {
   public float speed = 50;             //Floating point variable to store the player's movement speed.
   private string direction = "right";
   private Vector2 movement;
   private Rigidbody2D rb2d;   //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Animator animator;
    private Weapon weapon;
   // Use this for initialization
   void Start()
   {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
    }

   //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
   void FixedUpdate()
   {
      float moveHorizontal = Input.GetAxis("Horizontal");

      float moveVertical = Input.GetAxis("Vertical");

      Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (movement.x != 0 || movement.y != 0)
        {
            weapon.direction = new Vector2(getDirection(moveHorizontal), getDirection(moveVertical));
        }      

      rb2d.AddForce(movement * speed);

        if (Input.GetKey(KeyCode.D))
      {
         //rb2d.AddForce(Vector2.right * speed);
         //weapon.direction =  Vector2.right;
        if (direction == "left")
         {
            transform.Rotate(new Vector3(0, 180, 0));
            direction = "right";
         }
      }

      if (Input.GetKey(KeyCode.A))
      {
         //rb2d.AddForce(Vector2.left * speed);
         //weapon.direction = Vector2.left;
         if (direction == "right")
         {
            transform.Rotate(new Vector3(0, 180, 0));
            direction = "left";
         }
      }
        /*
        if (Input.GetKey(KeyCode.S))
        {
            rb2d.AddForce(Vector2.down * speed);
            weapon.direction = Vector2.down;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb2d.AddForce(Vector2.up * speed);
            weapon.direction = Vector2.up;
        }*/
    }

    void Update()
    {
        if (Input.GetButtonDown("changeWeapon"))
        {
            weapon.changeWeapon();
        }
        bool fire = Input.GetButtonDown("Fire1");

        //if (Input.GetKey(KeyCode.F))
        if(fire)
        {
             //weapon = GetComponent<Weapon>();
            if (weapon != null)
            {
 
                //weapon.direction = movement;               
                // false because the player is not an enemy
                weapon.Attack(false);
                animator.SetTrigger("playerChop");
            }
        }

    }

   
    /*void fire()
   {
      shot.SetActive(true);
   }*/

    int getDirection(float x)
    {
        int output = 0;
        if (x > 0)
            output = 1;
        if (x < 0)
            output = -1;
        return output;
    }
}
