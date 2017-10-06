using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public int startingMoney;

	GameObject[] towers;

    int playerMoney;
    int playerScore;

	bool placingTower;

    void Awake() {
        playerMoney = startingMoney;
    }

	#region stats
    public int score() {
        return playerScore;
    }

    public void score(int points) {
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
	#endregion

	public void spawnTower(GameObject towerType) {
		GameObject tower = Instantiate (towerType, new Vector3 (0, 0, 0), Quaternion.identity, transform.Find ("towers").transform);
		tower script = tower.GetComponent <tower>();
		script.player = this;
	}

}
