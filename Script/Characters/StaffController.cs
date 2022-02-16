using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffController : MonoBehaviour {
// ref: https://forum.unity3d.com/threads/rotate-away-from-game-object-method.144651/

	float TurnSpeed = 15;
	//public float RotationMargin = 1.0f;

	GameObject GameController;

	void Start() {
		GameController = GameObject.FindWithTag("GameController");
	}

	public void stop() {
		transform.gameObject.GetComponent<PathAgent>().setSpeed(0); // original speed: 0.08
		GameController.GetComponent<InventoryRemovePanel>().display(); // display inventory panel
	}

	// rotate character facing away from chosen vector3 position
	public void RotateTo(Vector3 position){
		Vector3 facing = position - transform.position;
		//if (facing.magnitude < RotationMargin) { return; }

		// Rotate the rotation away from position...
		Quaternion rotateValue = Quaternion.LookRotation(facing);
		Vector3 euler = rotateValue.eulerAngles;
		//euler.y -= 180; // makes the character face away from target position
		rotateValue = Quaternion.Euler(euler);

		// Rotate the game object:
		// rotate game object
		transform.rotation = Quaternion.Slerp(transform.rotation, rotateValue, TurnSpeed * Time.deltaTime);
		// rotate angle. this prevent character from tilting when near target position
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);	
	}
}
