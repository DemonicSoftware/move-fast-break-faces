using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

   public float speed = 100;
   public float jumppower = 2000;
   private int jump = 1;      //Floating point variable to store the player's movement speed.
   private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
   private string direction = "right";
   public GameObject bullet;
   public LayerMask GroundLayer;

   bool IsGrounded()
   {
      Vector2 position = transform.position;
      Vector2 direction = Vector2.down;
      float distance = 1.0f;

      RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, GroundLayer);
      if (hit.collider != null)
      {
         return true;
      }

      return false;
   }  

   void Start () {
      //Get and store a reference to the Rigidbody2D component so that we can access it.
      rb2d = GetComponent<Rigidbody2D>();
   }
	
	// Update is called once per frame
	void FixedUpdate () {

      if (Input.GetKey(KeyCode.D))
      {
         rb2d.AddForce(Vector2.right * speed);
         if (direction == "left")
         {
            transform.Rotate(new Vector3(0, 180, 0));
            direction = "right";
         }
      }

      if (Input.GetKey(KeyCode.A))
      {
         rb2d.AddForce(Vector2.left * speed);
         if (direction == "right")
         {
            transform.Rotate(new Vector3(0, 180, 0));
            direction = "left";
         }
      }

      if (Input.GetKeyDown(KeyCode.Space))
      {
         if (IsGrounded())
         {
            jump = 1;
            rb2d.AddForce(Vector2.up * jumppower);
         }
         else if (jump > 0)
         {
            rb2d.AddForce(Vector2.up * jumppower);
            jump = jump - 1;
         }
      }

   }
}
