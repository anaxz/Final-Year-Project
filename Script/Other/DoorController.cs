using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour {
	bool isOpen = false;

	public void checkOpen(string str) {
		// if door closed, open door - animation, sound?
		if (isOpen == false){
			if (str.Equals("Door1")){
				if (gameObject.tag.Equals("Door1")) gameObject.GetComponent<Animation>().Play("Door1.1-open");
				//else if (gameObject.name.Equals("Door1.2")) gameObject.GetComponent<Animation>().Play("Door1.2-open");
			}
			isOpen = true;

			StartCoroutine(doorClose(str));
		}
	}

	IEnumerator doorClose(string str) { 
		// if door left open, close door after 5 seconds
		if (isOpen){
			yield return new WaitForSeconds(5);

			if (str.Equals("Door1")){
				if (gameObject.tag.Equals("Door1")) gameObject.GetComponent<Animation>().Play("Door1.1-close");
				//else if (gameObject.name.Equals("Door1.2")) gameObject.GetComponent<Animation>().Play("Door1.2-close");
			}
			isOpen = false;
		}
	}
}
