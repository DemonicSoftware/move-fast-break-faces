using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Health health;
	public Text healthText;

	void Start () {
        //health = new Health(5);
        health = GetComponent<Health>();
        healthText.text = health.getHealth().ToString();
	}


	void update() {
	}

	void FixedUpdate() {
        if (Input.GetKey(KeyCode.X)) {
            health.dealDamage(1);
        }

        if (Input.GetKey(KeyCode.Z)) {
            health.refillHealth();
        }
        healthText.text = health.getHealth().ToString();
	}
}
