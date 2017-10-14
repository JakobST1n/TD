using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	/* This is a general class that contains an enemy, 
	 * Currently it follows the pathway, and dies when reacing the end */

	[Header("Attributes")]
	public float Speed;  // Speed multiplier
	public int InitialHp;  // HealthPoints
	public int Damage;
	public List<Vector3> Waypoints;  // Pathway waypoints, should be set by the spawner
	
	[Header("Scripting vars")]
	public Player Player;            // Reference to the player object, should be set when instantiating

	private Vector3 _waypointPos;  // Current waypoint position
	private int _waypointNum = -1;  // Using minus one so that first addition returns 0, first element in array

	void Update () {
		if (Player.GameIsPaused()) { return; }  // This ensures that the game stays paused
		
		if ( (transform.position == _waypointPos && _waypointNum + 1 < Waypoints.Count) || _waypointNum == -1) {
			_waypointNum++;
			_waypointPos = new Vector3 (Waypoints [_waypointNum].x, 0.483f, Waypoints [_waypointNum].z);
		}

		float transformStep = Speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, _waypointPos, transformStep);

		// Selfdestruct if object reached the end
		if (_waypointNum + 1 >= Waypoints.Count) {
			WaveSpawner.EnemiesAlive--;
			Player.DecreaseHealth (Damage);
			Destroy (gameObject);
			return;
		}
	}
		
}
