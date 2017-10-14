using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
	
	public Transform SpawnPoint;
	public Text WaveCountdownText;
	public float TimeBetweenWaves = 5f;
	[Header("Scripting vars")]
	public Player Player;            // Reference to the player object, should be set in designer

	public static int EnemiesAlive = 0;
	private float _countdown = 2f;
	private int _waveIndex = 0;

	void Update () {
		if (EnemiesAlive > 0) {
			return;
		}

		if (_countdown <= 0f) {
			StartCoroutine(SpawnWave());
			_countdown = TimeBetweenWaves;
			return;
		}

		_countdown -= Time.deltaTime;
		_countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);
		//waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

	IEnumerator SpawnWave () {
		int waveNum = 1;
		int gdshj = Mathf.FloorToInt(10.64 * (Math.Pow(Math.E, (0.57 * waveNum)))));

		EnemiesAlive = wave.Count;

		for (int i = 0; i < wave.Count; i++)
		{
			SpawnEnemy(wave.Enemy);
			yield return new WaitForSeconds(1f / wave.Rate);
		}

		_waveIndex++;
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation);
	}

}