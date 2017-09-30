﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	/* This is a general class that contains an enemy, 
	 * Currently it follows the pathway, and dies when reacing the end */

	public float speed;  // Speed multiplier
	public float initialHp;  // HealthPoints
	public List<Vector3> waypoints;  // Pathway waypoints, should be set by the spawner

	Vector3 waypointPos;  // Current waypoint position
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
