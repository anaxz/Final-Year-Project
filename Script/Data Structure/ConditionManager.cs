using UnityEngine;

public class ConditionManager : MonoBehaviour {
	// ref: http://www.nhs.uk/Conditions/Antibiotics-penicillins/Pages/Introduction.aspx

	LinkList<string> listConditions = new LinkList<string>();

	bool set = false;

	string bad = "bad";
	string good = "good";

	GameObject player;

	void Start () {
		player = GameObject.FindWithTag("Player");

		// condition, item that would trigger condition, type,  effect - how bad the penalty
		listConditions.insertNode("Allergic to nuts", "Nut Soup", bad,"severe");
		listConditions.insertNode("Vegetarian", "Salad", good, "minor");
		listConditions.insertNode("Vegetarian", "Veggy Soup", good, "minor");
		listConditions.insertNode("Needs a dosage of anti-bodies - Penicillin", "Macrolides", bad, "mediocre");
		listConditions.insertNode("Has insomnia", "Sleeping Pills", good, "mediocre");
		listConditions.insertNode("Needs a dosage of anti-bodies - Macrolides", "Penicillin", bad, "severe");
		listConditions.insertNode("Allergic to Penicllin", "Penicillin", bad, "severe");
		listConditions.insertNode("Needs some painkillers", "Painkillers", good, "mediocre");
		listConditions.insertNode("Addiction to painkillers. DON'T GIVE ANY!", "Painkillers", good, "mediocre"); //labelled gd cuz patient wants it
		listConditions.addNote("Avoid. Do no-");
		listConditions.insertNode("Patient allowed to be prescribed with sleeping pills", "Sleeping Pills", good, "mediocre");
	}

	// get a random condition
	public string getCondition(int num) {
		// if num greater than listConditions, give the patient no conditions
		if (num >= listConditions.size) return "";

		// else give patient a condition
		return listConditions.getNode(num, "condition");
	}

	public string getConditionItem(int num) { 
		// if num greater than listConditions, give the patient no conditions
		if (num >= listConditions.size) return "";

		// else give patient a condition item
		return listConditions.getNode(num, "item");
	}

	public bool consequence(int num) {
		string type = listConditions.getNode(num, "type");
		string effect = listConditions.getNode(num, "effect");

		if (type.Equals(bad)){
			if (effect.Equals("minor")){
				// just return to only decrease care rating
				return true;
			}
			else if(effect.Equals("mediocre")){
				// increase warning by 1
				player.GetComponent<PlayerStats>().setWarning(1);
				return true;
			}
			else if(effect.Equals("severe")){
				// increase warning by 3 - end screen
				player.GetComponent<PlayerStats>().setWarning(3);
				return true;
			}
		}
		// may be empty node/no condition
		return false;
	}
}
