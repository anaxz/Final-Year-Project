using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
	Camera fpsCam; // Holds a reference to the first person camera
	RaycastHit hit;
	Rigidbody rb;

	public float speed;
	float lineEnd = 6;

	public GameObject F_button;
	public GameObject itemText;
	GameObject detected;
	GameObject gameController;

	bool playerFreeze = false;

	AudioSource walk_sound;

	Vector3 startingPos;

	void Start () {
		rb = GetComponent<Rigidbody>();
		fpsCam = FindObjectOfType<Camera>();
		walk_sound = GetComponent<AudioSource>();
		gameController = GameObject.FindWithTag("GameController");
		startingPos = transform.position; // store starting position of player
	}

	void Update() {
		// Create a vector at the center of our camera's viewport
		Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

		// check if raycast hits something and if that object is interactable
		if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, lineEnd)){
			CheckInteractable();
		}

		// Scene view only; Draw a line in the Scene View from the point lineOrigin in the direction
		// of fpsCam.transform.forward * weaponRange, using the color green
		Debug.DrawRay(rayOrigin, fpsCam.transform.forward * lineEnd, Color.green);
	}

	void FixedUpdate () {
		playerControls();
	}

	public bool isFreeze(){
		return playerFreeze; // return whether player controls frozen
	}

	public void setFreeze(bool temp){
		playerFreeze = temp; // set whether or not to freeze player controls
	}

	void playerControls() {
		// stop player from moving if true
		if (playerFreeze == false){
			if (Input.GetKey(KeyCode.W)) rb.transform.Translate(Vector3.forward * Time.deltaTime * speed);
			if (Input.GetKey(KeyCode.S)) rb.transform.Translate(-(Vector3.forward * Time.deltaTime * speed));
			if (Input.GetKey(KeyCode.D)) rb.transform.Translate(Vector3.right * Time.deltaTime * speed);
			if (Input.GetKey(KeyCode.A)) rb.transform.Translate(Vector3.left * Time.deltaTime * speed);
			// note-to-self: Not using addforce as movement not as smooth

			/*playSound(true);*/
		}
	}

	void playSound(bool isWalk) {
		if (isWalk) walk_sound.Play();
		else walk_sound.Stop(); // stop playing sound if not walking
	}

	public Vector3 getStartPos(){
		return startingPos;	
	}

	void CheckInteractable() {
		// check if object interactable, display F button and cause an object's behaviour to occur
		if (hit.collider.gameObject.tag == "Door1"){
			F_button.SetActive(true); // display 'f' button icon
			itemText.SetActive(true); // display the item's name

			// set text to let player know they can open door
			itemText.GetComponent<TextController>().setText(hit.collider.gameObject.name, "");
			F_button.SetActive(true); // display 'f' button icon

			// if left mouse key/F button clicked...
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.F)){
				// check if door not open already, run animation
				hit.collider.gameObject.GetComponent<DoorController>().checkOpen("Door1");
			}
		}

		else if (hit.collider.gameObject.tag == "Clipboard") {
			string[] temp = hit.collider.gameObject.name.Split('.'); // split the string at .
			int lastLetter = int.Parse(temp[temp.Length - 1]);       // convert string to int
			//Debug.Log("Clipboard num: " + lastLetter);

			// if false: don't display interact button as there is no patient occuping the bed
			if (gameController.GetComponent<StatusController>().checkOccupied(lastLetter)){
				itemText.SetActive(true);
				itemText.GetComponent<TextController>().setText("Check patient status", ""); // set text to be displayed
				F_button.SetActive(true); // display 'f' button icon

				// if left mouse key/F button clicked...
				if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.F)){
					// freeze the player controls until the status display turns off
					if (gameController.GetComponent<StatusController>().getStatus(lastLetter)){
						//detected.SetActive(true);
						playerFreeze = true;
					}
				}
			}
		}

		// check if raycast hit patient and that there's a new demand. Display interactive buttons.
		else if (hit.collider.gameObject.tag == "Chara" && 
		         hit.collider.gameObject.GetComponent<Character>().checkDemand() == false){
			itemText.SetActive(true); // display the item's name
			itemText.GetComponent<TextController>().setText("Talk to patient", "");
			F_button.SetActive(true); // display 'f' button icon

			// if left mouse key/F button clicked...
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.F)){
				// store which patient gameobject last interacted with
				gameController.GetComponent<LastInteractedPatient>().setChara(hit.collider.gameObject);

				// check and make new demands else display inventory panel
				hit.collider.gameObject.GetComponent<Character>().Demand();
			}	
		}

		else if(hit.collider.gameObject.tag == "Nurse"){
			F_button.SetActive(true);
			itemText.SetActive(true);
			itemText.GetComponent<TextController>().setText("Nurse", "");

			// if left mouse key/F button clicked...
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.F)){
				hit.collider.gameObject.GetComponent<StaffController>().stop();
				playerFreeze = true;
			}
		}

		else if(hit.collider.gameObject.tag == "Water Bottle" || hit.collider.gameObject.tag == "Pot Noodles" ||
		        hit.collider.gameObject.tag == "Salad" || hit.collider.gameObject.tag == "Book" ||
		        hit.collider.gameObject.tag == "Blanket" || hit.collider.gameObject.tag == "Veggy Soup" ||
		        hit.collider.gameObject.tag == "Nut Soup" || hit.collider.gameObject.tag == "Chicken Soup" ||
		        hit.collider.gameObject.tag == "Macrolides" || hit.collider.gameObject.tag == "Penicillin" ||
		        hit.collider.gameObject.tag == "Sleeping Pills" || hit.collider.gameObject.tag == "Painkillers"){

			itemText.SetActive(true); // display the item's name
			itemText.GetComponent<TextController>().setText(hit.collider.gameObject.tag, "");
			F_button.SetActive(true); // display 'f' button icon

			// if left mouse key/F button clicked...
			if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.F)){
				// Add item to inventory
				if (gameController.GetComponent<ItemsController>().addToInventory(hit.collider.gameObject)){
					hit.collider.gameObject.SetActive(false); // Remove visually
				}
			}
		}

		// set f button inactive if no interactive object
		else {
			F_button.SetActive(false);
			itemText.SetActive(false);
		}
	}
}
