using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

	public GameObject dialoguePanel;
	public Text speakerText;
	public Text dialogueText;

	private string[] sceneSpeaker;
	private string[] sceneDialogue;
	private int lineCount;

	private int clickCoolDown;

	void Start () {
		GetSceneDialogue();
		lineCount = 0;
		DisplayDialogue(sceneSpeaker[lineCount], sceneDialogue[lineCount]);
		BeginDialogue();
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.F) && clickCoolDown <= 0) {
            lineCount++;
            clickCoolDown = 25;
        }
        if (lineCount < sceneSpeaker.Length) {
        	DisplayDialogue(sceneSpeaker[lineCount], sceneDialogue[lineCount]);
        }
        else {
        	EndDialogue();
        }

        clickCoolDown--;
	}

	private void BeginDialogue() {
		Time.timeScale = 0;
		dialoguePanel.SetActive(true);
	}

	private void DisplayDialogue(string speakerName, string dialogue) {
		speakerText.text = speakerName;
		dialogueText.text = dialogue;
	}

	private void EndDialogue() {
		Time.timeScale = 1;
		dialoguePanel.SetActive(false);
	}

	private void GetSceneDialogue() {
		string scene = Application.loadedLevelName;

		switch(scene) {
			case "Level 1":
				sceneSpeaker = new string[] { "Hero", "Hero", "Hero" };
				sceneDialogue = new string[] { "This is some dialogue. Move through it using the 'f' key.", "It should explain how to hit the bad guys...", "Use WASD to move and left click to break faces!" };
				// Specifically talk about left click and pointer...
				// Boss welcome to dungeon
				// tell player they have to kill a number of enemies to WIN
				// Talk about health
				break;
			case "Level 2":
				sceneSpeaker = new string[] { "Hero", "Hero", "Hero" };
				sceneDialogue = new string[] { "This is some other dialogue. Move through it using the 'f' key.", "You can pick up hammers on the ground and throw them.", "Use right click to do that." };
				// Talk about hammer suggest to pick it up and use it 
				// on enemioes behind pit
				break;

				// Look out for ranged enemies

				// Boss dialogue
				// Talk about how to win

				// Exposition and stuff
				// Tell player they need to escape
		}
	}
}
