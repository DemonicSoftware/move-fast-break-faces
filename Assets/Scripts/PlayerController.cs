using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Health health;
	public Text healthText;
    public Text scoreText;
    public Text endText;
    public Text endScore;

	void Start () {
        endText.enabled = false;
        endScore.enabled = false;
        health = GetComponent<Health>();
        healthText.text = health.getHealth().ToString();
	}

	void FixedUpdate() {
        healthText.text = "Health: " + health.getHealth().ToString();
        scoreText.text = "Faces Broken: " + (1).ToString();

        if (isDead()) {
            GetComponent<SpriteRenderer>().enabled = false;
            endText.enabled = true;
            endScore.text = "Total Faces Broken: " + (1).ToString();
            endScore.enabled = true;
        }
	}

    bool isDead() {
        return health.getHealth() <= 0;
    }
}
