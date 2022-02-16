using System.Collections.Generic;
using UnityEngine;

public class Patient {
	// stats
	int patientNum;
	int health = 100;
	//int happiness = 100;
	public int careRating;
	int limit = 3;

	string conditionStr;
	string conditionItem;
	int conditionNum;

	// misc
	string name;
	string gender;
	int age;
	//string occupation;

	string currentTask;

	GameObject Object;
	GameObject MainText;
	GameObject GameController;

	public Patient(GameObject Object){
		name = randomName();
		age = randomAge();
		this.Object = Object;

		GameController = GameObject.FindWithTag("GameController");
		MainText = GameObject.Find("MainText");
		getPos(Object); // set patient num

		setCondition(); // get and set random condition
		GameController.GetComponent<StatusController>().Status(patientNum, statusStr()); // store patient's stats on database
	}

	// generate and assign random name
	string randomName(){
		// even = female, odd = male
		string[] _name = { "Jane", "Henry", "June", "Matthew", "Sarah", "Jake", "Lucy", "Luke", "Sally", "Alex" };
		int num = Random.Range(0, _name.Length);
		setGender(num); // set gender
		return _name[num];
	}

	void setGender(int num){
		if (num % 2 == 0) gender = "Female"; // if even, female vice versa
		else gender = "Male";
	}

	// generate and assign random age
	int randomAge(){
		int num = Random.Range(12, 60);
		return num;
	}

	public void sendRating(){
		//note: patientNum starts from 1 so -1 to start from 0
		// addRating(list index, care rating)
		GameObject player = GameObject.FindWithTag("Player");
		player.GetComponent<PlayerStats>().addRating(patientNum-1, careRating);
		//Debug.Log("CareRating Added ");
	}

	// this is to display the patients status to the player
	public string statusStr(){
		string str = "Patient ID: " + patientNum + "\t\tName: " + name +
			"\n\n\nAge: " + age + "\t\tGender: " + gender + "\n\nNotes:\n" + conditionStr;
		
		return str;
	}

	// generate random number to get random conditions
	void setCondition(){
		int num = Random.Range(0, 15);
		conditionNum = num; // store which node/condition patient has

		conditionStr = GameController.GetComponent<ConditionManager>().getCondition(num);
		conditionItem = GameController.GetComponent<ConditionManager>().getConditionItem(num);
		Debug.Log("Patient Num: " + patientNum + " Condition num: " + num + " - " + conditionStr);	
	}

	// get current postion to set patient num
	void getPos(GameObject _object){
		/* position:
		 * 1. -8.93, 0, -14 	| 2. -8.93, 0, -4 	| 3. -8.93, 0, 7
		 * 4. 5.83, -9.33, -14 	| 5. 5.83, -9.33, -4.26 | 6. 5.83, -9.33, 6.9 */

		Vector3 pos = _object.gameObject.transform.position;
		float x = pos.x;
		//float y = pos.y;
		float z = pos.z;

		// check on left side...
		if (x <= -8 && x >= -9){
			if (z <= -14 && z >= -15) patientNum = 1;
			else if (z <= -4 && z >= -5) patientNum = 2;
			else patientNum = 3;
		}
		// else check if on opposite side
		else if (x >= 5 && x <= 6){
			if (z <= -14 && z >= -15) patientNum = 4;
			else if (z <= -4 && z >= -5) patientNum = 5;
			else patientNum = 6;
		}
	}

	//>-------------------------------------------Tasks Related function----------------------------------------------<//

	List<string> items = new List<string>(); // stores items that will let player finish task
	bool demand = false; 					 // use this to keep track if patient is already demanding anything
	bool found = false;
	//string currentItem = "";
	int TaskNum = -1;
	public bool isDelay = false;

	public void Demand(){
		// check if there isn't a task already set
		// If not, give the task first even if item is already in inventory
		if (demand == false && isDelay == false){
			Debug.Log("New demand from " + name + " Rating: " + careRating);
			int size = GameController.GetComponent<TaskManager>().getTaskSize();
			TaskNum = Random.Range(0, size); // get a random task

			currentTask = GameController.GetComponent<TaskManager>().getTask(TaskNum);
			MainText.GetComponent<TextController>().setText(currentTask, ""); // Display the task to player

			// add the items that will allow to complete the task
			items.AddRange(GameController.GetComponent<TaskManager>().getItems(currentTask));
			limit = 3; //reset limit

			found = false;
			demand = true;
		}
		// if true, grab display current task and respond if conditions met
		else {
			GameController.GetComponent<InventoryPanel>().display(); // display inventory
		}
	}

	public void itemGiven(string currentItem) {
		//bool found = false;

		// item match, found is set true
		for (int i = 0; i < items.Count; i++){
			if (items[i].Equals(currentItem)){
				//Debug.Log("Found - CurrentItem: " + currentItem);
				found = true;
				break;
			}
		}

		// if item matches...
		if (found){
			// if patient has a condition:
			// check if item and conditionItem matches
			// if match condition item which cause negative consequence
			// OR if vegetarian and current item is not salad nor soup, negative consequence
			if (!conditionStr.Equals("") && currentItem.Equals(conditionItem)
			    || (!conditionStr.Equals("") && conditionStr.Equals("Vegetarian") 
			        && !(currentItem.Equals("Salad") || currentItem.Equals("Veggy Soup"))) ) {
				
				// check if bad or good consequence - if true, bad effect
				if (GameController.GetComponent<ConditionManager>().consequence(conditionNum)){
					Debug.Log("Fail condition. Assign new task");
					if (careRating > 0) careRating--;
					MainText.GetComponent<TextController>().setText("Ow... Ugh", "");
					// give penalty to player

					found = false;
					demand = false;
				}
				// else good effect so increase rating twice
				Debug.Log("Pass condition");
				if (careRating < 10) careRating++;
			}

			if (careRating < 10) careRating++; // increment rating but no higher than 10
            sendRating(); // update care rating

			// display on cart
			GameController.GetComponent<CartController>().sendCartNum(currentItem, patientNum);

			// Display patient's response		
			MainText.GetComponent<TextController>().setText(GameController.GetComponent<TaskManager>().getResponse(currentTask, currentItem), "");                                                                                                                 //Debug.Log(name + " respond");

			// task finished so reset conditions
			items.Clear();
			demand = false;
			isDelay = true;
		}

		// if player asks patient too many times and still does not give correct item,
		// give new task and decrease rating
		else if (limit <= 0) {
			Debug.Log("Fail. Assign new task");
			if(careRating > 0) careRating--;

			// set negative response
			MainText.GetComponent<TextController>().setText(GameController.GetComponent<TaskManager>().negativeResponse(), "");

			// switch the inventory display off
			// note to self: if not off, can't get player to talk to patient to get new task
			GameController.GetComponent<InventoryPanel>().displayOff(); 

			// reset
			demand = false;
			isDelay = true;
		}

		else {
			// if no match, display the task to player again 
			// and decrease chances of completeing task
			MainText.GetComponent<TextController>().setText(currentTask, "");
			limit--;
		}
	}

	// send info to itemClicked script that item matches
	public bool isFound() {
		return found;
	}
}
