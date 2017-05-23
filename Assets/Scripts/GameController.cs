using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController gameControllerInstance;
	public GameObject gameOverText;
	public bool gameLost;

	void Awake() {
		if (gameControllerInstance == null) {
			gameControllerInstance = this;
		} 
		else if (gameControllerInstance != this) {
			Destroy(gameControllerInstance);
		}
	}
	
	void Update () {
        if (gameLost && Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

	public void PlayerDied() {
		gameOverText.SetActive(true);
		gameLost = true;
	}
}
