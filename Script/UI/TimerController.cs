using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerController : MonoBehaviour {
	
	Text timerText;
	int day = 1;
	int hour = 9;
	float min;

	GameObject resetItems;
	GameObject GameController;
	GameObject player;
	public GameObject endScreen;

	bool pause;
	bool endEarly;

	void Awake () {
		timerText = GetComponent<Text>();
		resetItems = GameObject.Find("ObtainableItems");
		GameController = GameObject.Find("GameController");
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		// increase time but only if not during report display
		// multiply by 2 to increase how fast the timer increaments
		if (pause == false) {
			min += Time.deltaTime * 2;
		}
		else {
			GameController.GetComponent<ReportDisplay>().display(); // display the report panel

			// if C button/right mouse clicked...
			if (Input.GetKey(KeyCode.C) || Input.GetMouseButtonDown(1)) {
				Debug.Log("Game Reset");
				// let player roam again and hide the report display
				GameController.GetComponent<ReportDisplay>().displayStatus(false);
				resetItems.GetComponent<resetItem>().resetItems();          // spawn items
				GameController.GetComponent<CartController>().reset();      // set items visiblity on carts off
				GameController.GetComponent<CharaSpawn>().setSpawn(true);	// make new patients
				player.GetComponent<PlayerController>().setFreeze(false);   // allow player movement
				player.GetComponent<PlayerStats>().setIsWarn(false); 		// reset isWarn bool

				hour = 9;
				day++;
				pause = false;

				// if true, display Game Over screen
				// end early if made severe mistake
				if (player.GetComponent<PlayerStats>().isEnd() || endEarly) endScreen.SetActive(true);
			}
		}
		// slight display difference for timer
		if (min <= 10) timerText.text = "Day: " + day + " Time: " + hour + ":0" + Mathf.Round(min);
		else timerText.text = "Day: " + day + " Time: " + hour + ":" + Mathf.Round(min);

		// note: shift will start from 9am to 3pm
		// if 60 mins, reset mins, add 1 to hour and pay wage for the hour
		if (min >= 60 || endEarly){
			min = 0f; // reset min
			hour++;
			player.GetComponent<PlayerStats>().addWage(); // add pay per hour
		}
		// if end of shift, pause and display report
		if (hour >= 15 || endEarly){ 
			player.GetComponent<PlayerController>().setFreeze(true);
			pause = true;
		}
	}

	public int getDay() {
		return day;
	}
}
