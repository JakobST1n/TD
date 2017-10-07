﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	/* This is a class that spawns an enemy with a random interval
	 * it is not very good, but is what it needs to be for testing purposes */
	// TODO Add wave system with increasing difficulty

	public Enemy enemyPrefab;
	public Transform pathWay;
	[Header("Scripting vars")]
	public player player;            // Reference to the player object, should be set when instantiating

	private Transform parentObject;

	List<Vector3> waypoints = new List<Vector3>();
	int next = 1;
	int n = 0;

	void Awake() {
		foreach (Transform child in pathWay) {
			waypoints.Add (child.position);
		}
	}

	void Start() {
		parentObject = transform.Find ("enemies").gameObject.GetComponent <Transform> ();
	}

	void Update () {
		n++;

		if (n == next) {
			n = 0;
			next = (int)Random.Range (50, 400);

			Enemy newEnemy = Instantiate (enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity, parentObject);
			Enemy script = newEnemy.GetComponent <Enemy> ();
			Transform transform = newEnemy.GetComponent <Transform>();

			script.waypoints = waypoints;
			script.speed = Random.Range (0.3f, 1.2f);
			script.player = player;
			transform.position = new Vector3 (0.93f, 0.483f, 0f);
		}

	}
}
