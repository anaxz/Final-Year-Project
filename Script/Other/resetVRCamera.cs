using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetVRCamera : MonoBehaviour {
	//note: doesnt work. Tried to use this to reset VR camera's position
	// becuase some reason the y pos keeps increasing when game stops and plays again

	public Camera VRCamera;

	Vector3 pos = Vector3.zero;

	void Start () {
		VRCamera.transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		VRCamera.transform.position = pos;
		Debug.Log(VRCamera.transform.position + "\nShould be: " + pos);
	}
}
