using System.Collections.Generic;
using Priority_Queue;
using System.Linq;

public class DijkstrasAgent : PathAgent {
	public override Pathfinder createPathfinder() {
		return new DijkstrasPathfinder();
	}
}

public class DijkstrasPathfinder : Pathfinder {
	public override List<int> findPath(int start, int goal) {
		SimplePriorityQueue<int> frontier = new SimplePriorityQueue<int>(); // Nodes to explore
		Dictionary<int, int> visitedFrom = new Dictionary<int, int>(); 		// Keep track which node visited from previous node
		Dictionary<int, int> costSoFar = new Dictionary<int, int>();		// Keep track of total cost so far
		List<int> path = new List<int>();

		// note: waypointset is from pathAgent class

		frontier.Enqueue(start, 0); // add starting node with 0 cost
		visitedFrom[start] = -1; 	// start node doesn't have predecessor
		costSoFar[start] = 0;		// Total cost at start is 0

		// if stack not empty/not all nodes visited...
		while (frontier.Count > 0) {
			//Debug.Log("frontier.Count: " + frontier.Count);

			int current = frontier.Dequeue(); //remove element from queue and store as current
			//Debug.Log("Current node: " + current + " Goal: " + goal);

			if (current == goal) {
				//Debug.Log("Found");
				break; //stop when goal found 
			}

			//add next neightbour
			List<int> neighbours = navGraph.neighbours(current);

			foreach (int next in neighbours){
				// Add cost of new edge to current
				int nextCost = costSoFar[current] + navGraph.cost(current, next);

				// If node not already added OR Check if new lower cost path has been found
				if (!costSoFar.ContainsKey(next) || nextCost < costSoFar[next]) {
					//Debug.Log("Next " + next + " Current: " + current);

					frontier.Enqueue(next, nextCost); 	// nextCost -> Prioritise node by cost
					visitedFrom[next] = current; 		// add to show predecessor (next, current)
					costSoFar[next] = nextCost;			// add cost for each edge

					//Debug.Log("costSoFar[" + next + "]: " + costSoFar[next]);
				}
			}
		}
		//Debug.Log("frontier empty");

		List<int> key = new List<int>();
		List<int> value = new List<int>();

		key = visitedFrom.Keys.ToList();
		value = visitedFrom.Values.ToList();

		// checking key and values...
		/*for (int i = 0; i < key.Count; i++){
			Debug.Log("key: " + key[i] + " v: " + value[i]);
		}*/

		//Debug.Log("Add last key");
		path.Add(key[key.Count - 1]); // add last key

		// start from last key and value
		int a = key.Count-1;
		int v = value[value.Count - 1];

		while (a > 0) {
			//Debug.Log("key: " + key[a - 1] + "=? v: " + v);

			// check if key matches v
			// which is the predecessor that points to the previous node connected to it
			if (key[a - 1].Equals(v)) {
				//Debug.Log("Add " + key[a-1]);
				path.Add(key[a-1]); // add node
				v = value[a-1]; 	// add new value to check
			}
			a--; // decrement to look at next key
			//if (a <= 0) break; //stop last key to check
		}
		path.Reverse(); //reverse the list as we added the nodes in reverse
		return path;
	}
}	