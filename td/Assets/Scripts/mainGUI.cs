using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class mainGUI : MonoBehaviour {

	GameObject pnlMenu;
	GameObject pnlSidebar;
	RectTransform pnlSidebarTransform;
	Button btnToggleSidebar;
	Button btnPauseGame;
	Button btnResumeGame;

	bool sidebarExpanded;
	float[] sidebarStates = new float[2] {0f, -202.4f};  // The x position of the sidebar expanded or collapsed

	bool menuActive;

	void Awake() {
		/* Panels */
		pnlMenu = transform.Find ("menu").gameObject;
		pnlSidebar = transform.Find ("sidebarWrapper").gameObject;
		pnlSidebarTransform = pnlSidebar.GetComponent <RectTransform> ();

		/* Buttons */
		btnToggleSidebar = pnlSidebar.transform.Find("toggleSidebar").gameObject.GetComponent <Button> ();
		btnPauseGame = pnlSidebar.transform.Find ("pauseGame").gameObject.GetComponent <Button> ();
		btnResumeGame = pnlMenu.transform.Find ("resumeGame").gameObject.GetComponent <Button> ();
		if (btnToggleSidebar != null) { btnToggleSidebar.onClick.AddListener (toggleSidebarHandler); }
		if (btnPauseGame != null) { btnPauseGame.onClick.AddListener (pauseGameHandler); }
		if (btnResumeGame != null) { btnResumeGame.onClick.AddListener (btnResumeGameHandler); }

		/* Set up initial states */
		updateSidebarPosandBtn ();
		pnlMenu.SetActive (false);
	}

	void toggleSidebarHandler() {
		/* handler for btnToggleSidebar */
		sidebarExpanded = !sidebarExpanded;
		updateSidebarPosandBtn ();
	}

	void pauseGameHandler() {
		/* handler for btnPauseGame */
		menuActive = true;
		pnlMenu.SetActive (menuActive);
		Time.timeScale = 0.0F;
		btnToggleSidebar.interactable = false;
		btnPauseGame.interactable = false;
	}

	void btnResumeGameHandler() {
		/* handler for btnResumeGame */
		menuActive = false;
		pnlMenu.SetActive (menuActive);
		Time.timeScale = 1.0F;
		btnToggleSidebar.interactable = true;
		btnPauseGame.interactable = true;
	}

	void updateSidebarPosandBtn() {
		/* update state of sidebar based on the expanded var */
		if (sidebarExpanded) {
			pnlSidebarTransform.localPosition = new Vector3 (sidebarStates [1], 0f, 0f);
			btnToggleSidebar.transform.GetComponent <RectTransform> ().localScale = new Vector3 (-1, 1, 1);
		} else {
			pnlSidebarTransform.localPosition = new Vector3 (sidebarStates [0], 0f, 0f);
			btnToggleSidebar.transform.GetComponent <RectTransform> ().localScale = new Vector3 (1, 1, 1);
		}
	}

}
