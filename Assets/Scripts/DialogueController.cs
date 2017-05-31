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
		if (Input.GetKey(KeyCode.X)) {
            EndDialogue();
        }
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
		if (speakerName == "Larry") {
			speakerText.color = Color.red;
			dialogueText.color = Color.red;
		}
		else {
			speakerText.color = Color.white;
			dialogueText.color = Color.white;
		}
		speakerText.text = speakerName;
		dialogueText.text = dialogue;
	}

	private void EndDialogue() {
		Time.timeScale = 1;
		dialoguePanel.SetActive(false);
	}

	private void GetSceneDialogue() {
		string scene = Application.loadedLevelName;

		string l = "Larry";
		string v = "Voice";

		switch(scene) {
			case "Level 1":
				sceneSpeaker = new string[] { v, l, v, l, v, v, l, v, v, l, v, v };
				sceneDialogue = new string[] { "Wake up Larry... and welcome... TO YOUR DOOOOOOOOOOOM (use the 'f' key to progress through dialogue)", "Who are... what is this?", "Who I am doesn't matter and you're here because I decided to put you through an arbitrary series of life or death trials for my own entertainment.", "...", "Anyway! Take a look arround the room using your cursor.", "Once I unpause time you'll be able to move arround with WASD.", "Is this just an elaborate prank?", "You can swing that baseball bat there with your left mouse button 'LMB' to break the faces of your enemies!", "Your health is displayed behind you in a bar and you will die if it reaches 0.", "WHAT ARE YOU ON ABOUT, YOU LUNATIC?", "This... Larry...", "... is only the tutorial! HAHAHHAHHAHHAHHAHA!" };
				break;

			case "Level 2":
				sceneSpeaker = new string[] { l, v, v, v, v, v, v, v };
				sceneDialogue = new string[] { "What the hell were those things?", "Well done, Larry.", "But I like it rare and you... are nothing special.", "I am now introducing a new mechanic - HAMMERS!", "You can pick them up and throw them at enemies...", "... using your your right mouse button 'RMB'! HAHAHHAHHHAHHAHAHAH!", "Go pick that one up and throw it at those enemies over there!", "I'm so EXCITED I can barely contain myself!" };
				break;

			case "Level 3":
				sceneSpeaker = new string[] { v, v, l, v };
				sceneDialogue = new string[] { "Oh how fun that was!", "Time for a NEW MECHANIC, Larry!", "Please! No more objects, rules or mechanic! There's only so much my heart can take!", "There are now RANGED enemies. They'll fire orbs of orb'iness at you so be careful HAHAHA!" };
				break;

			case "Level 4":
				sceneSpeaker = new string[] { v, l, v };
				sceneDialogue = new string[] { "Guess what, Larry?", "... a... a new mechic?", "Time for the boss level! Have ALL OF MY MECHANICS AT THE SAME TIME!" };
				break;

			case "Level 5":
				sceneSpeaker = new string[] { v, "", l };
				sceneDialogue = new string[] { "Oh well, that was fun while it lasted, TIME FOR YOU TO MAKE YOUR EXIT, Larry!", "*rumbling sounds*", "What... what was that?" };
				break;
		}
	}
}
