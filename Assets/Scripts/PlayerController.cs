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
    //private Combat combat;
    private Animator anim;
    void Start () {
        endText.enabled = false;
        endScore.enabled = false;
        health = GetComponent<Health>();
        healthText.text = health.getHealth().ToString();
        //combat = GetComponent<Combat>();

        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        // weapon direction calculation, face to mouse cursor 
        Vector3 pointInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToLookAt = (pointInSpace - transform.position).normalized;
        //combat.direction = directionToLookAt; 

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

        //about attack
        if (Input.GetButtonDown("changeWeapon"))
        {
            //combat.changeWeapon();
        }
        bool fire = Input.GetButtonDown("Fire1");

        //if (Input.GetKey(KeyCode.F))
        if (fire)
        {
            //if (combat != null)
            //{
            //    // false because the player is not an enemy
            //    combat.Attack(false);
            //    anim.SetTrigger("punch");
            //}
        }
    }

    bool isDead() {
        return health.getHealth() <= 0;
    }
}
