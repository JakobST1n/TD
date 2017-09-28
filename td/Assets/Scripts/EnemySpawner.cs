using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Enemy enemyPrefab;
	public Transform pathWay;
	public Transform gameWorld;

	int wave;
	int next = 1;
	int n = 0;

	void Update () {
		n++;

		if (n == next) {
			n = 0;
			next = (int)Random.Range (50, 400);

			Enemy newEnemy = Instantiate (enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameWorld);
			newEnemy.GetComponent <Enemy> ().pathWay = pathWay;
			newEnemy.GetComponent <Enemy> ().speed = Random.Range (0.3f, 1.2f);
		}
	}
}
