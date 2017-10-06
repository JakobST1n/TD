using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class gameStats : MonoBehaviour {

	public player Player;
    GameObject canvas;
    Text txtMoney;
    Text txtScore;
    int displayedScore;
    int displayedMoney;

    void Start() {
		canvas = transform.GetChild (0).gameObject;
		txtMoney = canvas.transform.Find ("playerMoney").gameObject.GetComponent <Text>();
		txtScore = canvas.transform.Find ("playerScore").gameObject.GetComponent <Text>();
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

	}

	void updateScore(int newScore) {
		txtScore.text = ("Score: " + newScore.ToString ());
	}

	void updateMoney(int newMoney) {
		txtMoney.text = ("Money: " + newMoney.ToString () + "$");
	}

}
