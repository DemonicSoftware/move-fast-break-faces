using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLance : MonoBehaviour {
    public int damage = 1;

    // Use this for initialization
    void Start () {
        GetComponent<Collider2D>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
