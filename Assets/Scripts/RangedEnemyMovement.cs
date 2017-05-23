using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class RangedEnemyMovement : Physics2DObject
{
    public Transform Shots;
    public float shootingRate = 3;
    private float shootCooldown = 0;
    public bool CanAttack = true;
    public Vector2 direction = new Vector2(1, 0);

    // This is the player the object is going to move towards
    public Enums.Players targetPlayer = Enums.Players.Player;

    [Header("Movement")]
    // Speed used to move towards the player
    public float speed = 1f;

    // Used to decide if the object will look at the player while pursuing him
    public bool lookAtPlayer = true;

    // The direction that will face the player
    public Enums.Directions useSide = Enums.Directions.Up;

    private Transform playerTransform;

    void Start()
    {
        // Find the player in the scene and store a reference for later use
        playerTransform = GameObject.FindGameObjectWithTag(targetPlayer.ToString()).transform;
    }

    private void Update()
    {
        if (CanAttack == false)
        {
            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
            if (shootCooldown <= 0f)
            {
                CanAttack = true;
            }
            else
            {
                CanAttack = false;
            }
        }

    }
    // FixedUpdate is called once per frame
    void FixedUpdate()
    {

        //print(Vector2.Distance(transform.position, playerTransform.position));

        if(Vector2.Distance(transform.position, playerTransform.position) > Random.Range(12, 14))
        {
            //Move towards the player
            rigidbody2D.MovePosition(Vector2.Lerp(transform.position, playerTransform.position, Time.fixedDeltaTime * speed));
            //look towards the player
            if (lookAtPlayer)
            {
                Utils.SetAxisTowards(useSide, transform, playerTransform.position - transform.position);
            }
        }

        Attack(true);
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
                shootCooldown = shootingRate;
                CanAttack = false;
                // Create a new shot
                var shotTransform = Instantiate(Shots) as Transform;

                // Assign position
                shotTransform.position = transform.position;

                RangeAttack shot = shotTransform.gameObject.GetComponent<RangeAttack>();
            shot.speed = 1;
            shot.direction = playerTransform.position - transform.position;//this.direction;

                // The is enemy property
                if (shot != null)
                {
                    shot.isEnemy = isEnemy;
                }
            
        }
    }
}
