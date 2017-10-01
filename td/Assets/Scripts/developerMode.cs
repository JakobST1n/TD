using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class developerMode : MonoBehaviour {

	public string output = "";
	public string stack = "";

	bool developerModeActive;
	GameObject pnlCanvas;
	Text lblConsoleLog;

	void Start () {
		pnlCanvas = transform.Find ("Canvas").gameObject;
		lblConsoleLog = pnlCanvas.transform.Find ("consoleLog").gameObject.GetComponent <Text>();

		lblConsoleLog.text = "";
	}

	void Update () {
		
		if (PlayerPrefs.HasKey ("developerMode")) {
			if (PlayerPrefs.GetInt ("developerMode") == 1) { developerModeActive = true; }
			else { developerModeActive = false; }
		}

		if (developerModeActive) {
			this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		} else {
			this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
		}
	}
		

	void OnEnable() {
		Application.logMessageReceived += HandleLog;
	}
	void OnDisable() {
		Application.logMessageReceived -= HandleLog;
	}
	public void HandleLog(string logString, string stackTrace, LogType type) {
		string backLog = lblConsoleLog.text;
		lblConsoleLog.text = logString + "\n" + backLog;
	}

}
