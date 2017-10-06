using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class developerMode : MonoBehaviour {

	public string output = "";
	public string stack = "";
	public bool cheatsAllowed;

	GameObject pnlCanvas;
	GameObject pnlCheats;
	Button btnToggleCheats;
	Text lblConsoleLog;

	bool developerModeActive;
	bool cheatMenuOpen;

	void Start () {
		/* Panels */
		pnlCanvas = this.gameObject.transform.GetChild (0).gameObject;
		pnlCheats = pnlCanvas.transform.Find ("cheatMenu").gameObject;
		/* Buttons */
		/* Button handlers */
		/* Lablels */
		lblConsoleLog = pnlCanvas.transform.Find ("consoleLog").gameObject.GetComponent <Text>();
		/* Do setup */
		lblConsoleLog.text = "";

		if (cheatsAllowed) {
			btnToggleCheats = pnlCanvas.transform.Find ("toggleCheats").gameObject.GetComponent <Button> ();
			if (btnToggleCheats != null) { btnToggleCheats.onClick.AddListener (btnToggleCheatsHandler); }
			cheatMenuOpen = false;
		} else {
			pnlCanvas.transform.Find ("toggleCheats").gameObject.SetActive (false);
		}
		pnlCheats.SetActive (false);
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

	void btnToggleCheatsHandler() {
		/* Handler for btnToggleCheats */
		if (cheatsAllowed) {
			cheatMenuOpen = !cheatMenuOpen;
			pnlCheats.SetActive (cheatMenuOpen);
		}
	}
		
	#region GetDebugLog
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
	#endregion

}
