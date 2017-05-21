using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

	public Transform range;
    //public Transform melee;
    public GameObject meleeAttackTrigger;
    public float shootingRate = 0.25f;
    private float shootCooldown = 0;
    public bool CanAttack =true;
    public Vector2 direction = new Vector2(1, 0);
    public bool gun = true;
    public double gunAmmo = 100;
    public bool hand = true;
    public bool sword = false;
    public string currentWeapon = "hand";
    
	void Start () {

        //meleeAttackTrigger.SetActive(false);
        //meleeAttackTrigger.GetComponent<Collider2D>().enabled = false;
        //meleeAttackTrigger.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update() {
        if (CanAttack == false)
        {
            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
            if (shootCooldown <= 0f)
            {
                CanAttack = true;
                meleeAttackTrigger.GetComponent<Collider2D>().enabled = false;
                meleeAttackTrigger.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                CanAttack = false;
            }
        }
    }


    public void Attack(bool isEnemy) {
        if (CanAttack) {
            if (currentWeapon == "gun"&& gun==true) {
				
                shootCooldown = shootingRate;
                CanAttack = false;
                // Create a new shot
                var shotTransform = Instantiate(range) as Transform;

                // Assign position
                shotTransform.position = transform.position;

                RangeAttack shot = shotTransform.gameObject.GetComponent<RangeAttack>();
                shot.direction = this.direction;

                // The is enemy property
                if (shot != null) {
                    shot.isEnemy = isEnemy;
                }

                gunAmmo -= 1;

                if (gunAmmo <= 0) {
                    gun = false;
                }
            }
            else if (currentWeapon == "hand") {
                shootCooldown = shootingRate;
                CanAttack = false;
                meleeAttackTrigger.GetComponent<Collider2D>().enabled = true;
                meleeAttackTrigger.GetComponent<SpriteRenderer>().enabled = true;

                // The is enemy property
                // if (handAttack != null) {
                  //   handAttack.isEnemy = isEnemy;
                 //}
            }
            //else if(currentWeapon == "sword"&& sword == true) {

           // }
            else {
                changeWeapon();
            }
        }
    }

    /*public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }*/

    public void changeWeapon() {
		if (currentWeapon == "hand") {
			currentWeapon = "sword";
		} else if (currentWeapon == "sword") {
			currentWeapon = "gun";
		} else if (currentWeapon == "gun") {
			currentWeapon = "hand";
		}
	}
}
