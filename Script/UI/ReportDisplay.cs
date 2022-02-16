using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportDisplay : MonoBehaviour {

	public GameObject reportPanel;
	public GameObject reportText;
	GameObject player;

	bool displayed = false;

	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	public void display(){
		reportPanel.SetActive(true);
		displayed = true;
		reportText.GetComponent<Text>().text = player.GetComponent<PlayerStats>().stats();
	}

	public bool isDisplay() {
		return displayed;
	}

	// set to display panel or not
	public void displayStatus(bool type){
		if (type) reportPanel.SetActive(true);
		else reportPanel.SetActive(false);
	}
}
