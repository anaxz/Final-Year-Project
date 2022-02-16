using UnityEngine;
using System.Collections.Generic;

public class WaypointGraph {

	public Graph navGraph;
	protected List<GameObject> waypoints;

	public GameObject this[int i] {
		get { return waypoints [i]; }
		set { waypoints [i] = value; }
	}

	public WaypointGraph(GameObject waypointSet) {
		waypoints = new List<GameObject> ();
		navGraph = new AdjacencyListGraph ();

		findWaypoints (waypointSet);
		buildGraph ();
	}
		
	private void findWaypoints(GameObject waypointSet) {

		if (waypointSet != null) {
			foreach (Transform t in waypointSet.transform) {
				waypoints.Add (t.gameObject);
			}
			//Debug.Log("Found " + waypoints.Count + " waypoints.");

		} else {
			Debug.Log ("No waypoints found.");

		}
	}

	private void buildGraph() {
		int n = waypoints.Count;

		navGraph = new AdjacencyListGraph ();
		for (int i = 0; i < n; i++) {
			navGraph.addNode(i);
		}
		navGraph.addEdge(13, 0, 1);
		navGraph.addEdge(0, 13, 1);

		navGraph.addEdge(0, 1, 1); //from node, to node, cost
		navGraph.addEdge(1, 2, 1);
		navGraph.addEdge(2, 3, 1);

		navGraph.addEdge(3, 4, 2);
		navGraph.addEdge(4, 3, 3);

		navGraph.addEdge(4, 5, 1);
		navGraph.addEdge(5, 4, 1);
		navGraph.addEdge(4, 6, 1);
		navGraph.addEdge(6, 4, 1);

		navGraph.addEdge(4, 7, 2);
		navGraph.addEdge(7, 4, 3);

		navGraph.addEdge(7, 8, 1);
		navGraph.addEdge(8, 7, 1);
		navGraph.addEdge(7, 9, 1);
		navGraph.addEdge(9, 7, 1);

		navGraph.addEdge(7, 10, 2);
		navGraph.addEdge(10, 7, 3);

		navGraph.addEdge(10, 11, 1);
		navGraph.addEdge(11, 10, 1);
		navGraph.addEdge(10, 12, 1);
		navGraph.addEdge(12, 10, 1);
	}

	public int? findNearest(Vector3 here) {
		int? nearest = null;

		if (waypoints.Count > 0) {
			nearest = 0;
			Vector3 there = waypoints [0].transform.position;
			float minDistance = Vector3.Distance (here, there);

			for (int i = 1; i < waypoints.Count; i++) {
				there = waypoints[i].transform.position;
				float distance = Vector3.Distance (here, there);
				if (distance < minDistance) {
					nearest = i;
				}
			}
		}
		return nearest;
	}

	// Draw lines as the edge - For debugging purposes
	public void drawEdges(){
		for (int i = 0; i < waypoints.Count; i++){
			//Debug.Log("Total neighbours: " + navGraph.neighbours(i).Count);
			List<int> temp = navGraph.neighbours(i); // store a node's neighbour

			// iterate and draw nodes that are connected with current node
			for (int j = 0; j < temp.Count; j++) {
				int index = temp[j];
				Debug.DrawLine(waypoints[i].transform.position, waypoints[index].transform.position, Color.red);
			}
		}
	}
}
