﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHandler : MonoBehaviour {

	public float PanSpeed = 20f;
	public float ZoomSpeedTouch = 0.1f;
	public float ZoomSpeedMouse = .5f;

	public static readonly float[] BoundsX = new float[]{-10f, 10f};
	public static readonly float[] BoundsY = new float[]{-5f, 10f};
	public static readonly float[] BoundsZ = new float[]{-8f, 8f};
	public static readonly float[] ZoomBounds = new float[]{1f, 5f};

	private Camera cam;

	private bool panActive;
	private Vector3 lastPanPosition;
	private int panFingerId; // Touch mode only

	private bool zoomActive;
	private Vector2[] lastZoomPositions; // Touch mode only

	void Awake() {
		cam = GetComponent<Camera>();
	}

	void Update() {
		// If there's an open menu, or the clicker is being pressed, ignore the touch.
		/*
		if (GameManager.Instance.MenuManager.HasOpenMenu || GameManager.Instance.BitSpawnManager.IsSpawningBits) {
			return;
		}*/

		if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer) {
			HandleTouch();
		} else {
			HandleMouse();
		}
		//HandleTouch ();
	}

	void HandleTouch() {
		switch(Input.touchCount) {

		case 1: // Panning
			zoomActive = false;

			// If the touch began, capture its position and its finger ID.
			// Otherwise, if the finger ID of the touch doesn't match, skip it.
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				lastPanPosition = touch.position;
				panFingerId = touch.fingerId;
				panActive = true;
			} else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved) {
				PanCamera(touch.position);
			}
			break;

		case 2: // Zooming
			panActive = false;

			Vector2[] newPositions = new Vector2[]{Input.GetTouch(0).position, Input.GetTouch(1).position};
			if (!zoomActive) {
				lastZoomPositions = newPositions;
				zoomActive = true;
			} else {
				// Zoom based on the distance between the new positions compared to the 
				// distance between the previous positions.
				float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
				float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
				float offset = newDistance - oldDistance;

				ZoomCamera(offset, ZoomSpeedTouch);

				lastZoomPositions = newPositions;
			}
			break;

		default:
			panActive = false;
			zoomActive = false;
			break;
		}
	}

	void HandleMouse() {
		// On mouse down, capture it's position.
		// On mouse up, disable panning.
		// If there is no mouse being pressed, do nothing.
		if (Input.GetMouseButtonDown(0)) {
			panActive = true;
			lastPanPosition = Input.mousePosition;
		} else if (Input.GetMouseButtonUp(0)) {
			panActive = false;
		} else if (Input.GetMouseButton(0)) {
			PanCamera(Input.mousePosition);
		}

		// Check for scrolling to zoom the camera
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		zoomActive = true;
		ZoomCamera(scroll, ZoomSpeedMouse);
		zoomActive = false;
	}

	void ZoomCamera(float offset, float speed) {
		if (!zoomActive || offset == 0) {
			return;
		}

		cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
	}

	void PanCamera(Vector3 newPanPosition) {
		if (!panActive) {
			return;
		}

		// Translate the camera position based on the new input position
		Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
		Vector3 move = new Vector3(offset.x * PanSpeed, offset.y * PanSpeed, 0f);
		transform.Translate(move, Space.World);  
		ClampToBounds();

		lastPanPosition = newPanPosition;
	}

	void ClampToBounds() {
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
		pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);

		transform.position = pos;
	}

}
