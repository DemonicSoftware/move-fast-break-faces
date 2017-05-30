using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {
    public GameObject lance;
    public bool CanAttack = true;
    public float shootingRate = 0.25f;
    private float shootCooldown = 0;
    public int damage = 1;
    // Use this for initialization
    void Start () {
        lance.GetComponent<Collider2D>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
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
            lance.GetComponent<Collider2D>().enabled = true;
        }
    }
}
