using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashColor : MonoBehaviour {

	public Color Color1;
	public Color Color2;
	public float TransitionTime;
	public bool HideAfterCountdown;
	public float DisplayTime;

	private Text _text;
	public float TimeSinceBorn;

	void Start() {
		TimeSinceBorn = 0;
		_text = gameObject.GetComponent<Text>();
		StartCoroutine("Flash");
	}
	
	void OnEnable() {
		TimeSinceBorn = 0;
		_text = gameObject.GetComponent<Text>();
		StartCoroutine("Flash");
	}

	void Update () {
		if (HideAfterCountdown) {
			TimeSinceBorn += Time.deltaTime;
			if (TimeSinceBorn >= DisplayTime) {
				gameObject.SetActive(false);
			}
		}
	}

	IEnumerator Flash() {
		while (true) {
			float elapsedTime = 0.0f;
			while (elapsedTime < TransitionTime) {
				elapsedTime += Time.deltaTime;
				_text.color = Color.Lerp(Color1, Color2, (elapsedTime / TransitionTime));
				yield return null;
			}

			elapsedTime = 0.0f;
			while (elapsedTime < TransitionTime) {
				elapsedTime += Time.deltaTime;
				_text.color = Color.Lerp(Color2, Color1, (elapsedTime / TransitionTime));
				yield return null;
			}
			yield return null;
		}
	}

	
}
