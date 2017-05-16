﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawner : MonoBehaviour
{
	[Header("Object creation")]

	// The object to spawn
	// WARNING: take if from the Project panel, NOT the Scene/Hierarchy!
	public GameObject prefabToSpawn;

	[Header("Other options")]

	// Configure the spawning pattern
	public int spawnNumberID = 1;

    private float spawnInterval = 32;
    private float initalWait;
    private bool waited = false;

	private BoxCollider2D boxCollider2D;

	void Start ()
	{
        switch(spawnNumberID)
        {
            case 1:
                initalWait = 0;
                break;
            case 2:
                initalWait = 8;
                break;
            case 3:
                initalWait = 16;
                break;
            case 4:
                initalWait = 24;
                break;

        }


		boxCollider2D = GetComponent<BoxCollider2D>();

		StartCoroutine(SpawnObject());



	}
	
	// This will spawn an object, and then wait some time, then spawn another...
	IEnumerator SpawnObject ()
	{
        if(!waited)
        {
            waited = true;
            yield return new WaitForSeconds(initalWait);
        }
		while(true)
		{
            spawnInterval -= 1f;

			// Create some random numbers
			float randomX = Random.Range (-boxCollider2D.size.x, boxCollider2D.size.x) *.5f;
			float randomY = Random.Range (-boxCollider2D.size.y, boxCollider2D.size.y) *.5f;

			// Generate the new object
			GameObject newObject = Instantiate<GameObject>(prefabToSpawn);
			newObject.transform.position = new Vector2(randomX + this.transform.position.x, randomY + this.transform.position.y);

			// Wait for some time before spawning another object
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
