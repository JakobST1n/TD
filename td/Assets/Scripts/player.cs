﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int InitialHealth;
    public int StartingMoney;
	public MainGui MainGui;
	
	private GameObject[] _towers;
    private int _playerMoney;
    private int _playerScore;
	private int _playerHealth;

    void Awake() {
	    /* This method initializes the player class */
        _playerMoney = StartingMoney;
		_playerHealth = InitialHealth;
	    InvokeRepeating ("GameStateWatcher", 0f, 0.5f);
    }

	#region stats
    public int Score() {
        return _playerScore;
    }

    public void ScoreAdd(int points) {
        _playerScore += points;
    }

    public int Money() {
        return _playerMoney;
    }

    public void MoneyAdd(int sum) {
        _playerMoney += sum;
    }

    public void MoneySubtract(int sum) {
        _playerMoney -= sum;
    }

	public int Health() {
		return _playerHealth;
	}

	public int HealthAsPercentage()
	{
		return Mathf.RoundToInt((InitialHealth * _playerHealth) / 100.0f);  // Basic percentage calc...
	}

	public void DecreaseHealth(int hp) {
		_playerHealth -= hp;
	}
	#endregion

	public void SpawnTower(GameObject towerType) {
		GameObject tower = Instantiate (towerType, new Vector3 (0, 0, 0), Quaternion.identity, transform.Find ("towers").transform);
		Tower script = tower.GetComponent <Tower>();
		script.Player = this;
	}

	public void GameStateWatcher() {
		if (_playerHealth <= 0) {
			MainGui.GameOverScreen(_playerScore);
		}
	}

}