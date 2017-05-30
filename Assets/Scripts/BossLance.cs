using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLance : MonoBehaviour {
    public int damage = 1;
    private bool isEnemy = true;
    // Use this for initialization
    void Start () {
        //GetComponent<Collider2D>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            otherCollider.gameObject.GetComponent < Health > ().Damage(damage);
            StartCoroutine(otherCollider.gameObject.GetComponent<Health>().knockback(0.05f, 500, otherCollider.transform.position - transform.position));
        }
    }
}
