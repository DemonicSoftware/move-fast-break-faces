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
            clickCoolDown = 50;
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
		sceneSpeaker = new string[] { "Hero", "Hero", "Hero" };
		sceneDialogue = new string[] { "This is some dialogue. Move through it using the 'f' key.", "It should explain how to hit the bad guys...", "Use WASD to move and left click to break faces!" };
	}
}
