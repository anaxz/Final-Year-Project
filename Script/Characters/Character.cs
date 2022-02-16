using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	
	public GameObject Chara;

	Patient patient;

	float sec;

	void Start () {
		patient = new Patient(Chara);
		//patient.setCondition();
	}

	void Update() {
		/*if (done == false){
			int num = Random.Range(0, 10);
			patient.setCondition(num);
			done = true;
		}*/

		// delay giving new task
		if (patient.isDelay) {
			sec += Time.deltaTime;

			// if timer up, give new task
			if (sec >= 5){
				patient.isDelay = false;
				sec = 0; // reset
			}
		}
	}

	// check if new demand. New demand if isDelay is false
	public bool checkDemand() {
		return patient.isDelay;
	}

	public void Demand() {
		// check if delay demand else give demand
		patient.Demand();
	}

	// if 
	public void itemGiven(string currentItem) {
		patient.itemGiven(currentItem);
	}

	public bool isFound(){
		return patient.isFound();
	}
}