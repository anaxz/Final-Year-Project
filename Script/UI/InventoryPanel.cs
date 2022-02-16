using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour {

	public InventoryDisplay inven;

	public GameObject slot1;
	public GameObject slot2;
	public GameObject slot3;
	public GameObject slot4;
	public GameObject slot5;
	public GameObject panel;

	GameObject player;

	List<string> inventory = new List<string>();

	void Start() {
		inven = new InventoryDisplay(slot1, slot2, slot3, slot4, slot5, panel);
		player = GameObject.FindWithTag("Player");
	}

	void Update(){
		// To exit out of display if player does not want to remove items:
		// if right mouse click/C button clicked enable player controls
		if (Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.C)){
			player.GetComponent<PlayerController>().setFreeze(false); // let player walk around again
			panel.SetActive(false); 	// turn inventoryRemove panel off
			inven.displayOnce = false; 	// set false so next time display again, get current inventory
		}	
	}

	public void display () {
		// only display the info once or there will be inconsistant text
		if(inven.displayOnce == false){
			// Get current inventory list
			inventory = gameObject.GetComponent<ItemsController>().getInventory();
			inven.display(inventory); // display the panel
			player.GetComponent<PlayerController>().setFreeze(true); // freeze player controls
		}
	}

	public void displayOff() {
		player.GetComponent<PlayerController>().setFreeze(false); // let player walk around again
		panel.SetActive(false); // switch inventory panel off
		inven.displayOnce = false;
	}
}
