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
		if ( (transform.position == waypointPos && waypointNum + 1 < waypoints.Count) || waypointNum == -1) {
			waypointNum++;
			waypointPos = new Vector3 (waypoints [waypointNum].x, 0.483f, waypoints [waypointNum].z);
		}

		float transformStep = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, waypointPos, transformStep);

		// Selfdestruct if object reached the end
		if (waypointNum + 1 >= waypoints.Count) {
			Destroy (gameObject);
		}
	}
		
}
