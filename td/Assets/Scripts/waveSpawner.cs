using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour {
	
	[Header("Attributes")]
	public float TimeBetweenWaves;
	public float SpawnRate;
	
	[Header("Objects")]
	public Transform SpawnPoint;
	public Transform PathWay;
	public Text WaveCountdownText;
	
	[Header("Every possible enemy")]
	public EnemyType[] Enemies;
	
	[Header("Scripting vars")]
	public Player Player;            // Reference to the player object, should be set in designer

	private Transform _parentObject;
	private List<Vector3> _waypoints = new List<Vector3>();
	
	public static int EnemiesAlive = 0;
	private float _countdown = 2f;
	private int _waveIndex = 0;
	private bool _lastWavePayed;

	void Awake() {
		foreach (Transform child in PathWay) {
			_waypoints.Add (child.position);
		}
	}
	
	void Start() {
		_parentObject = transform.Find ("enemies").gameObject.GetComponent <Transform> ();
	}
	
	void Update () {
		if (EnemiesAlive > 0) {
			return;
		}

		if (EnemiesAlive == 0 && !_lastWavePayed) {
			Player.Payday();
			_lastWavePayed = true;
		}

		if (_countdown <= 0f) {
			StartCoroutine(SpawnWave());  // TODO Bytt ut denne med SpawnWaveRand (Gjør ferdig SpawnWaveRand)
			_countdown = TimeBetweenWaves;
			return;
		}
		Debug.Log(_countdown);
		_countdown -= Time.deltaTime;
		_countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);
		//waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

	private int WaveEnemyCount(int waveNum) {
		// 10.64 * e^0,57x
		float pow = (float) Math.Pow( Math.E, 0.57f * waveNum);
		return (int) Math.Floor(10.64f * pow);
	}

	private float EnemyAmountThing(int currentEnemy, int maxTypes) {
		// TODO Change the for loop into a faster method
		float rest = 1;

		for (int i=1; i <= maxTypes; i++) {
			if (i != maxTypes) { rest = rest / 2; }
			if (i == currentEnemy + 1) { return rest; }
		}
		
		return 0;
	}
	
	IEnumerator SpawnWave () {
		int enemiesToSpawn = WaveEnemyCount(_waveIndex);
		EnemiesAlive = enemiesToSpawn;
		List<WaveElement> wave = new List<WaveElement>();
		
		for (int i = 0; i < Enemies.Length; i++) {
			EnemyType enemy = Enemies[i];

			float amount = enemiesToSpawn * EnemyAmountThing(i, Enemies.Length);
			if (amount >= 1) {
				wave.Add(new WaveElement {Prefab = enemy.Enemy, Amount = (int)Math.Floor(amount)} );
			}
			
		}

		foreach (var enemyType in wave) {
			for (int i = 0; i < enemyType.Amount; i++) {
				SpawnEnemy(enemyType.Prefab);
				yield return new WaitForSeconds(1f / SpawnRate);
			}
			yield return new WaitForSeconds(1f / 100f);
		}

		SpawnRate = SpawnRate * 2;
		_waveIndex++;
		_lastWavePayed = false;
	}

	IEnumerator SpawnWaveRand() {
		int enemiesToSpawn = WaveEnemyCount(_waveIndex);
		int[] waveEnemies = new int[Enemies.Length - 1];
		EnemiesAlive = enemiesToSpawn;

		for (int i = 1; i < enemiesToSpawn; i++) {
			int currentEnemyInt = UnityEngine.Random.Range(0, Enemies.Length - 1);
			EnemyType currentEnemy = Enemies[currentEnemyInt];
			
			if (_waveIndex <= 1) {
			} else if (_waveIndex <= 5) {
				
			}
			
			
			yield return new WaitForSeconds(1f / SpawnRate);
		}
		
		SpawnRate = SpawnRate * 2;
		_waveIndex++;
	}

	void SpawnEnemy (GameObject enemyPrefab) {
		GameObject newEnemy = Instantiate (enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity, _parentObject);
		Enemy script = newEnemy.GetComponent <Enemy> ();
		Transform transform = newEnemy.GetComponent <Transform>();

		script.Waypoints = _waypoints;
		script.Player = Player;
		transform.position = new Vector3 (0.93f, 0.483f, 0f);
	}
	
	[System.Serializable]
	public class EnemyType {
		public string Name;
		public GameObject Enemy;
		[Range(0, 1)]
		public float Percentage;
	}

	public class WaveElement {
		public GameObject Prefab { get; set; }
		public int Amount { get; set; }
	}
	
}