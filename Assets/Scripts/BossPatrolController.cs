using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrolController : MonoBehaviour
{
    GameObject bossEnemy;

	// Use this for initialization
	void Start ()
    {
		bossEnemy = GameObject.Find("BossEnemy");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {           
            bossEnemy.GetComponent<BossController>().PlayerEnteredPatrol();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bossEnemy.GetComponent<BossController>().PlayerExitedPatrol();
        }
    }
}
