using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private BoxCollider2D bossPatrol;
    public float movementTimer = 3f;
    public float attackCoolDown = 2f;
    public float bossSpeed = 1f;
    public float fireCastCoolDown = 2f;
    public float explosionDelay = 2f;

    public GameObject explosionToSpawn;

    private float movementTimerCount, attackTimerCount, fireCastTimerCount, explosionDelayCount;
      
    private Enums.Players targetPlayer = Enums.Players.Player;
    private Enums.Directions useSide = Enums.Directions.Up;
    private Transform playerTransform;
    private Animator anim;

    float randomX, randomY;
    bool goAfterPlayer = false;
    bool castFire = false;
    int attack = 1;
    int fireCount = 0;

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
        fireCastTimerCount += Time.fixedDeltaTime;
        explosionDelayCount += Time.fixedDeltaTime;

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
                //Utils.SetAxisTowards(useSide, transform, playerTransform.position - transform.position);

                Vector3 vectorToTarget = playerTransform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * 1f);
            }
            else
            {
                //Utils.SetAxisTowards(useSide, transform, playerTransform.position - transform.position);

                Vector3 vectorToTarget = playerTransform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * 1f);

                Attack();
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, new Vector2(randomX, randomY), Time.fixedDeltaTime * bossSpeed));
            Utils.SetAxisTowards(useSide, transform, new Vector3(randomX, randomY, 0) - transform.position);

            if(fireCastTimerCount > fireCastCoolDown)
            {
                anim.SetBool("FireCast", true);
                fireCastTimerCount = 0;
            }
        }

        if(castFire && explosionDelayCount > explosionDelay)
        {
            GameObject newObject = Instantiate<GameObject>(explosionToSpawn);
            newObject.transform.position = playerTransform.position;

            fireCount++;

            castFire = false;
//            if (fireCount >= 3)
//            {
//                castFire = false;
//				fireCount = 0;
//            }
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

    void FireCast()
    {
        EndAnimation();
        castFire = true;
    }

    public void PlayerEnteredPatrol()
    {
        print("Player Enter");
        goAfterPlayer = true;
    }

    public void PlayerExitedPatrol()
    {
        print("Player Exited");
        goAfterPlayer = false;
    }
}
