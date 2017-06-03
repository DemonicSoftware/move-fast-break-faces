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

    void FixedUpdate()
    {

        if (IsDead())
        {
            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().enabled = false;
            GameController.gameControllerInstance.PlayerDied();
        }

        if (!IsDead())
        {
            //about attack
            bool fire1 = Input.GetButtonDown("Fire1");

            //if (Input.GetKey(KeyCode.F))
            if (fire1)
            {
                if (combat != null)
                {
                    // false because the player is not an enemy
                    combat.currentWeapon = "hand";
                    combat.Attack(false);
                    anim.SetTrigger("swing");
                }
            }

            bool fire2 = Input.GetButtonDown("Fire2");

            //if (Input.GetKey(KeyCode.F))
            if (fire2)
            {
                if (combat != null)
                {
                    // false because the player is not an enemy
                    combat.currentWeapon = "gun";
                    combat.Attack(false);
                    anim.SetTrigger("swing");
                }
            }
        }

    }

    bool IsDead() {
        return health.getHealth() <= 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ammo")
        {
            combat.gunAmmo += 2;
            Destroy(other.gameObject);
        }
    }

    public int GetAmmoCount() {
        return combat.GetAmmo();
    }

}
