using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

	public Transform Shots;
    //public Transform melee;
    public GameObject meleeAttackTrigger;
    public float shootingRate = 0.0f;
    private float shootCooldown = 0;
    public bool CanAttack = true;
    public Vector2 direction = new Vector2(1, 0);
    public bool gun = false;
    public double gunAmmo = 0;
    public bool hand = true;
    public bool sword = false;
    public string currentWeapon = "hand";
    
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
				shootCooldown = shootingRate;
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
			shootCooldown = shootingRate;
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
