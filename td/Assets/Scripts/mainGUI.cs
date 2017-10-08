using System;
using UnityEngine.UI;
using UnityEngine;

public class MainGui : MonoBehaviour {
	private GameObject _pnlMenu;
	private GameObject _pnlSidebar;
	private GameObject _pnlSettings;
	private GameObject _pnlGameOver;
	private RectTransform _pnlSidebarTransform; 
	private Button _btnToggleSidebar;
	private Button _btnPauseGame;
	private Button _btnResumeGame;
	private Button _btnExitGame;
	private Button _btnSettings;
	private Button _btnSettingsDiscard;
	private Button _btnSettingsSave;
	private Button _btnGoRetry;
	private Button _btnGoMenu;
	private Text _txtGoScore;
	private Text _txtGoHighScore;
	private Text _txtGoNewHighScore;

	private bool _sidebarExpanded;
	private readonly float[] _sidebarStates = new float[2] {0f, -202.4f};  // The x position of the sidebar expanded or collapsed

	private bool _menuActive;

	private void Awake() {
		/* Panels */
		_pnlMenu = transform.Find ("menu").gameObject;
		_pnlSidebar = transform.Find ("sidebarWrapper").gameObject;
		_pnlSettings = transform.Find ("settings").gameObject;
		_pnlGameOver = transform.Find("GameOver").gameObject;
		_pnlSidebarTransform = _pnlSidebar.GetComponent <RectTransform> ();

		/* Buttons */
		_btnToggleSidebar = _pnlSidebar.transform.Find("toggleSidebar").gameObject.GetComponent <Button> ();
		_btnPauseGame = _pnlSidebar.transform.Find ("pauseGame").gameObject.GetComponent <Button> ();
		_btnResumeGame = _pnlMenu.transform.Find ("resumeGame").gameObject.GetComponent <Button> ();
		_btnExitGame = _pnlMenu.transform.Find ("exitGame").gameObject.GetComponent <Button> ();
		_btnSettings = _pnlMenu.transform.Find ("settings").gameObject.GetComponent <Button> ();
		_btnSettingsDiscard = _pnlSettings.transform.Find ("discardChanges").gameObject.GetComponent <Button> ();
		_btnSettingsSave = _pnlSettings.transform.Find ("saveChanges").gameObject.GetComponent <Button> ();
		_btnGoMenu = _pnlGameOver.transform.Find ("menu").gameObject.GetComponent <Button> ();
		_btnGoRetry = _pnlGameOver.transform.Find ("restart").gameObject.GetComponent <Button> ();
		if (_btnToggleSidebar != null) { _btnToggleSidebar.onClick.AddListener (toggleSidebarHandler); }
		if (_btnPauseGame != null) { _btnPauseGame.onClick.AddListener (pauseGameHandler); }
		if (_btnResumeGame != null) { _btnResumeGame.onClick.AddListener (btnResumeGameHandler); }
		if (_btnExitGame != null) { _btnExitGame.onClick.AddListener (btnExitGameHandler); }
		if (_btnSettings != null) { _btnSettings.onClick.AddListener (btnSettingsHandler); }
		if (_btnSettingsDiscard != null) { _btnSettingsDiscard.onClick.AddListener (btnSettingsDiscardHandler); }
		if (_btnSettingsSave != null) { _btnSettingsSave.onClick.AddListener (btnSettingsSaveHandler); }
		if (_btnGoMenu != null) { _btnGoMenu.onClick.AddListener (btnGoMenuHandler); }
		if (_btnGoRetry != null) { _btnGoRetry.onClick.AddListener (btnGoRetryHandler); }
		
		/* Text */
		_txtGoScore = _pnlGameOver.transform.Find("score").gameObject.GetComponent<Text>();
		_txtGoHighScore = _pnlGameOver.transform.Find("highScore").gameObject.GetComponent<Text>();
		_txtGoNewHighScore = _pnlGameOver.transform.Find("newHighscore").gameObject.GetComponent<Text>();

		/* Set up initial states */
		UpdateSidebarPosandBtn ();
		_pnlMenu.SetActive (false);
		_pnlSettings.SetActive (false);
		_pnlGameOver.SetActive(false);
	}

