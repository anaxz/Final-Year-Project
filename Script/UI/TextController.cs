using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class TextController : MonoBehaviour {
	Text Dialogue;
	bool stop = false;

	void Start () {
		Dialogue = GetComponent<Text>();
		Dialogue.text = ""; // reset text at start
	}

	void Update() {
		// Display how to move player at start
		// Stop display controls if they open door/press f key
		//displayControls(stop);
		if (Input.GetKey(KeyCode.F)) stop = true;
	}

	public void setText(string str, string Default) {
		Dialogue.text = str; // set text

		// make text disappear after set time
		// Default -> set a default str to appear when time is up
		StartCoroutine(setTimer(2, Default));
	}

	// set text blank after set seconds
	IEnumerator setTimer(int sec, string str) {
		yield return new WaitForSeconds(sec);
		Dialogue.text = str;
	}

	// Check if player press F button to continue next dialogue
	/*public void NextDialogue(string str) {
		// if f button pressed, new text
		if (Input.GetKey(KeyCode.F)) Dialogue.text = str;
	}*/

	// Note: Will only use this once
	void displayControls(bool stop) {
		GameObject info = GameObject.Find("Info");
		if (stop == false) info.GetComponent<Text>().text = "Use w, a, s, d keys to move\n" +
				 "\nUse the mouse to look around and the direction you want to walk towards";
		else info.GetComponent<Text>().text = "";
		//else info.SetActive(false); // set active false when f key pressed at least once
	}
}
