using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour {

	public List<GameObject> Carts;

	public bool sendCartNum(string item, int num) {
		foreach (GameObject Cart in Carts) {
			// if returns true, stop iterating
			// else keep going through the list to find the correct cart
			if (Cart.GetComponent<DisplayOnCart>().setActive(item, num)) return true;
		}
		return false;
	}

	// set items displayed on all cart as false
	public void reset() {
		foreach (GameObject Cart in Carts){
			Cart.GetComponent<resetItem>().resetItems();
		}
	}
}
