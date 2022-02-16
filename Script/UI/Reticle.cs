using UnityEngine;

public class Reticle : MonoBehaviour {
	
// notes:
// ref: https://www.youtube.com/watch?v=LLKYbwNnKDg&list=WL&t=598s&index=13
// shader files: https://unity3d.com/get-unity/download/archive

	public Camera cameraFacing;
	private Vector3 currentScale;

	void Start () {
		// save current scale
		currentScale = transform.localScale;
	}

	void Update () {
		RaycastHit hit;
		float distance;

		// check if raycast has hit anything
		if (Physics.Raycast(new Ray(cameraFacing.transform.position, 
		                    cameraFacing.transform.rotation * Vector3.forward), out hit)){
		// set distance as the distance from the player to the object that raycast has hit
			distance = hit.distance;
		}
		else {
			// farClipPlane is the maximium distance you can see from camera
			// make reticle be far from player if no object
			distance = cameraFacing.farClipPlane + 0.95f;
		}

		// rotate the reticle to face the camera
		transform.LookAt(cameraFacing.transform.position);
		// rotate the the quad to show the side thats visible - quad only has one side thats visible
		transform.Rotate(0, 180, 0);

		// set reticle infront of camera then move it forward by set amount
		transform.position = cameraFacing.transform.position +
			cameraFacing.transform.rotation* Vector3.forward * distance;

		// make the reticle look bigger when closer to objects
		if(distance < 10){
			distance +=  1 + 5 * Mathf.Exp(-distance);
		}

		// To make the reticle the same size no matter how far:
		// scale it equally by distance and current scale
		transform.localScale = currentScale * distance;

		// Vergence-accommodation conflict where if the player is using vr console,
		// they can see the reticle as a double image so to solve this: 
		// Draw the reticle directly onto the object that reticle is targeting on screen
		// Assuming thats where the eyes will converge
		// Optional: allow the reticle to change size whenever the raycast is not hitting anything
		// and when you get close to an object.
	}
}
