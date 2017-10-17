using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DeveloperMode : MonoBehaviour {
	
	[Header("Options")]
	public bool CheatsAllowed;
	[Header("Scripting vars")]
	public Player Player;            // Reference to the player object, should be set in designer
	
	public string Output = "";
	public string Stack = "";

	private GameObject _pnlCanvas;
	private GameObject _pnlCheats;
	private Button _btnToggleCheats;
	private Button _btnMAdd1000;
	private Button _btnMAdd100000;
	private Button _btnSAdd10;
	private Button _btnSAdd1000;
	private Button _btnHpAdd10;
	private Button _btnHpAdd100;
	private Text _lblConsoleLog;

	private bool _developerModeActive;
	private bool _cheatMenuOpen;

	void Start () {
		/* Panels */
		_pnlCanvas = this.gameObject.transform.GetChild (0).gameObject;
		_pnlCheats = _pnlCanvas.transform.Find ("cheatMenu").gameObject;
		/* Buttons */
		_btnMAdd1000 = _pnlCheats.transform.Find("btnMAdd1000").gameObject.GetComponent<Button>();
		_btnMAdd100000 = _pnlCheats.transform.Find("btnMAdd100000").gameObject.GetComponent<Button>();
		_btnSAdd10 = _pnlCheats.transform.Find("btnSAdd10").gameObject.GetComponent<Button>();
		_btnSAdd1000 = _pnlCheats.transform.Find("btnSAdd1000").gameObject.GetComponent<Button>();
		_btnHpAdd10 = _pnlCheats.transform.Find("btnHPAdd10").gameObject.GetComponent<Button>();
		_btnHpAdd100 = _pnlCheats.transform.Find("btnHPAdd100").gameObject.GetComponent<Button>();
		/* Button handlers */
		if (_btnMAdd1000 != null) { _btnMAdd1000.onClick.AddListener(_btnMAdd1000Handler); }
		if (_btnMAdd100000 != null) { _btnMAdd100000.onClick.AddListener(_btnMAdd100000Handler); }
		if (_btnSAdd10 != null) { _btnSAdd10.onClick.AddListener(_btnSAdd10Handler); }
		if (_btnSAdd1000 != null) { _btnSAdd1000.onClick.AddListener(_btnSAdd1000Handler); }
		if (_btnHpAdd10 != null) { _btnHpAdd10.onClick.AddListener(_btnHpAdd10Handler); }
		if (_btnHpAdd100 != null) { _btnHpAdd100.onClick.AddListener(_btnHpAdd100Handler); }
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

	void _btnMAdd1000Handler() {
		Player.MoneyAdd(1000);
	}
	
	void _btnMAdd100000Handler() {
		Player.MoneyAdd(100000);
	}
	
	void _btnSAdd10Handler() {
		Player.ScoreAdd(10);
	}
	
	void _btnSAdd1000Handler() {
		Player.ScoreAdd(1000);
	}

	void _btnHpAdd10Handler() {
		Player.IncreaseHealth(10);
	}
	
	void _btnHpAdd100Handler() {
		Player.IncreaseHealth(100);
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
