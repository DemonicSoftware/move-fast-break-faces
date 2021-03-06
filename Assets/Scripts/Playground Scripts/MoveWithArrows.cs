﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveWithArrows : Physics2DObject {
	[Header("Input keys")]
	public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;

	[Header("Movement")]
	public float speed = 5f;
	public Enums.MovementType movementType = Enums.MovementType.AllDirections;

	[Header("Orientation")]
	public bool orientToDirection = false;
	// The direction that will face the player
	public Enums.Directions lookAxis = Enums.Directions.Up;

	private Vector2 movement, cachedDirection;
	private float moveHorizontal;
	private float moveVertical;

    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCd = 0.3f;


    Animator anim;
    // about attack

    void Start() {
        anim = GetComponent<Animator>();
    }


    // Update gets called every frame
    void Update ()
    {
        // Moving with the arrow keys
        //if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        //{
        //    moveHorizontal = Input.GetAxis("Horizontal");
        //    moveVertical = Input.GetAxis("Vertical");
        //}
        //else
        //{
        //    moveHorizontal = Input.GetAxis("Horizontal2");
        //    moveVertical = Input.GetAxis("Vertical2");
        //}
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1;
        }           
        else if(Input.GetKey(KeyCode.S))
        {
            moveVertical = -1;
        }            
        else
        {
            moveVertical = 0;
        }
            

        if (Input.GetKey(KeyCode.A))
            moveHorizontal = -1;
        else if (Input.GetKey(KeyCode.D))
            moveHorizontal = 1;
        else
            moveHorizontal = 0;



        //zero-out the axes that are not needed, if the movement is constrained
        switch (movementType) {
			case Enums.MovementType.OnlyHorizontal:
				moveVertical = 0f;
				break;
			case Enums.MovementType.OnlyVertical:
				moveHorizontal = 0f;
				break;
		}
			
		movement = new Vector2(moveHorizontal, moveVertical);

        //rotate the gameObject towards the direction of movement
        //the axis to look can be decided with the "axis" variable
        if (orientToDirection)
        {
			if(movement.sqrMagnitude >= 0.01f)
			{
				cachedDirection = movement;
			}
			Utils.SetAxisTowards(lookAxis, transform, cachedDirection);
		}

        if (Input.GetMouseButtonDown(0))
        {
            attacking = true;
            attackTimer = attackCd;
        }
    }



	// FixedUpdate is called every frame when the physics are calculated
	void FixedUpdate ()
    {
        anim.SetFloat("speed", Mathf.Abs(moveVertical + moveHorizontal));

        

        if(attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
            }
        }
        anim.SetBool("swing", attacking);

        // Apply the force to the Rigidbody2d
        rigidbody2D.AddForce(movement * speed * 10f);


    }

    int getDirection(float x) {
        int output = 0;
        if (x > 0)
            output = 1;
        if (x < 0)
            output = -1;
        return output;
    }
}