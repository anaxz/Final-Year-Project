using UnityEngine;
using System.Text;
using System.Collections.Generic;

public abstract class Pathfinder {
	public Graph navGraph;
	public abstract List<int> findPath (int start, int goal);
}

public abstract class PathAgent : MonoBehaviour {
	//MonoBehaviour m = new MonoBehaviour();

	// Set from inspector
	public GameObject waypointSet;

	// Waypoints
	protected static WaypointGraph waypoints;
	protected int? current;

	protected List<int> path;
	protected Pathfinder pathfinder;

	public float speed;
	protected static float NEARBY = 0.2f;
	protected static System.Random rnd = new System.Random();

	public abstract Pathfinder createPathfinder ();

	protected int[] Targets = { 13, 5, 6, 8, 9, 11, 12 }; // node targets - patient (5-12) or exits (13)

	void Start () {
		waypoints = new WaypointGraph (waypointSet);
		path = new List<int> ();
		pathfinder = createPathfinder ();
		pathfinder.navGraph = waypoints.navGraph;
	}
		
	void Update () {
		if (path.Count == 0){
			// We don't know where to go next
			generateNewPath();
		}
		else {
			// Get the next waypoint position
			GameObject next = waypoints[path[0]];
			Vector3 there = next.transform.position; 	// target position
			Vector3 here = transform.position;			// current position

			// Are we there yet?
			float distance = Vector3.Distance(here, there);
			if (distance < NEARBY){
				// We're here

				// rotate the character to face the direction they're heading
				//GameObject staff = GameObject.Find("Staff");
				//staff.GetComponent<StaffController>().RotateTo(there);

				current = path[0];
				path.RemoveAt(0);
				//Debug.Log("Arrived at waypoint " + current);
			}
		} // END else
		waypoints.drawEdges(); // Draw lines as the edge - For debugging purposes
	}

	void FixedUpdate() {
		if (path.Count > 0) {
			GameObject next = waypoints[path[0]]; 		// get next target position of the node
			Vector3 position = next.transform.position; // convert target position to vector3

			// rotate the character to face the direction they're heading
			GameObject staff = GameObject.FindWithTag("Nurse");
			staff.GetComponent<StaffController>().RotateTo(position);

			// make the character move towards next position
			transform.position = Vector3.MoveTowards(transform.position, position, speed);
		}
	}

	// set speed of character - use to stop character then reset speed again
	public void setSpeed(float num) {
		speed = num;
	}
		
	protected void generateNewPath() {

		if (current != null) {
			// We know where are
			List<int> nodes = waypoints.navGraph.nodes ();

			if (nodes.Count > 0) {
				// Pick a random node to aim for
				//int target = nodes [rnd.Next (nodes.Count)]; <-- original

				int target = Targets[rnd.Next(0, Targets.Length)]; // choose random node to visit
				//Debug.Log ("New target: " + target);
				// Find a path from here to there
				path = pathfinder.findPath (current.Value, target);
				//Debug.Log ("New path: " + writePath(path));

			} else {
				// There are zero nodes
				Debug.Log ("No waypoints - can't select new target");
			}

		} else {
			// We don't know where we are

			// Find the nearest waypoint
			int? target = waypoints.findNearest (transform.position);

			if (target != null) {
				// Go to target
				path.Clear ();
				path.Add (target.Value);

				//Debug.Log ("Heading for nearest waypoint: " + target);
			} else {
				// Couldn't find a waypoint
				Debug.Log ("Can't find nearby waypoint to target");
			}
		
		}
	}

	public static string writePath(List<int> path) {
		var s = new StringBuilder();
		bool first = true;
		foreach(int t in path) {
			if(first) {
				first = false;
			} else {
				s.Append(", ");
			}
			s.Append(t);
		}    
		return s.ToString();
	}
}



