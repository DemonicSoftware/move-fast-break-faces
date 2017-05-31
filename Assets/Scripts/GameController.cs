using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController gameControllerInstance;
	public GameObject player;
	public GameObject gameOverText;
	public GameObject gameWonText;

	public Text enemyCountText;
	public Text ammoCountText;
	public int enemyCount;

	private bool gameLost;
	private bool gameWon;

	void Awake() {
		if (gameControllerInstance == null) {
			gameControllerInstance = this;
		} 
		else if (gameControllerInstance != this) {
			Destroy(gameControllerInstance);
		}
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) {
            Application.LoadLevel("Main Menu");
        }
        if (gameLost && Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (gameWon && Input.GetKey(KeyCode.F)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
        UpdateEnemyCount();
        UpdateAmmoCount();
        CheckWin();
	}

	void UpdateEnemyCount() {
		string t = "Enemies Left:";
		if (Application.loadedLevelName == "Level 4") {
			t = "Boss Health: ";
		}
		enemyCountText.text = t + enemyCount.ToString();
	}

	void UpdateAmmoCount() {
		int ammo = player.GetComponent<PlayerController>().GetAmmoCount();
		ammoCountText.text = "Hammers: " + ammo.ToString();
	}

	void CheckWin() {
		if (enemyCount <= 0) {
			gameWonText.SetActive(true);
			gameWon = true;
		}
	}

	public void EnemyKilled() {
		enemyCount--;
	}

	public void PlayerDied() {
		gameOverText.SetActive(true);
		gameLost = true;
	}

	public void PlayerWon() {
		gameOverText.SetActive(true);
		gameWon = true;
	}

	public int GetEnemyCount() {
		return enemyCount;
	}
}
