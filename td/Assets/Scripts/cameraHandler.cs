using UnityEngine;

public class CameraHandler : MonoBehaviour {
	// TODO Fiks panning, ser idiotisk ut nå så jeg har satt panSpeed til 0. (I editoren)

	public float PanSpeed = 20f;
	public float ZoomSpeedTouch = 0.1f;
	public float ZoomSpeedMouse = .5f;

	public static readonly float[] BoundsX = new float[]{-10f, 10f};
	public static readonly float[] BoundsY = new float[]{-5f, 10f};
	public static readonly float[] BoundsZ = new float[]{-8f, 8f};
	public static readonly float[] ZoomBounds = new float[]{1f, 5f};
	[Header("Scripting vars")]
	public Player Player;            // Reference to the player object, should be set in designer

	private Camera _cam;

	private bool _panActive;
	private Vector3 _lastPanPosition;
	private int _panFingerId; // Touch mode only

	private bool _zoomActive;
	private Vector2[] _lastZoomPositions; // Touch mode only

	void Awake() {
		_cam = GetComponent<Camera>();
	}

	void Update() {
		if (Player.GameIsPaused()) { return; }
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
			_zoomActive = false;

			// If the touch began, capture its position and its finger ID.
			// Otherwise, if the finger ID of the touch doesn't match, skip it.
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				_lastPanPosition = touch.position;
				_panFingerId = touch.fingerId;
				_panActive = true;
			} else if (touch.fingerId == _panFingerId && touch.phase == TouchPhase.Moved) {
				PanCamera(touch.position);
			}
			break;

		case 2: // Zooming
			_panActive = false;

			Vector2[] newPositions = new Vector2[]{Input.GetTouch(0).position, Input.GetTouch(1).position};
			if (!_zoomActive) {
				_lastZoomPositions = newPositions;
				_zoomActive = true;
			} else {
				// Zoom based on the distance between the new positions compared to the 
				// distance between the previous positions.
				float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
				float oldDistance = Vector2.Distance(_lastZoomPositions[0], _lastZoomPositions[1]);
				float offset = newDistance - oldDistance;

				ZoomCamera(offset, ZoomSpeedTouch);

				_lastZoomPositions = newPositions;
			}
			break;

		default:
			_panActive = false;
			_zoomActive = false;
			break;
		}
	}

	void HandleMouse() {
		// On mouse down, capture it's position.
		// On mouse up, disable panning.
		// If there is no mouse being pressed, do nothing.
		if (Input.GetMouseButtonDown(0)) {
			_panActive = true;
			_lastPanPosition = Input.mousePosition;
		} else if (Input.GetMouseButtonUp(0)) {
			_panActive = false;
		} else if (Input.GetMouseButton(0)) {
			PanCamera(Input.mousePosition);
		}

		// Check for scrolling to zoom the camera
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		_zoomActive = true;
		ZoomCamera(scroll, ZoomSpeedMouse);
		_zoomActive = false;
	}

	void ZoomCamera(float offset, float speed) {
		if (!_zoomActive || offset == 0) {
			return;
		}

		_cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
	}

	void PanCamera(Vector3 newPanPosition) {
		if (!_panActive) {
			return;
		}

		// Translate the camera position based on the new input position
		Vector3 offset = _cam.ScreenToViewportPoint(_lastPanPosition - newPanPosition);
		Vector3 move = new Vector3(offset.x * PanSpeed, offset.y * PanSpeed, 0f);
		transform.Translate(move, Space.World);  
		ClampToBounds();

		_lastPanPosition = newPanPosition;
	}

	void ClampToBounds() {
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
		pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);

		transform.position = pos;
	}

}
