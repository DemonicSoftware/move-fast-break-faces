using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

	public Transform Shots;
    public Vector2 direction = new Vector2(1, 0);
    public bool gun;
    public double gunAmmo = 0;
    public bool hand;
    public string currentWeapon;
    
	void Start () {

        //meleeAttackTrigger.SetActive(false);
        //meleeAttackTrigger.GetComponent<Collider2D>().enabled = false;
        //meleeAttackTrigger.GetComponent<SpriteRenderer>().enabled = false;
    }
		
    public void Attack(bool isEnemy) {
        if (currentWeapon == "gun") {
			
			if (gunAmmo <= 0) {
				gun = false;
			} else {
				gun = true;
			}

			if (gun == true) {
				// Create a new shot
				var shotTransform = Instantiate (Shots) as Transform;
			
				// Assign position
				shotTransform.position = transform.position;
				RangeAttack shot = shotTransform.gameObject.GetComponent<RangeAttack> ();
				shot.direction = this.direction;

				// The is enemy property
				if (shot != null) {
					shot.isEnemy = isEnemy;
				}
					
				gunAmmo -= 1;

			}
				
		}
		else if (currentWeapon == "hand") {
			
		}
			
	}

    public void changeWeapon() {
		if (currentWeapon == "hand") {
			currentWeapon = "gun";
		} else if (currentWeapon == "gun") {
			currentWeapon = "hand";
		} 
	}

    public int GetAmmo() {
        return (int) gunAmmo;
    }
}
