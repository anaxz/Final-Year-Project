using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsController : MonoBehaviour {
	
	List<string> inventory = new List<string>();

	GameObject MainText;

	void Start () {
		MainText = GameObject.Find("MainText");
	}

	public bool addToInventory (GameObject _object) {
		string Name = _object.tag; // get gameObject's name

		// Player can only have max items
		if (inventory.Count < 5) {
			inventory.Add(Name);
			//Debug.Log(Name + " added");
			return true;
		}
		MainText.GetComponent<TextController>().setText("I only have two hands...", "");
		return false;
	}

	public bool RemoveFromInventory(string _object) {
		// if item in inventory, remove item
		if (inventory.Contains(_object)) {
			inventory.Remove(_object);
			Debug.Log(_object+" removed");
			return true;
		}
		return false;
	}

	// check if item in inventory
	public bool checkInInventory(string _object) {
		if (inventory.Contains(_object)) return true;
		else return false;
	}

	public List<string> getInventory() {
		return inventory;
	}
}
