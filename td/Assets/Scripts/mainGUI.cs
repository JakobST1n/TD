using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainGui : MonoBehaviour {

	GameObject _pnlMenu;
	GameObject _pnlSidebar;
	GameObject _pnlSettings;
	RectTransform _pnlSidebarTransform;
	Button _btnToggleSidebar;
	Button _btnPauseGame;
	Button _btnResumeGame;
	Button _btnExitGame;
	Button _btnSettings;
	Button _btnSettingsDiscard;
	Button _btnSettingsSave;

	bool _sidebarExpanded;
	float[] _sidebarStates = new float[2] {0f, -202.4f};  // The x position of the sidebar expanded or collapsed

	bool _menuActive;

	void Awake() {
		/* Panels */
		_pnlMenu = transform.Find ("menu").gameObject;
		_pnlSidebar = transform.Find ("sidebarWrapper").gameObject;
		_pnlSettings = transform.Find ("settings").gameObject;
		_pnlSidebarTransform = _pnlSidebar.GetComponent <RectTransform> ();

		/* Buttons */
		_btnToggleSidebar = _pnlSidebar.transform.Find("toggleSidebar").gameObject.GetComponent <Button> ();
		_btnPauseGame = _pnlSidebar.transform.Find ("pauseGame").gameObject.GetComponent <Button> ();
		_btnResumeGame = _pnlMenu.transform.Find ("resumeGame").gameObject.GetComponent <Button> ();
		_btnExitGame = _pnlMenu.transform.Find ("exitGame").gameObject.GetComponent <Button> ();
		_btnSettings = _pnlMenu.transform.Find ("settings").gameObject.GetComponent <Button> ();
		_btnSettingsDiscard = _pnlSettings.transform.Find ("discardChanges").gameObject.GetComponent <Button> ();
		_btnSettingsSave = _pnlSettings.transform.Find ("saveChanges").gameObject.GetComponent <Button> ();
		if (_btnToggleSidebar != null) { _btnToggleSidebar.onClick.AddListener (toggleSidebarHandler); }
		if (_btnPauseGame != null) { _btnPauseGame.onClick.AddListener (pauseGameHandler); }
		if (_btnResumeGame != null) { _btnResumeGame.onClick.AddListener (btnResumeGameHandler); }
		if (_btnExitGame != null) { _btnExitGame.onClick.AddListener (btnExitGameHandler); }
		if (_btnSettings != null) { _btnSettings.onClick.AddListener (btnSettingsHandler); }
		if (_btnSettingsDiscard != null) { _btnSettingsDiscard.onClick.AddListener (btnSettingsDiscardHandler); }
		if (_btnSettingsSave != null) { _btnSettingsSave.onClick.AddListener (btnSettingsSaveHandler); }

		/* Set up initial states */
		UpdateSidebarPosandBtn ();
		_pnlMenu.SetActive (false);
		_pnlSettings.SetActive (false);
	}

	void toggleSidebarHandler() {
		/* handler for btnToggleSidebar */
		_sidebarExpanded = !_sidebarExpanded;
		UpdateSidebarPosandBtn ();
	}

	void pauseGameHandler() {
		/* handler for btnPauseGame */
		_menuActive = true;
		_pnlMenu.SetActive (_menuActive);
		Time.timeScale = 0.0F;
		_btnToggleSidebar.interactable = false;
		_btnPauseGame.interactable = false;
	}

	void btnResumeGameHandler() {
		/* handler for btnResumeGame */
		_menuActive = false;
		_pnlMenu.SetActive (_menuActive);
		Time.timeScale = 1.0F;
		_btnToggleSidebar.interactable = true;
		_btnPauseGame.interactable = true;
	}

	void btnExitGameHandler() {
		/* handler for btnExitGame */
		Application.Quit ();
	}

	void btnSettingsHandler() {
		/* handler for btnSettings */
		_pnlMenu.SetActive (false);
		_pnlSettings.SetActive (true);

		if (PlayerPrefs.HasKey ("developerMode")) {
			_pnlSettings.transform.Find ("developerEnabled").gameObject.GetComponent <Toggle> ().isOn = IntToBool(PlayerPrefs.GetInt ("developerMode"));
		}
	}

	void btnSettingsSaveHandler() {
		/* handler for btnSaveSettings */
		_pnlMenu.SetActive (true);
		_pnlSettings.SetActive (false);

		PlayerPrefs.SetInt ("developerMode", Convert.ToInt32(_pnlSettings.transform.Find ("developerEnabled").gameObject.GetComponent <Toggle>().isOn));
	}

	void btnSettingsDiscardHandler() {
		/* handler for btnSettingsDiscard */
		_pnlMenu.SetActive (true);
		_pnlSettings.SetActive (false);
	}

	void UpdateSidebarPosandBtn() {
		/* update state of sidebar based on the expanded var */
		if (_sidebarExpanded) {
			_pnlSidebarTransform.localPosition = new Vector3 (_sidebarStates [1], 0f, 0f);
			_btnToggleSidebar.transform.GetComponent <RectTransform> ().localScale = new Vector3 (-1, 1, 1);
		} else {
			_pnlSidebarTransform.localPosition = new Vector3 (_sidebarStates [0], 0f, 0f);
			_btnToggleSidebar.transform.GetComponent <RectTransform> ().localScale = new Vector3 (1, 1, 1);
		}
	}

	bool IntToBool(int input) {
		/* Converts int to boolean */
		if (input >= 1) {
			return true;
		} else {
			return false;
		}
	}

}
