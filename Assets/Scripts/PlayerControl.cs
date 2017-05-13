using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float maxSpeed = 10f;

	//public Enums.MovementType movementType = Enums.MovementType.AllDirections;

	//public Enums.Directions lookAxis = Enums.Directions.Up;

	private Vector2 movement, cachedDirection;
	private float moveHorizontal;
	private float moveVertical;


    Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}


	void update()
	{

		//float move = Input.GetAxis("Horizontal");
		Debug.Log("TEST");

		//Debug.Log(moveHorizontal);




		//Utils.SetAxisTowards(lookAxis, transform, cachedDirection);


	}

	// Update is called once per frame
	void FixedUpdate ()
    {
		//moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");
		anim.SetFloat("speed", Mathf.Abs(moveVertical));

		if (Input.GetKey ("space")) 
		{
			anim.SetTrigger ("punch");
		}

		if(Input.GetKey("up"))
		{
			GetComponent<Rigidbody2D>().AddForce(transform.up * maxSpeed * 10);
		}
		if(Input.GetKey("down"))
		{
			GetComponent<Rigidbody2D>().AddForce(-transform.up * maxSpeed * 10);
		}
		if(Input.GetKey("right"))
		{
			transform.Rotate(0, 0, -5);
		}
		if(Input.GetKey("left"))
		{
			transform.Rotate(0, 0, 5);
		}

	}
}
