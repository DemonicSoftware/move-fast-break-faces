using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour {
	
	public int damage = 1;
	public float speed = 100;
	public Vector2 direction = new Vector2(1, 0);
	private Vector2 movement;
	private Rigidbody2D rb2d;
	public bool isEnemy = false;
	public int time = 20;
	public Enums.Directions useSide = Enums.Directions.Up;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		movement = new Vector2(direction.x, direction.y);

		// Limited time to live to avoid any leak
		Destroy(gameObject, time); //sec
	}
		
	void Update() {
		Vector3 pointInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //adjust the Z, because the Camera is at -10f on the Z!
        pointInSpace.z = 0f;
        //calculate the direction
        Vector3 directionToLookAt = (pointInSpace - transform.position).normalized;
        //orient the object
        Utils.SetAxisTowards(useSide, transform, directionToLookAt);
        transform.Rotate(new Vector3(0, 0, 90));
	}
		
	void FixedUpdate() {
		rb2d.AddForce(movement.normalized * speed);
	}

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
