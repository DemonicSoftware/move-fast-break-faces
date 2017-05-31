using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour {

	public void LoadLevelOne() {
		Application.LoadLevel("Level 1");
		Time.timeScale = 1.0f;
	}

	public void LoadLevelTwo() {
		Application.LoadLevel("Level 2");
		Time.timeScale = 1.0f;
	}

	public void LoadLevelThree() {
		Application.LoadLevel("Level 3");
		Time.timeScale = 1.0f;
	}

	public void LoadLevelFour() {
		Application.LoadLevel("Level 4");
		Time.timeScale = 1.0f;
	}

	public void LoadLevelFive() {
		Application.LoadLevel("Level 5");
		Time.timeScale = 1.0f;
	}

	public void LoadMainMenu() {
		Application.LoadLevel("Main Menu");
		Time.timeScale = 1.0f;
	}
}
