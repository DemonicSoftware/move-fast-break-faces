using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int HP = 5;
    public bool isEnemy = true;
    public float damageCooldown = 1;
    private Animator anim;
    public GameObject damagePanel;
	private AudioSource[] zombieAudio;

    private Vector3 healthScale;
    private SpriteRenderer healthBar;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();

        // Getting the intial scale of the healthbar (whilst the player has full health).
        healthScale = healthBar.transform.localScale;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
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
        }

		if (HP <= 0)
        {
			if (isEnemy)
            {
                if(GetComponent<Animator>() != null)
				    anim.SetBool("dead", true);
                if (GetComponent<FollowPlayer>() != null)
                    GetComponent<FollowPlayer>().enabled = false;
                if (GetComponent<Collider2D>() != null)
                    GetComponent<Collider2D>().enabled = false;
                if(GetComponent<RangedEnemyMovement>() != null)
                    GetComponent<RangedEnemyMovement>().enabled = false;

				zombieAudio = GetComponents<AudioSource>();
                if (GetComponents<AudioSource>() != null)
                {
                    zombieAudio[0].enabled = false;
                    zombieAudio[1].Play();
                }
				// To give the animation some time to run
				StartCoroutine(DestroyObject());
			}

			if (!isEnemy)
            {
                GameController.gameControllerInstance.PlayerDied();
				GetComponent<AudioSource>().Play();
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

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (!isEnemy) {
            if (otherCollider.gameObject.tag == "Enemy" && damageCooldown <= 0)
            {
                damageCooldown = 1;
                Damage(1);
            }
        }
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
}
