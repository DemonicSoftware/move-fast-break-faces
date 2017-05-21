using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4EnemySpawner : MonoBehaviour
{
    [Header("Object creation")]

    // The object to spawn
    // WARNING: take if from the Project panel, NOT the Scene/Hierarchy!
    public GameObject prefabToSpawn;

    public float spawnAmount = 40;
    int amountSpawned;
    private BoxCollider2D boxCollider2D;

    // Use this for initialization
    void Start ()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (amountSpawned < spawnAmount)
        {
            // Create some random numbers
            float randomX = Random.Range(-boxCollider2D.size.x, boxCollider2D.size.x) * .5f;
            float randomY = Random.Range(-boxCollider2D.size.y, boxCollider2D.size.y) * .5f;

            // Generate the new object
            GameObject newObject = Instantiate<GameObject>(prefabToSpawn);
            newObject.transform.position = new Vector2(randomX + this.transform.position.x, randomY + this.transform.position.y);

            amountSpawned++;
        }
	}
}
