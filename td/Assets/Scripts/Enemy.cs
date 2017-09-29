using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public float initialHp;
	public List<Vector3> waypoints;

	Vector3 waypointPos;
	int waypointNum = -1;  // Using minus one so that first addition returns 0, first element in array

	void Update () {
		updateWaypoint ();

		float transformStep = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, waypointPos, transformStep);

		if (waypointNum == waypoints.Count - 1) {
			Destroy (gameObject);
		}
	}

	void updateWaypoint() {
		if ( (transform.position == waypointPos && waypointNum < waypoints.Count - 1) || waypointNum == -1) {
			waypointNum++;
			waypointPos = new Vector3 (waypoints [waypointNum].x, 0.483f, waypoints [waypointNum].z);
		}
	}
		
}
