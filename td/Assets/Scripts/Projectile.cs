using UnityEngine;

public class Projectile : MonoBehaviour {

	public float Speed = 70f;
	public int PointsPerHit;
	[Header("Scripting vars")]
	public Player Player;            // Reference to the player object, should be set when instantiating
	private Transform _target;

	public void Seek(Transform target) {
		_target = target;
	}
		

	void Update () {

		if (_target == null) {
			Destroy (gameObject);
			return;
		}

		Vector3 direction = _target.position - transform.position;
		float distanceThisFrame = Speed * Time.deltaTime;

		if (direction.magnitude <= distanceThisFrame) {
			HitTarget ();
			return;
		}

		transform.Translate (direction.normalized * distanceThisFrame, Space.World);


	}

	void HitTarget() {
		Player.ScoreAdd (PointsPerHit);
		Destroy (_target.gameObject);
		Destroy (gameObject);
	}

}
