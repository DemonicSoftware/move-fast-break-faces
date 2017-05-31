using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour {

	public void LoadLevelOne() {
		Application.LoadLevel("Level 1");
	}

	public void LoadLevelTwo() {
		Application.LoadLevel("Level 2");
	}

	public void LoadLevelThree() {
		Application.LoadLevel("Level 3");
	}

	public void LoadLevelFour() {
		Application.LoadLevel("Level 4");
	}

	public void LoadLevelFive() {
		Application.LoadLevel("Level 5");
	}

	public void LoadMainMenu() {
		Application.LoadLevel("Main Menu");
	}

	public void LoadNextLevel() {
		string scene = Application.loadedLevelName;

		switch(scene) {
			case "Level 1":
				LoadLevelTwo();
				break;

			case "Level 2":
				LoadLevelThree();
				break;

			case "Level 3":
				LoadLevelFour();
				break;

			case "Level 4":
				LoadLevelFive();
				break;

			case "Level 5":
				LoadMainMenu();
				break;
		}
	}
}
