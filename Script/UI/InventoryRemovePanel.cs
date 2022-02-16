using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRemovePanel : MonoBehaviour {

	public InventoryDisplay inven;

	public GameObject slot1;
	public GameObject slot2;
	public GameObject slot3;
	public GameObject slot4;
	public GameObject slot5;
	public GameObject panel;
	public GameObject dialogue;

	List<string> inventory = new List<string>();

	void Start(){
		inven = new InventoryDisplay(slot1, slot2, slot3, slot4, slot5, panel);
	}

	void Update() {
		// To exit out of display if player does not want to remove items:
		// if right mouse click/C button clicked enable player controls
		if (Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.C)){
			panel.SetActive(false); // turn inventoryRemove panel off

			GameObject staff = GameObject.FindWithTag("Nurse");
			staff.GetComponent<PathAgent>().setSpeed(0.08f); // make the character move again

			GameObject player = GameObject.FindWithTag("Player");
			player.GetComponent<PlayerController>().setFreeze(false);
			inven.displayOnce = false; // set false so next time display again, get current inventory
		}
	}

	public void display(){
		// only display the info once or there will be inconsistant text
		if(inven.displayOnce == false){
			// Get current inventory list
			inventory = gameObject.GetComponent<ItemsController>().getInventory();
			inven.display(inventory); // display the panel
		}
	}

	public void Text() {
		dialogue.GetComponent<TextController>().setText("I'll take that for you", "Do you need any help?");
	}
}
