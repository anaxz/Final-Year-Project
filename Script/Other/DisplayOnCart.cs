using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOnCart : MonoBehaviour {

	public GameObject Cart;
	public List<GameObject> items;

	int id;

	void Start () {
		string[] temp = Cart.name.Split('.'); // split the string at .
		id = int.Parse(temp[temp.Length - 1]); // take the last letter and convert it to int
	}

	public bool setActive (string str, int num) {
		// note: using name instead of tag as tag is untagged for these items
		// so that player can't reuse items except book

		// if matches this cart's id, then this is the patient's cart
		if (id.Equals(num)){
			foreach (GameObject _items in items){
				// if the item's name matches string, set it active
				if (_items.gameObject.name.Equals(str)){
					_items.SetActive(true);
					return true;
				}
			}
		}
		return false;
	}
}
