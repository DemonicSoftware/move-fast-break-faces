using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : ScriptableObject
{

	public void LoadLevelOne()
    {

        SceneManager.LoadScene("Level 1");
	}

	public void LoadLevelTwo() {
        SceneManager.LoadScene("Level 2");
	}

	public void LoadLevelThree() {
        SceneManager.LoadScene("Level 3");
	}

	public void LoadLevelFour() {
        SceneManager.LoadScene("Level 4");
	}

	public void LoadLevelFive() {
        SceneManager.LoadScene("Level 5");
	}

	public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
	}

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadNextLevel() {
        string scene = SceneManager.GetActiveScene().name;

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
