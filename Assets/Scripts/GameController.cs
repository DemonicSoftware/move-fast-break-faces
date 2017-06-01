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
	private LevelSwitcher levelSwitcher;

	void Awake() {
		if (gameControllerInstance == null) {
			gameControllerInstance = this;
		} 
		else if (gameControllerInstance != this) {
			Destroy(gameControllerInstance);
		}
	}
	
	void Update () {
		levelSwitcher = new LevelSwitcher();

		if (Input.GetKey(KeyCode.Escape)) {
            Application.LoadLevel("Main Menu");
        }
        if (gameLost && Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (gameWon && Input.GetKey(KeyCode.F)) {
            levelSwitcher.LoadNextLevel();
            Time.timeScale = 1;
        }
        UpdateEnemyCount();
        UpdateAmmoCount();
        if(SceneManager.GetActiveScene().name != "Level 5")
        {
            CheckWin();
        }
        
	}

	void UpdateEnemyCount() {
		string t = "Enemies Left:";
		if (Application.loadedLevelName == "Level 4") {
			t = "Boss Health: ";
		}
        if(enemyCountText != null)
		    enemyCountText.text = t + enemyCount.ToString();
	}

	void UpdateAmmoCount()
    {
		int ammo = player.GetComponent<PlayerController>().GetAmmoCount();
        if(ammoCountText != null)
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

	public void PlayerWon()
    {
        gameWonText.SetActive(true);
		gameWon = true;
        GameObject.Find("HealthBar").GetComponent<SpriteRenderer>().enabled = false;
        Time.timeScale = 0;
	}

	public int GetEnemyCount() {
		return enemyCount;
	}
}
