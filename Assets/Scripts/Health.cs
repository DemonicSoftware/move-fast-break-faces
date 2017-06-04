﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int HP = 5;
    public bool isEnemy = true;
    public float damageCooldown = 0.5f;
    public float damageCooldownCount;
    private bool damageDelaying = false;
    private Animator anim;
    public GameObject damagePanel;
	private AudioSource[] enemyAudio;
	private AudioSource[] playerAudio;
	private bool dead = false;

    private Vector3 healthScale;
    private SpriteRenderer healthBar;

    // Use this for initialization
    void Start ()
    {
        damageCooldownCount = damageCooldown;
        damageDelaying = false;
        anim = GetComponent<Animator>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();

        // Getting the intial scale of the healthbar (whilst the player has full health).
        healthScale = healthBar.transform.localScale;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isEnemy)
        {
            if (damageCooldownCount < damageCooldown)
            {
                damageCooldownCount += Time.deltaTime;
            }
            //if (damageDelaying == true && damageCooldownCount > 0)
            //{
            //    damageCooldownCount -= Time.deltaTime;
            //}
            //else
            //{
            //    //damageDelaying = false;
            //}
        }
        if (dead) 
		{
			transform.rotation = Quaternion.identity;
		}
    }

	public void Damage(int damageCount)
    {
		HP -= damageCount;

        if(!isEnemy)
        {
            StartCoroutine(showDamaged());
            // Update what the health bar looks like.
            UpdateHealthBar();
			playerAudio = GetComponents<AudioSource>();
			playerAudio [Random.Range(0, playerAudio.Length)].Play ();


        }

		if (HP <= 0)
        {
			if (isEnemy)
            {
				// To give the animation some time to run
				Destroy (gameObject, 2f);

				dead = true;
                if(GetComponent<Animator>() != null)
				    anim.SetBool("dead", true);
                if (GetComponent<FollowPlayer>() != null)
                    GetComponent<FollowPlayer>().enabled = false;
                if (GetComponent<Collider2D>() != null)
                    GetComponent<Collider2D>().enabled = false;
                if(GetComponent<RangedEnemyMovement>() != null)
                    GetComponent<RangedEnemyMovement>().enabled = false;

				enemyAudio = GetComponents<AudioSource> ();
				enemyAudio [0].Play ();

				GameController.gameControllerInstance.EnemyKilled();
			}

			if (!isEnemy)
            {
				dead = true;
				anim.SetBool ("dead", true);
                if (GetComponent<LookAtCursor>() != null)
                    GetComponent<LookAtCursor>().enabled = false;
                if (GetComponent<MoveWithArrows>() != null)
                    GetComponent<MoveWithArrows>().enabled = false;
                if (GetComponent<BoxCollider2D>() != null)
                    GetComponent<BoxCollider2D> ().enabled = false;
                GameController.gameControllerInstance.PlayerDied();
			}
		}
	}

    public Health(int startingHealth)
    {
        HP = startingHealth;
        //baseHealth = startingHealth;
        //health = baseHealth;
    }

    public void dealDamage(int damage)
    {
        HP -= damage;
    }

    void restoreHealth(int healing)
    {
        HP += healing;
    }

    public void refillHealth()
    {
        HP += 1; //baseHealth;
    }

    public int getHealth()
    {
        return HP;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
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
      
                //Destroy(melee.gameObject); 
           }
        }
    }
    private void OnCollisionStay2D(Collision2D otherCollider)
    {
        if(isEnemy)
        {
            anim.SetBool("attacking", true);
        }
        if (!isEnemy)
        {
            if (damageDelaying == true && damageCooldownCount > 0)
            {
                damageCooldownCount -= Time.deltaTime * 2;
            }
            if (otherCollider.gameObject.tag == "Enemy")
            {
                damageDelaying = true;
            }
            if (otherCollider.gameObject.tag == "Enemy" && damageCooldownCount <= 0)
            {
                gameObject.GetComponent<Health>().Damage(1);
                gameObject.GetComponent<Health>().StartCoroutine(knockback(0.05f, 500, (transform.position - otherCollider.transform.position).normalized));
                damageCooldownCount = damageCooldown;
             
            }
        }
    }
    private void OnCollisionExit2D(Collision2D otherCollider)
    {
        if (!isEnemy)
        {
            if (otherCollider.gameObject.tag == "Enemy")
            {
                damageDelaying = false;
                //damageCooldownCount = damageCooldown;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
		if (!isEnemy) 
		{
            if (otherCollider.gameObject.tag == "Enemy")
            {
                damageDelaying = true;
                //damageCooldownCount = damageCooldown;
            }
        }
        if (isEnemy) 
		{
        }

    }
	public void finishedAttacking()
	{
		anim.SetBool ("attacking", false);
	}

    public void UpdateHealthBar()
    {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - (HP * 20) * 0.01f);

        // Set the scale of the health bar to be proportional to the player's health.
        healthBar.transform.localScale = new Vector3(healthScale.x * (HP * 20) * 0.01f, healthScale.y, 1);
    }

    private IEnumerator showDamaged()
    {
        damagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damagePanel.SetActive(false);
    }

    public IEnumerator knockback(float knockbackDur, float knockbackPower, Vector3 knockbackDir)
    {
        float timer = 0;
        while (knockbackDur > timer)
        {
            timer += Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(new Vector3(knockbackDir.x * knockbackPower, knockbackDir.y * knockbackPower, transform.position.z));
        }
        yield return 0;
    }
}
