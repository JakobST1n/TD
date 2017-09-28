using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public float initialHp;
	public Transform pathWay;

	List<Vector3> waypoints = new List<Vector3>();
	Vector3 currentWaypointPosition;
	int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		foreach (Transform child in pathWay) {
			waypoints.Add (child.position);
		}
		currentWaypointPosition = new Vector3(waypoints [currentWaypoint].x, 0.604f, waypoints[currentWaypoint].z);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position == currentWaypointPosition && currentWaypoint < waypoints.Count - 1) {
			currentWaypoint += 1;
			currentWaypointPosition = new Vector3(waypoints [currentWaypoint].x, 0.604f, waypoints[currentWaypoint].z);
		}

		transform.position = Vector3.MoveTowards (transform.position, currentWaypointPosition, speed * Time.deltaTime);
	}
		
}
