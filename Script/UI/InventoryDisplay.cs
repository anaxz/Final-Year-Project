using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay {

	GameObject slot1;
	GameObject slot2;
	GameObject slot3;
	GameObject slot4;
	GameObject slot5;
	GameObject panel;

	public bool displayOnce;

	public InventoryDisplay(GameObject slot1, GameObject slot2, GameObject slot3, 
	                        GameObject slot4, GameObject slot5, GameObject panel){
		this.slot1 = slot1;
		this.slot2 = slot2;
		this.slot3 = slot3;
		this.slot4 = slot4;
		this.slot5 = slot5;
		this.panel = panel;
	}

	// Display what is inside inventory
	public void display (List<string> inventory) {
		panel.SetActive(true); // set visible true
		int slotNum = 0;

		foreach (string str in inventory) {
			slotNum++;
			//Debug.Log(str + " in bag, slot: " + slotNum);

			// if not empty, set slot with item's name
			if (!str.Equals("")) setSlot(slotNum, str);
			else setSlot(slotNum, "Empty Slot"); // reset in case
		}
		displayOnce = true;
	}

	// get item's name depending on which slot clicked
	public void setSlot(int num, string str) {
		//string[] temp = .Split('1', '2', '3', '4', '5');

		if (num.Equals(1)) slot1.GetComponent<Text>().text = str;
		else if (num.Equals(2)) slot2.GetComponent<Text>().text = str;
		else if (num.Equals(3)) slot3.GetComponent<Text>().text = str;
		else if (num.Equals(4)) slot4.GetComponent<Text>().text = str;
		else if (num.Equals(5)) slot5.GetComponent<Text>().text = str;
	}
}
