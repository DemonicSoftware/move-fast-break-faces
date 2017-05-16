using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform Shots;
    public Transform hands;
    public float shootingRate = 0.25f;
    private float shootCooldown = 0;
    public bool CanAttack =true;
    public Vector2 direction = new Vector2(1, 0);

    public bool gun = true;
    public double gunAmmo = 10;
    public bool hand = true;
    public bool sword = true;
    public string currentWeapon = "hand";
    void Start () {

   }


   void Update()
   {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if (shootCooldown <= 0f)
        {
            CanAttack = true;
        }
        else
        {
            CanAttack = false;
        }
    }


    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            if (currentWeapon == "gun"&& gun==true)
            {
                shootCooldown = shootingRate;

                // Create a new shot
                var shotTransform = Instantiate(Shots) as Transform;

                // Assign position
                shotTransform.position = transform.position;

                Shot shot = shotTransform.gameObject.GetComponent<Shot>();
                shot.direction = this.direction;
                // The is enemy property
                if (shot != null)
                {
                    shot.isEnemy = isEnemy;
                }
                gunAmmo -= 1;
                if (gunAmmo <= 0)
                {
                    gun = false;
                }
            }
            else if (currentWeapon == "hand")
            {
                shootCooldown = shootingRate;

                // Create a new shot
                var handTransform = Instantiate(hands) as Transform;

                // Assign position
                handTransform.position = transform.position;

                Hand handAttack = handTransform.gameObject.GetComponent<Hand>();
                handAttack.direction = this.direction;
                // The is enemy property
                if (handAttack != null)
                {
                    handAttack.isEnemy = isEnemy;
                }
            }
            else if(currentWeapon == "sword"&& sword == true)
            {

            }
            else
            {
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

    public void changeWeapon()
    {
            if(currentWeapon == "hand" )
            {
                currentWeapon = "sword";
            }
            else if (currentWeapon == "sword" )
            {
                currentWeapon = "gun";
            }
            else if (currentWeapon == "gun" )
            {
                currentWeapon = "hand";
            }
    }
}
