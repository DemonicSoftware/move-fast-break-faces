using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private BoxCollider2D bossPatrol;
    public float movementTimer = 3f;
    public float attackCoolDown = 2f;


    private float movementTimerCount, attackTimerCount;
    private float bossSpeed = 1f;  
    private Enums.Players targetPlayer = Enums.Players.Player;
    private Enums.Directions useSide = Enums.Directions.Up;
    private Transform playerTransform;
    private Animator anim;

    float randomX, randomY;
    bool goAfterPlayer = false;
    int attack = 1;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        bossPatrol = GameObject.Find("BossPatrol").GetComponent<BoxCollider2D>();
        playerTransform = GameObject.FindGameObjectWithTag(targetPlayer.ToString()).transform;
        randomX = bossPatrol.transform.position.x + Random.Range(-bossPatrol.size.x, bossPatrol.size.x) * .5f;
        randomY = bossPatrol.transform.position.y + Random.Range(-bossPatrol.size.y, bossPatrol.size.y) * .5f;
        goAfterPlayer = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementTimerCount += Time.fixedDeltaTime;
        attackTimerCount += Time.fixedDeltaTime;

        if (movementTimerCount > movementTimer)
        {
            // Create some random numbers
            randomX = bossPatrol.transform.position.x + (Random.Range(-bossPatrol.size.x, bossPatrol.size.x) * .5f);
            randomY = bossPatrol.transform.position.y + Random.Range(-bossPatrol.size.y, bossPatrol.size.y) * .5f;
            movementTimerCount = 0;
        }

        if(goAfterPlayer)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > 3)
            {
                //Move towards the player
                GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, playerTransform.position, Time.fixedDeltaTime * bossSpeed));

                //look towards the player
                Utils.SetAxisTowards(useSide, transform, playerTransform.position - transform.position);
            }
            else
            {
                Utils.SetAxisTowards(useSide, transform, playerTransform.position - transform.position);
                Attack();
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, new Vector2(randomX, randomY), Time.fixedDeltaTime * bossSpeed));
            Utils.SetAxisTowards(useSide, transform, new Vector3(randomX, randomY, 0) - transform.position);
        }

       
    }

    void Attack()
    {
        if(attackTimerCount > attackCoolDown)
        {
            attackTimerCount = 0;
            switch (attack)
            {
                case 1:
                    anim.SetBool("bigHit", true);
                    attack++;
                    break;
                case 2:
                    anim.SetBool("bigHit", true);
                    attack++;
                    break;
                case 3:
                    anim.SetBool("bigSwing", true);
                    attack = 1;
                    break;
            }
        }    
    }

    void EndAnimation()
    {
        anim.SetBool("bigHit", false);
        anim.SetBool("bigSwing", false);
        anim.SetBool("FireCast", false);
    }

    public void PlayerEnteredPatrol()
    {
        goAfterPlayer = true;
    }

    public void PlayerExitedPatrol()
    {
        goAfterPlayer = false;
    }
}
