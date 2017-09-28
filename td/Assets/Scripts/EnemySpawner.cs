using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Enemy enemyPrefab;
	public Transform pathWay;

	int wave;
	int next = 1;
	int n = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		n++;


		if (n == next) {
			Debug.Log ("Spawn");
			n = 0;
			next = (int)Random.Range (50, 400);

			Enemy newEnemy = Instantiate (enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			newEnemy.GetComponent <Enemy> ().pathWay = pathWay;
			newEnemy.GetComponent <Enemy> ().speed = Random.Range (0.3f, 1.2f);
		}
	}
}
