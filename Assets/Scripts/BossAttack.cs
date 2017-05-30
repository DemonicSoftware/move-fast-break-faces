using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public bool CanAttack = true;
    public float shootingRate = 0.25f;
    private float shootCooldown = 0;
    public int damage = 1;
    public int HP = 20;
    private bool dead = false;
    private bool isEnemy = true;
    private Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (dead)
        {
            transform.rotation = Quaternion.identity;
        }

        if (CanAttack == false)
        {
            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
            if (shootCooldown <= 0f)
            {
                CanAttack = true;
                //lance.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                CanAttack = false;
            }
        }
    }

    void Attack()
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;
        }
    }

    void Damage(int damageCount)
    {
        HP -= damageCount;
        if(HP <= 0)
        {
            dead = true;
            if (GetComponent<Animator>() != null)
                anim.SetBool("dead", true);
            if (GetComponent<Collider2D>() != null)
                GetComponent<Collider2D>().enabled = false;
            GetComponent<BossController>().enabled = false;
            GameController.gameControllerInstance.EnemyKilled();
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a shot?
        RangeAttack range = otherCollider.gameObject.GetComponent<RangeAttack>();
        MeleeAttack melee = otherCollider.gameObject.GetComponent<MeleeAttack>();
        if (range != null)
        {
            // Avoid friendly fire
            if (range.isEnemy != isEnemy)
            {
                Damage(range.damage);

                // Destroy the shot
                Destroy(range.gameObject);
                // Remember to always target the game object, otherwise you will just remove the script
            }
        }

        if (melee != null)
        {
            // Avoid friendly fire
            if (melee.isEnemy != isEnemy)
            {
                Damage(melee.damage);

                //Destroy(melee.gameObject); 
            }
        }
    }
}
