﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public int HP = 5;
    public bool isEnemy = true;
    public float damageCooldown = 1;
    private Animator anim;
	private AudioSource[] zombieAudio;
    public Text scoreText;

   // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (damageCooldown > 0) {
            damageCooldown -= Time.deltaTime;
        }
    }

	public void Damage(int damageCount) {
		HP -= damageCount;

		if (HP <= 0) {
			if (isEnemy) {
				anim.SetBool("dead", true);
				GetComponent<FollowPlayer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;

				zombieAudio = GetComponents<AudioSource>();
				zombieAudio[0].enabled = false;
				zombieAudio[1].Play();

				// To give the animation some time to run
				StartCoroutine(DestroyObject());
			}

			if (!isEnemy) {
				GetComponent<AudioSource>().Play();
			}
		}
	}

    public Health(int startingHealth) {
        HP = startingHealth;
        //baseHealth = startingHealth;
        //health = baseHealth;
    }

    public void dealDamage(int damage) {
        HP -= damage;
    }

    void restoreHealth(int healing) {
        HP += healing;
    }

    public void refillHealth() {
        HP += 1; //baseHealth;
    }

    public int getHealth() {
        return HP;
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
       // Is this a shot?
        RangeAttack range = otherCollider.gameObject.GetComponent<RangeAttack>();
        MeleeAttack melee = otherCollider.gameObject.GetComponent<MeleeAttack>();
       if (range != null) {
            // Avoid friendly fire
            if (range.isEnemy != isEnemy) {
                Damage(range.damage);

                // Destroy the shot
                Destroy(range.gameObject); 
                // Remember to always target the game object, otherwise you will just remove the script
            }
        }

        if (melee != null) {
            // Avoid friendly fire
          if (melee.isEnemy != isEnemy) {
                Damage(melee.damage);
      
                // Destroy the shot
                //Destroy(melee.gameObject); 
                // Remember to always target the game object, otherwise you will just remove the script
           }
        }
    }

    IEnumerator DestroyObject() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D otherCollider) {
        if (!isEnemy) {
            if (otherCollider.gameObject.tag == "Enemy" && damageCooldown <= 0) {
                damageCooldown = 1;
                Damage(1);
            }
        }
    }
}
