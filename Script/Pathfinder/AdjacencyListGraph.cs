using System.Collections.Generic;

public interface Graph {
	bool addNode(int a);          // true if node added
	bool addEdge(int a, int b, int c);   // true if edge added

	List<int> nodes();
	List<int> neighbours(int a);

	int cost(int a, int b); 	  // Weight of an edge
}

public class AdjacencyListGraph : Graph {
	Dictionary<int, List<int>> dict1 = new Dictionary<int, List<int>>(); // <node, nodes connected to>
	Dictionary<int, Dictionary<int, int>> dict2 = new Dictionary<int, Dictionary<int, int>>(); // <node, node[weight]>
	Dictionary<int, int>[] connectedFrom = new Dictionary<int, int>[100];

	List<int> key = new List<int>();
	List<int>[] values = new List<int>[100]; //state how the no. of array of lists

	public List<int> nodes(){
		return key;
	}

	//return the adjacency list of a node
	public List<int> neighbours(int a){
		//Debug.Log(dict[a]);
		return dict1[a];
	}

	public bool addNode(int a){
		//if key not already added, add new key with empty list
		if (!dict2.ContainsKey(a)){
			key.Add(a); 					// add new key
			values[a] = new List<int>(); 	//intialise list
			dict1.Add(a, values[a]); 		// add new key with new empty list
			return true;
		}
		return false;
	}

	public bool addEdge(int a, int b, int c){
		dict1[a].Add(b); //add values to key

		connectedFrom[a] = new Dictionary<int, int>();
		//Debug.Log("Adding  a: " + a + ", b: " + b + ", c: " + c);

		// if Key not already added
		if (!dict2.ContainsKey(a)) {
			dict2.Add(a, connectedFrom[a]); //add (node, dictionary)
			//Debug.Log("Added a: " + a);
		}

		connectedFrom[a].Add(b, c); //add (node, cost)
		//Debug.Log("Added b: " + b + " c: " + c + " connectedFrom[a][b]: " + connectedFrom[a][b]);

		return true;
	}

	public int cost(int a, int b) {
		//Debug.Log("a: " + a + " b: " + b);

		// Check if dict2 has key, return cost of that edge
		if (dict2[a].ContainsKey(b)){
			//Debug.Log("Cost: " + dict2[a][b]);
			return dict2[a][b];
		}
		return 0; //return 0 if node b is not connected to node a
	}
}