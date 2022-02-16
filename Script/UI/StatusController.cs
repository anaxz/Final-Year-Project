using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StatusController : MonoBehaviour {

	int max = 6;

	bool[] bedOccupied;
	string[] stats;

	public GameObject statusPanel;
	public GameObject statusText;
	public GameObject health;

	GameObject player;
	GameObject GameController;

	bool loadOnce = false;

	void Start () {
		bedOccupied = new bool[max];
		stats = new string[max];

		statusText.GetComponent<Text>().text = ""; //reset
		player = GameObject.FindWithTag("Player");
		GameController = GameObject.FindWithTag("GameController");
	}

	void Update () {
		// Need to set panel active false here instead of start
		// Else when player clicks clipboard, intially will not display the statusText
		if (loadOnce == false){
			statusPanel.SetActive(false);
			loadOnce = true;
		}

		// Check if right mouse/C button is clicked then allow player to move again
		if (Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.C)){
			displayStatus(false); // set display status off
			player.GetComponent<PlayerController>().setFreeze(false);
		}
	}

	//note: used in patient script
	public void Status(int patientNum, string str){
		//newPatient[num] = true;
		stats[patientNum - 1] = str; // num-1 because the patient num stored started from 1 not 0
		bedOccupied[patientNum - 1] = true; // set if bed is being used
	}

	public bool checkOccupied(int num) {
		return bedOccupied[num - 1];
	}

	// this is to display the patients status to the player
	public bool getStatus(int patientNum) {
		statusPanel.SetActive(true); 									// set active to display patient info
		statusText.GetComponent<Text>().text = stats[patientNum - 1]; 	// get patient info and display it
		return true;
	}

	// set to display status panel or not
	public void displayStatus(bool type) {
		if (type) statusPanel.SetActive(true);
		else statusPanel.SetActive(false);
	}
}
