using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetItem : MonoBehaviour {
	// note: http://answers.unity3d.com/questions/390990/gameobject-setactive-not-reactivating.html

	string parent;

	void Start() {
		parent = transform.gameObject.name;
	}

	public void resetItems() {
		// iterate through all child objects
		foreach (Transform child in this.transform){
			if(parent.Equals("ObtainableItems")){
				// if the child object is not visible, set it active again
				if (child.gameObject.activeSelf.Equals(false)) child.gameObject.SetActive(true);
			}
			// set items as false if visible - for the carts
			else if (child.gameObject.activeSelf.Equals(true)) child.gameObject.SetActive(false);
		}
	}

}
