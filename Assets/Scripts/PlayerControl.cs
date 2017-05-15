using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float maxSpeed = 10f;

	//public Enums.MovementType movementType = Enums.MovementType.AllDirections;

	//public Enums.Directions lookAxis = Enums.Directions.Up;

	private Vector2 movement, cachedDirection;
	private float moveHorizontal;
	private float moveVertical;


    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}


	void update() {

		//float move = Input.GetAxis("Horizontal");
		Debug.Log("TEST");

		//Debug.Log(moveHorizontal);




		//Utils.SetAxisTowards(lookAxis, transform, cachedDirection);

	}

	// Update is called once per frame
	void FixedUpdate () {
		moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");
		anim.SetFloat("speed", Mathf.Abs(moveVertical));

        //if (Input.GetKey ("space")) 
        //{
        //	anim.SetTrigger ("punch");
        //}
        if (Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("punch");
        }

        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(0, maxSpeed * Time.deltaTime, 0);
		}

        if(Input.GetKey(KeyCode.S)) {
			transform.Translate(0, -maxSpeed * Time.deltaTime, 0);
		}

        if (Input.GetKey(KeyCode.A)) {
			transform.Translate(-maxSpeed * Time.deltaTime, 0, 0);
		}

        if (Input.GetKey(KeyCode.D)) {
			transform.Translate(maxSpeed * Time.deltaTime, 0, 0);
		}
			
		var mouse = Input.mousePosition;
		var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
		// var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
		// var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		// transform.rotation = Quaternion.Euler(0, 0, angle - 90);
	}
}
