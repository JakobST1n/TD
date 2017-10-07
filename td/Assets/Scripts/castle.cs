using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

	private void Update () {
		//CheckThing();
	}

	private void CheckThing() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
		
		foreach (var enemy in enemies) {
			var distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy <= 0) {
				Debug.Log("INTRUDER!");
			}
		}
	}


}
