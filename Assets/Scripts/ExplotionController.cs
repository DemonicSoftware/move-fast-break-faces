using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            otherCollider.gameObject.GetComponent<Health>().Damage(1);
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
