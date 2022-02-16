using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

	TreeNode<string> root;
	string[] list = { "I'm thirsty", "I'm hungry", "Get me a salad", "I'm bored", "I'm cold", "It's bit chilly today...", 
		"Psst you got any sleeping pills?", "Psst got any drugs?", "I can't sleep", "Can I have some soup" };

	void Start() {
		root = new TreeNode<string>("ListsOfTask");

		string thanks = "Thank you";

		addTask(list[0], "Water Bottle", thanks);

		addTask(list[1], "Pot Noodles", thanks);
		addResponse(list[1], "Pot Noodles", "Ugh hospital food");
		addResponse(list[1], "Pot Noodles", "Not bad");

		addItem(list[1], "Salad", thanks);
		addResponse(list[1], "Salad", "Ugh hospital food");
		addResponse(list[1], "Pot Noodles", "I miss homecooked food");

		addItem(list[1], "Veggy Soup", thanks);
		addResponse(list[1], "Veggy Soup", "Ugh hospital food");

		addItem(list[1], "Nut Soup", thanks);
		addResponse(list[1], "Nut Soup", "I miss homecooked food");
		addResponse(list[1], "Nut Soup", "Ugh hospital food");

		addItem(list[1], "Chicken Soup", thanks);
		addResponse(list[1], "Chicken Soup", "Ugh hospital food");

		addTask(list[2], "Salad", thanks);
		addResponse(list[2], "Salad", "This will do");

		addTask(list[3], "Book", thanks);
		addResponse(list[3], "Book", "This will do");
        addResponse(list[3], "Book", "I want to go home...");
        addResponse(list[3], "Book", "Why don't you have wifi T^T");

		addTask(list[4], "Blanket", thanks);

		addTask(list[5], "Blanket", "You should fixed the heating");
		addResponse(list[5], "Blanket", thanks);

		addTask(list[6], "Sleeping Pills", thanks);
		addResponse(list[6], "Sleeping Pills", "zzZ");

        addTask(list[7], "Sleeping Pills", thanks);
        addResponse(list[7], "Sleeping Pills", "zzZ");

		addItem(list[7], "Painkillers", thanks);

		addItem(list[7], "Macrolides", thanks);
		addResponse(list[7], "Macrolides", "Hate swallowing these things");

		addItem(list[7], "Penicillin", thanks);
        addResponse(list[7], "Penicillin", "Hate swallowing these things");

        addTask(list[8], "Sleeping Pills", thanks);
		addResponse(list[8], "Sleeping Pills", "zzZ");

		addTask(list[9], "Nut Soup", thanks);
		addResponse(list[9], "Nut Soup", "Looks... yummy");

		addItem(list[9], "Veggy Soup", thanks);
		addResponse(list[9], "Veggy Soup", "So lukewarm...");

		addItem(list[9], "Chicken Soup", thanks);
		addResponse(list[9], "Chicken Soup", "This is nice");
	}

	public int getTaskSize() {
		return list.Length;
	}

	public void addTask(string task, string item, string response){
		root.addChild(task);
		root.getChild(task).addChild(item);
		root.getChild(task).getChild(item).addChild(response);
	}

	// add another node to existing task node
	public void addItem(string task, string item, string response){
		root.getChild(task).addChild(item);
		root.getChild(task).getChild(item).addChild(response);
	}

	// add another node to item node
	public void addResponse(string task, string item, string response) { 
		root.getChild(task).getChild(item).addChild(response);
	}

	public string getTask(int num) {
		return list[num];
	}

	/*public bool itemMatch(int num) {
		return root.SearchNode(root.getChild(list[num]));
	}*/

	public List<string> getItems(string task) {
		return root.getChild(task).getChildData();
	}

	public string getResponse(string task, string item) {
		List<string> temp = new List<string>();
		temp.AddRange(root.getChild(task).getChild(item).getChildData()); // add the avalible responses to list

		int num = Random.Range(0, temp.Count); // get random number and return a random reponse
		return temp[num];
	}

	public string negativeResponse(){
		string[] badResponse = { "...", "Useless...", "-.-", "Ugh", "..." };

		int num = Random.Range(0, badResponse.Length); // get random number and return a random reponse
		return badResponse[num];
	}
}