using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class gameStats : MonoBehaviour {

	public player Player;
    GameObject canvas;
    Text txtMoney;
    Text txtScore;
	Text txtHp;
    int displayedScore;
    int displayedMoney;
	int displayedHealth;

    void Start() {
		canvas = transform.GetChild (0).gameObject;
		txtMoney = canvas.transform.Find ("playerMoney").gameObject.GetComponent <Text>();
		txtScore = canvas.transform.Find ("playerScore").gameObject.GetComponent <Text>();
		txtHp = canvas.transform.Find ("playerHealth").gameObject.GetComponent <Text>();
    }

	void Update () {

		if (Player.money () != displayedMoney) {
			displayedMoney = Player.money ();
			updateMoney (displayedMoney);
		}

		if (Player.score () != displayedScore) {
			displayedScore = Player.score ();
			updateScore (displayedScore);
		}

		if (Player.health () != displayedHealth) {
			displayedHealth = Player.health ();
			updateHealth (displayedHealth);
		}

	}

	void updateScore(int newScore) {
		txtScore.text = ("Score: " + newScore.ToString ());
	}

	void updateMoney(int newMoney) {
		txtMoney.text = ("Money: " + newMoney.ToString () + "$");
	}

	void updateHealth(int newHp) {
		txtHp.text = ("HP: " + newHp.ToString ());
	}

}
