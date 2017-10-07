using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DeveloperMode : MonoBehaviour {

	public string Output = "";
	public string Stack = "";
	public bool CheatsAllowed;

	GameObject _pnlCanvas;
	GameObject _pnlCheats;
	Button _btnToggleCheats;
	Text _lblConsoleLog;

	bool _developerModeActive;
	bool _cheatMenuOpen;

	void Start () {
		/* Panels */
		_pnlCanvas = this.gameObject.transform.GetChild (0).gameObject;
		_pnlCheats = _pnlCanvas.transform.Find ("cheatMenu").gameObject;
		/* Buttons */
		/* Button handlers */
		/* Lablels */
		_lblConsoleLog = _pnlCanvas.transform.Find ("consoleLog").gameObject.GetComponent <Text>();
		/* Do setup */
		_lblConsoleLog.text = "";

		if (CheatsAllowed) {
			_btnToggleCheats = _pnlCanvas.transform.Find ("toggleCheats").gameObject.GetComponent <Button> ();
			if (_btnToggleCheats != null) { _btnToggleCheats.onClick.AddListener (btnToggleCheatsHandler); }
			_cheatMenuOpen = false;
		} else {
			_pnlCanvas.transform.Find ("toggleCheats").gameObject.SetActive (false);
		}
		_pnlCheats.SetActive (false);
	}

	void Update () {
		
		if (PlayerPrefs.HasKey ("developerMode")) {
			if (PlayerPrefs.GetInt ("developerMode") == 1) { _developerModeActive = true; }
			else { _developerModeActive = false; }
		}

		if (_developerModeActive) {
			this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		} else {
			this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
		}
	}

	void btnToggleCheatsHandler() {
		/* Handler for btnToggleCheats */
		if (CheatsAllowed) {
			_cheatMenuOpen = !_cheatMenuOpen;
			_pnlCheats.SetActive (_cheatMenuOpen);
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
		string backLog = _lblConsoleLog.text;
		_lblConsoleLog.text = logString + "\n" + backLog;
	}
	#endregion

}
