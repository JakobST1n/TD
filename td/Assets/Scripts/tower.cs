using UnityEngine;

public class Tower : MonoBehaviour {

	[Header("Attributes")]
	public int TowerPrice;           // The price of the tower, set this in the desiger (tower prefab)
	public float FireRate;           // How long the turret should use to reload, Set in designer (tower prefab)
	public float TurnSpeed;          // How fast the turret should rotate. Set in designer (tower prefab)
	[Range(0, 10)]
	public float TowerRange;         // How large the range of the tower is. this is the diameter. Set in designer (tower prefab)
	public GameObject ProjectilePrefab;
	public Transform FirePoint;
	[Header("Materials")]
	public Material MaterialDanger;  // The material used when tower can't be placed, set in the designer (tower prefab)
	public Material MaterialSuccess; // The material used when the tower can be placed, or is selected, set in the designer (tower prefab)
	[Header("Scripting vars")]
	public Player Player;            // Reference to the player object, should be set when instantiating

	private GameObject _placementIndicator;        // The placement indicator
	private Renderer _placementIndicatorRenderer;  // The renderer of the placement indicator
	private Plane _groundPlane;                    // Plane used for raycasting when placing tower
	private float _groundYpoint = 0.525f;          // What Y-position the Tower should be placed on, this should be constant in every use-case.
	private bool _towerPlaced;                     // Bool used to descide what to do this frame
	private bool _colliding;                       // Set if the tower collides with any GameObject, used when placing, to see where it can be placed

	private Transform _target;
	private float _fireCountdown;

	void Start () {
		_placementIndicator = transform.GetChild (0).gameObject;
		_placementIndicatorRenderer = _placementIndicator.GetComponent<Renderer> ();
		_placementIndicator.transform.localScale = new Vector3 (TowerRange*7, 0.00000001f, TowerRange*7); 

		_groundPlane = new Plane (Vector3.up, new Vector3(0f, _groundYpoint, 0f));
	}

	void Update () {

		#region placeTower
		if (!_towerPlaced) {
			if (Input.touchCount == 1 || Input.GetMouseButton (0)) {
				
				/* Activate indicator if not already */
				if (!_placementIndicator.activeSelf) { _placementIndicator.SetActive (true); }
				/* Change indicator-color based on placement */
				if (!_colliding) { _placementIndicatorRenderer.sharedMaterial = MaterialDanger; }
				else { _placementIndicatorRenderer.sharedMaterial = MaterialSuccess; }
				/* Calculate new position */
				Ray touchRay = Camera.main.ScreenPointToRay (Input.mousePosition);
				float rayDistance;
				if (_groundPlane.Raycast (touchRay, out rayDistance)) {
					transform.position = touchRay.GetPoint (rayDistance);
				}

			} else {
				
				/* User let go of the screen, decide if tower can be placed */
				if (!_colliding) { Destroy (gameObject); }  // Skal kollidere for å være på et godkjent område
				else {
					_towerPlaced = true;
					Player.MoneySubtract (TowerPrice);
					_placementIndicator.SetActive (false);
					_placementIndicatorRenderer.sharedMaterial = MaterialSuccess;
					InvokeRepeating ("UpdateTarget", 0f, 0.5f);  // This starts the 
					gameObject.GetComponent <BoxCollider>().enabled = false;
				}

			}

			return;
		}
		#endregion

		// Stop rest of update if no target is aquired
		if (_target == null) {
			return;
		}
		// Target lockon
		Vector3 direction = _target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		Vector3 rotation = Quaternion.Lerp (transform.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);

		if (_fireCountdown <= 0f) {
			// FAIAAAAA
			Shoot();
			_fireCountdown = 1f / FireRate;
		}

		_fireCountdown -= Time.deltaTime;


	}

	void Shoot() {
		GameObject projectileGo = (GameObject) Instantiate (ProjectilePrefab, FirePoint.position, FirePoint.rotation);
		Projectile projectile = projectileGo.GetComponent <Projectile>();
		if (projectile != null) {
			projectile.Player = Player;
			projectile.Seek (_target);
		}
	}

	void UpdateTarget() {
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

		if (nearestEnemy != null && shortestDistance <= TowerRange) {
			_target = nearestEnemy.transform;
		} else {
			_target = null;
		}
	}

	void OnTriggerEnter(Collider other) {
		_colliding = true;
	}

	void OnTriggerStay(Collider other) {
		_colliding = true;
	}

	void OnTriggerExit(Collider other) {
		_colliding = false;
	}

	void OnDrawGizmosSelected() {
		/* Show gizmos in designer */
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, TowerRange);
	}

}


