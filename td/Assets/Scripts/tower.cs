using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour {

	[Header("Attributes")]
	public int towerPrice;           // The price of the tower, set this in the desiger (tower prefab)
	public float fireRate;           // How long the turret should use to reload, Set in designer (tower prefab)
	public float turnSpeed;          // How fast the turret should rotate. Set in designer (tower prefab)
	[Range(0, 10)]
	public float towerRange;         // How large the range of the tower is. this is the diameter. Set in designer (tower prefab)
	[Header("Materials")]
	public Material materialDanger;  // The material used when tower can't be placed, set in the designer (tower prefab)
	public Material materialSuccess; // The material used when the tower can be placed, or is selected, set in the designer (tower prefab)
	[Header("Scripting vars")]
	public player player;            // Reference to the player object, should be set when instantiating

	private GameObject placementIndicator;        // The placement indicator
	private Renderer placementIndicatorRenderer;  // The renderer of the placement indicator
	private Plane groundPlane;                    // Plane used for raycasting when placing tower
	private float groundYpoint = 0.525f;          // What Y-position the Tower should be placed on, this should be constant in every use-case.
	private bool towerPlaced;                     // Bool used to descide what to do this frame
	private bool colliding;                       // Set if the tower collides with any GameObject, used when placing, to see where it can be placed

	private Transform target;
	private float fireCountdown;

	void Start () {
		placementIndicator = transform.GetChild (0).gameObject;
		placementIndicatorRenderer = placementIndicator.GetComponent<Renderer> ();
		placementIndicator.transform.localScale = new Vector3 (towerRange*7, 0.00000001f, towerRange*7); 

		groundPlane = new Plane (Vector3.up, new Vector3(0f, groundYpoint, 0f));
	}

	void Update () {

		#region placeTower
		if (!towerPlaced) {
			if (Input.touchCount == 1 || Input.GetMouseButton (0)) {
				
				/* Activate indicator if not already */
				if (!placementIndicator.activeSelf) { placementIndicator.SetActive (true); }
				/* Change indicator-color based on placement */
				if (!colliding) { placementIndicatorRenderer.sharedMaterial = materialDanger; }
				else { placementIndicatorRenderer.sharedMaterial = materialSuccess; }
				/* Calculate new position */
				Ray touchRay = Camera.main.ScreenPointToRay (Input.mousePosition);
				float rayDistance;
				if (groundPlane.Raycast (touchRay, out rayDistance)) {
					transform.position = touchRay.GetPoint (rayDistance);
				}

			} else {
				
				/* User let go of the screen, decide if tower can be placed */
				if (!colliding) { Destroy (gameObject); }  // Skal kollidere for å være på et godkjent område
				else {
					towerPlaced = true;
					player.moneySubtract (towerPrice);
					placementIndicator.SetActive (false);
					placementIndicatorRenderer.sharedMaterial = materialSuccess;
					InvokeRepeating ("updateTarget", 0f, 0.5f);  // This starts the 
					gameObject.GetComponent <BoxCollider>().enabled = false;
				}

			}

			return;
		}
		#endregion

		// Stop rest of update if no target is aquired
		if (target == null) {
			return;
		}
		// Target lockon
		Vector3 direction = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		Vector3 rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);

		if (fireCountdown <= 0f) {
			// FAIAAAAA
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;


	}

	void updateTarget() {
		/* Method that updates the currentTarget.
		 * The target will be set to the nearest in range */
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (var enemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= towerRange) {
			Debug.Log ("Target aquired");
			target = nearestEnemy.transform;
		} else {
			target = null;
		}
	}

	void OnTriggerEnter(Collider other) {
		colliding = true;
	}

	void OnTriggerStay(Collider other) {
		colliding = true;
	}

	void OnTriggerExit(Collider other) {
		colliding = false;
	}

	void OnDrawGizmosSelected() {
		/* Show gizmos in designer */
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, towerRange);
	}

}