	private void toggleSidebarHandler() {
		/* handler for btnToggleSidebar */
		_sidebarExpanded = !_sidebarExpanded;
		UpdateSidebarPosandBtn ();
	}

	private void pauseGameHandler() {
		/* handler for btnPauseGame */
		_menuActive = true;
		_pnlMenu.SetActive (_menuActive);
		Time.timeScale = 0.0F;
		_btnToggleSidebar.interactable = false;
		_btnPauseGame.interactable = false;
	}

	private void btnResumeGameHandler() {
		/* handler for btnResumeGame */
		_menuActive = false;
		_pnlMenu.SetActive (_menuActive);
		Time.timeScale = 1.0F;
		_btnToggleSidebar.interactable = true;
		_btnPauseGame.interactable = true;
	}

	private void btnExitGameHandler() {
		/* handler for btnExitGame */
		Application.Quit ();
	}

	private void btnSettingsHandler() {
		/* handler for btnSettings */
		_pnlMenu.SetActive (false);
		_pnlSettings.SetActive (true);

		if (PlayerPrefs.HasKey ("developerMode")) {
			_pnlSettings.transform.Find ("developerEnabled").gameObject.GetComponent <Toggle> ().isOn = IntToBool(PlayerPrefs.GetInt ("developerMode"));
		}
	}

	private void btnSettingsSaveHandler() {
		/* handler for btnSaveSettings */
		_pnlMenu.SetActive (true);
		_pnlSettings.SetActive (false);

		PlayerPrefs.SetInt ("developerMode", Convert.ToInt32(_pnlSettings.transform.Find ("developerEnabled").gameObject.GetComponent <Toggle>().isOn));
	}

	private void btnSettingsDiscardHandler() {
		/* handler for btnSettingsDiscard */
		_pnlMenu.SetActive (true);
		_pnlSettings.SetActive (false);
	}

	private void UpdateSidebarPosandBtn() {
		/* update state of sidebar based on the expanded var */
		if (_sidebarExpanded) {
			_pnlSidebarTransform.localPosition = new Vector3 (_sidebarStates [1], 0f, 0f);
			_btnToggleSidebar.transform.GetComponent <RectTransform> ().localScale = new Vector3 (-1, 1, 1);
		} else {
			_pnlSidebarTransform.localPosition = new Vector3 (_sidebarStates [0], 0f, 0f);
			_btnToggleSidebar.transform.GetComponent <RectTransform> ().localScale = new Vector3 (1, 1, 1);
		}
	}

	private void btnGoMenuHandler() {
		/* Handler for btnGoMenu */
	}

	private void btnGoRetryHandler() {
		/* Handler for btnGoRetry */
	}

	private bool IntToBool(int input) {
		/* Converts int to boolean */
		if (input >= 1) {
			return true;
		} else {
			return false;
		}
	}

	public void GameOverScreen(int score) {
		/* Show game over screen */
		bool newHighscore = false;
		int highScore = 0;
		
		if (PlayerPrefs.HasKey("highscore")) {
			highScore = PlayerPrefs.GetInt("highscore");
			if (score > highScore) {
				newHighscore = true;
			}
		}
		
		if (_sidebarExpanded) { toggleSidebarHandler(); }
		
		/* Pause game */
		Time.timeScale = 0.0F;
		/* Activate panel */
		_pnlGameOver.SetActive(true);
		/* Set text, score */
		_txtGoScore.text = "Score: " + score.ToString();
		/* set text, highscore */
		_txtGoHighScore.text = "Highscore: " + highScore.ToString();
		/* set newHicgscore */
		_txtGoNewHighScore.gameObject.SetActive(newHighscore);
		/* Disable other onScreenButtons */
		_btnToggleSidebar.interactable = false;
		_btnPauseGame.interactable = false;
	}

}
