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

    Animator anim;
    // about attack
    private Weapon weapon;

    void Start() {
        anim = GetComponent<Animator>();
        weapon = GetComponent<Weapon>();
    }


    // Update gets called every frame
    void Update () {	
		// Moving with the arrow keys
		if(typeOfControl == Enums.KeyGroups.ArrowKeys) {
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}
		else {
			moveHorizontal = Input.GetAxis("Horizontal2");
			moveVertical = Input.GetAxis("Vertical2");
		}

		//zero-out the axes that are not needed, if the movement is constrained
		switch(movementType) {
			case Enums.MovementType.OnlyHorizontal:
				moveVertical = 0f;
				break;
			case Enums.MovementType.OnlyVertical:
				moveHorizontal = 0f;
				break;
		}
			
		movement = new Vector2(moveHorizontal, moveVertical);
 		// weapon direction calculation, face to mouse cursor 
        Vector3 pointInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToLookAt = (pointInSpace - transform.position).normalized;
        weapon.direction = directionToLookAt; new Vector2(getDirection(moveHorizontal), getDirection(moveVertical));

        //rotate the gameObject towards the direction of movement
        //the axis to look can be decided with the "axis" variable
        if (orientToDirection) {
			if(movement.sqrMagnitude >= 0.01f)
			{
				cachedDirection = movement;
			}
			Utils.SetAxisTowards(lookAxis, transform, cachedDirection);
		}
	}



	// FixedUpdate is called every frame when the physics are calculated
	void FixedUpdate () {
        anim.SetFloat("speed", Mathf.Abs(moveVertical + moveHorizontal));

        if (Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("punch");
        }
        // Apply the force to the Rigidbody2d
        rigidbody2D.AddForce(movement * speed * 10f);

        //about attack
        if (Input.GetButtonDown("changeWeapon")) {
            weapon.changeWeapon();
        }
        bool fire = Input.GetButtonDown("Fire1");

        //if (Input.GetKey(KeyCode.F))
        if (fire) {
            //weapon = GetComponent<Weapon>();
            if (weapon != null)
            {

                //weapon.direction = movement;               
                // false because the player is not an enemy
                weapon.Attack(false);
                anim.SetTrigger("punch");
            }
        }
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