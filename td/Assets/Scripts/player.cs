using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public int initialHealth;
    public int startingMoney;

	private GameObject[] towers;
    private int playerMoney;
    private int playerScore;
	private int playerHealth;

    void Awake() {
        playerMoney = startingMoney;
		playerHealth = initialHealth;
    }

	#region stats
    public int score() {
        return playerScore;
    }

    public void scoreAdd(int points) {
        playerScore += points;
    }

    public int money() {
        return playerMoney;
    }

    public void moneyAdd(int sum) {
        playerMoney += sum;
    }

    public void moneySubtract(int sum) {
        playerMoney -= sum;
    }

	public int health() {
		return playerHealth;
	}

	public void decreaseHealth(int hp) {
		playerHealth -= hp;
	}
	#endregion

	public void spawnTower(GameObject towerType) {
		GameObject tower = Instantiate (towerType, new Vector3 (0, 0, 0), Quaternion.identity, transform.Find ("towers").transform);
		tower script = tower.GetComponent <tower>();
		script.player = this;
	}

}
