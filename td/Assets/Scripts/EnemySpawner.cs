using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	/* This is a class that spawns an enemy with a random interval
	 * it is not very good, but is what it needs to be for testing purposes */
	// TODO Add wave system with increasing difficulty
	public Enemy EnemyPrefab;
	public Transform PathWay;
	[Header("Scripting vars")]
	public Player Player;            // Reference to the player object, should be set when instantiating

	private Transform _parentObject;

	List<Vector3> _waypoints = new List<Vector3>();
	int _next = 1;
	int _n = 0;

	void Awake() {
		foreach (Transform child in PathWay) {
			_waypoints.Add (child.position);
		}
	}

	void Start() {
		_parentObject = transform.Find ("enemies").gameObject.GetComponent <Transform> ();
	}

	void Update () {
		_n++;

		if (_n == _next) {
			_n = 0;
			_next = (int)Random.Range (50, 400);

			Enemy newEnemy = Instantiate (EnemyPrefab, new Vector3(0, 0, 0), Quaternion.identity, _parentObject);
			Enemy script = newEnemy.GetComponent <Enemy> ();
			Transform transform = newEnemy.GetComponent <Transform>();

			script.Waypoints = _waypoints;
			script.Speed = Random.Range (0.3f, 1.2f);
			script.Player = Player;
			transform.position = new Vector3 (0.93f, 0.483f, 0f);
		}

	}
}
