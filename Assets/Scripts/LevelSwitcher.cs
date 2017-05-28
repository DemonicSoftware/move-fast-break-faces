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
}
