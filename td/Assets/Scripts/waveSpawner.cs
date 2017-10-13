using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public static int EnemiesAlive = 0;

	public Wave[] Waves;

	public Transform SpawnPoint;

	public float TimeBetweenWaves = 5f;
	private float _countdown = 2f;

	public Text WaveCountdownText;


	private int _waveIndex = 0;

	void Update ()
	{
		if (EnemiesAlive > 0) {
			return;
		}

		if (_waveIndex == Waves.Length)
		{
			// WIN LEVEL!!!
			this.enabled = false;
		}

		if (_countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			_countdown = TimeBetweenWaves;
			return;
		}

		_countdown -= Time.deltaTime;

		_countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);

		//waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

	IEnumerator SpawnWave ()
	{
		Wave wave = Waves[_waveIndex];

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

[System.Serializable]
public class Wave {
	public GameObject Enemy;
	public int Count;
	public float Rate;
}