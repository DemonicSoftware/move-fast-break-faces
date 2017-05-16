using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public int HP = 5;
    public bool isEnemy = true;
    public float damageCooldown = 1;
    private Animator anim;
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
                GetComponent<AudioSource>().enabled = false;

                // To give the animation some time to run
                StartCoroutine(DestroyObject());
            }
            if (!isEnemy) {
                
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
        Shot shot = otherCollider.gameObject.GetComponent<Shot>();
        Hand hand = otherCollider.gameObject.GetComponent<Hand>();
        if (shot != null) {
            // Avoid friendly fire
            if (shot.isEnemy != isEnemy) {
                Damage(shot.damage);

                // Destroy the shot
                Destroy(shot.gameObject); 
                // Remember to always target the game object, otherwise you will just remove the script
            }
        }

        if (hand != null) {
            // Avoid friendly fire
            if (hand.isEnemy != isEnemy) {
                Damage(hand.damage);
      
                // Destroy the shot
                Destroy(hand.gameObject); 
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
