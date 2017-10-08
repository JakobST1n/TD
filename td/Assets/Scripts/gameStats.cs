using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameStats : MonoBehaviour {

	public Player Player;
    private GameObject _canvas;
    private Text _txtMoney;
    private Text _txtScore;
	private Text _txtHp;
	private Slider _sldHp;
    private int _displayedScore;
    private int _displayedMoney;
	private int _displayedHealth;

    private void Start() {
		_canvas = transform.GetChild (0).gameObject;
		_txtMoney = _canvas.transform.Find ("playerMoney").gameObject.GetComponent <Text>();
		_txtScore = _canvas.transform.Find ("playerScore").gameObject.GetComponent <Text>();
	    _sldHp = _canvas.transform.Find("playerHealth").gameObject.GetComponent<Slider>();
    }

	private void Update () {

		if (Player.Money () != _displayedMoney) {
			_displayedMoney = Player.Money ();
			UpdateMoney (_displayedMoney);
		}

		if (Player.Score () != _displayedScore) {
			_displayedScore = Player.Score ();
			UpdateScore (_displayedScore);
		}

		if (Mathf.RoundToInt(Player.HealthAsPercentage()) != Mathf.RoundToInt(_displayedHealth)) {
			_displayedHealth = Player.HealthAsPercentage();
			UpdateHealth (_displayedHealth);

			if (_displayedHealth <= 10) {
				_txtScore.color = Color.red;
			}
		}

	}

	private void UpdateScore(int newScore) {
		_txtScore.text = ("Score: " + newScore.ToString ());
	}

	private void UpdateMoney(int newMoney) {
		_txtMoney.text = ("Money: " + newMoney.ToString () + "$");
	}

	private void UpdateHealth(int newHp) {
		_sldHp.value = newHp;
	}

}
