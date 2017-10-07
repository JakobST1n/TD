using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameStats : MonoBehaviour {

	public Player Player;
    GameObject _canvas;
    Text _txtMoney;
    Text _txtScore;
	Text _txtHp;
    int _displayedScore;
    int _displayedMoney;
	int _displayedHealth;

    void Start() {
		_canvas = transform.GetChild (0).gameObject;
		_txtMoney = _canvas.transform.Find ("playerMoney").gameObject.GetComponent <Text>();
		_txtScore = _canvas.transform.Find ("playerScore").gameObject.GetComponent <Text>();
		_txtHp = _canvas.transform.Find ("playerHealth").gameObject.GetComponent <Text>();
    }

	void Update () {

		if (Player.Money () != _displayedMoney) {
			_displayedMoney = Player.Money ();
			UpdateMoney (_displayedMoney);
		}

		if (Player.Score () != _displayedScore) {
			_displayedScore = Player.Score ();
			UpdateScore (_displayedScore);
		}

		if (Player.Health () != _displayedHealth) {
			_displayedHealth = Player.Health ();
			UpdateHealth (_displayedHealth);
		}

	}

	void UpdateScore(int newScore) {
		_txtScore.text = ("Score: " + newScore.ToString ());
	}

	void UpdateMoney(int newMoney) {
		_txtMoney.text = ("Money: " + newMoney.ToString () + "$");
	}

	void UpdateHealth(int newHp) {
		_txtHp.text = ("HP: " + newHp.ToString ());
	}

}
