using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Health health;
    private Combat combat;
    private Animator anim;
    void Start () {
        health = GetComponent<Health>();
        combat = GetComponent<Combat>();

        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        // weapon direction calculation, face to mouse cursor 
        Vector3 pointInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToLookAt = (pointInSpace - transform.position).normalized;
        combat.direction = directionToLookAt; 

    }

    void FixedUpdate() {

        if (IsDead())
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GameController.gameControllerInstance.PlayerDied();
        }

        //about attack
        if (Input.GetButtonDown("changeWeapon"))
        {
            combat.changeWeapon();
        }
        bool fire = Input.GetButtonDown("Fire1");

        //if (Input.GetKey(KeyCode.F))
        if (fire)
        {
            if (combat != null)
            {
                // false because the player is not an enemy
                combat.Attack(false);
                anim.SetTrigger("swing");
            }
        }
    }

    bool IsDead() {
        return health.getHealth() <= 0;
    }
}
