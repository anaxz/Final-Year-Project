using UnityEngine;

//ref: https://www.youtube.com/watch?v=195KUinjBpU

public class Node <T> {
	public T condition; // patient's condition
	public T item; 		// what item will  trigger condition
	public T type; 		// good or bad
	public T effect; 	// how bad the penalty
	public Node <T> next; //this is needed for reference next node

	public Node(T notes){
		condition = notes;
	}
	public Node(T condition, T item, T type, T effect){
		this.condition = condition;
		this.item = item;
		this.type = type;
		this.effect = effect; // how bad if player did not complete/give wrong item - minor, mediocre, severe
		next = null;
	}
}

public class LinkList<T> {
	public int size = 0;
	Node <T> head = null;

	public bool isEmpty(){
		return (head.Equals(null)); //if empty, returns true
	}

	// display only a note that has no effect
	public void addNote(T note) {
		Node<T> newNode = new Node<T>(note);
		newNode.next = head;
		head = newNode;
		size++;
	}

	//creating/inserting nodes before head
	public void insertNode(T condition, T item, T type, T effect){
		Node <T> newNode = new Node<T>(condition, item, type, effect); // make new node
		newNode.next = head; 	// newNode.next point to previous node
		head = newNode; 		// new Node is the head now
		size++; 				// increase size amount
	}

	// remove from the top of the list
	public void remove(){
		Node <T> Ref = head;
		head = head.next;
	}

	public T getNode(int num, string type){
		int count = 0;
		Node<T> node = head;

		//if node not empty
		while (node != null){
			if (count == num){
				if (type.Equals("condition")) return node.condition;
				else if (type.Equals("item")) return node.item;
				else if (type.Equals("type")) return node.type;
				else if (type.Equals("effect")) return node.effect;
			}
			count++;
			node = node.next; //check next node
		}
		return default (T);
	}

	//print the list
	public void display(){
		//int count = 0;
		Node <T> node = head;

		//if node not empty
		while (node != null){
			//print current node
			Debug.Log(node.condition + " | " + node.item + " | " + node.type + "\n");
			node = node.next; //check next node
		}
	}
}