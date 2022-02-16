using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClicked : MonoBehaviour {

	GameObject GameController;

	void Start() { 
		GameController = GameObject.FindWithTag("GameController");
	}

	// select item to give to patient
	public void clicked() {
		// if string clicked is not 'empty slot', remove item and display text
		if (!(transform.gameObject.GetComponent<Text>().text == "Empty Slot")) {

			// get the patient that you last interacted with
			// then send the name of the item
			GameObject currentPatient = GameController.GetComponent<LastInteractedPatient>().getChara();
			currentPatient.GetComponent<Character>().itemGiven(transform.gameObject.GetComponent<Text>().text);

			// only remove and reset text if item found.
			// else the item remains in the inventory
			if(currentPatient.GetComponent<Character>().isFound()){
				// remove item from inventory
				GameController.GetComponent<ItemsController>().RemoveFromInventory(transform.gameObject.GetComponent<Text>().text);
				transform.gameObject.GetComponent<Text>().text = "Empty Slot"; // reset slot text

				// let player move again and set inventory panel off
				GameController.GetComponent<InventoryPanel>().displayOff();
			}
		}
	}

	// remove item during interacting with staff/nurse
	public void RemoveItemClick() {
		// note: can't use tag as it takes 'slot' tag not the item tag!
		// so take the slot text instead

		// set initial text
		GameController.GetComponent<InventoryRemovePanel>().dialogue.GetComponent<Text>().text = "Do you need any help?";

		// if string clicked is not 'empty slot', remove item and display text
		if (!(transform.gameObject.GetComponent<Text>().text == "Empty Slot")) {
			GameController.GetComponent<ItemsController>().RemoveFromInventory(transform.gameObject.GetComponent<Text>().text);
			transform.gameObject.GetComponent<Text>().text = "Empty Slot"; // reset slot text

			// Show some kind of confirmation that item has been removed
			GameController.GetComponent<InventoryRemovePanel>().Text();
		}
	}
}
