using UnityEngine;

public class NewPlayerController : MonoBehaviour {

	public float MAX_ROTATE = 45f;
	public float ROTATION_DELTA = 90f;

	public float BASE_SPEED = 50f;
	public float MAX_SPEED_DELTA = 30f;

	public const float SPEED_OF_CHANGE_IN_SPEED = 0.75f;
	public const float INV_SPEED_OF_CHANGE = 1 - SPEED_OF_CHANGE_IN_SPEED;

	public const float SPEED_OF_CHANGE_IN_TILT = 0.75f;
	public const float INV_SPEED_OF_CHANGE_IN_TILT = 1 - SPEED_OF_CHANGE_IN_TILT;

	public float SPEED_LOSS_FACTOR = 1.5f;

	public float MAX_TRANSLATE = 30f;

	public float GRAVITY1 = 30f;
	public float GRAVITY2 = 20f;
	public float GRAVITY3 = 10f;

	public float GRAVITY_BOOST = 3f;
	public float BOOST = 1f;
	public float MAX_BOOST = 99f;

	public Vector3 gravityAdjust;
	public Vector3 controlAdjust;
	private float gravityBoost;
	public bool gravityWell;

	public float targetSpeed;
	public float currentSpeed;
	public float decel;

	private float currentRotation;

	public Transform ship;

	public float GravityBoost {
		get {
			return gravityBoost;
		}

		set {
			if (value < 0) {
				gravityBoost = 0f;
			}
			else if (value > MAX_BOOST) {
				gravityBoost = MAX_BOOST;
			}
			else gravityBoost = value;
		}
	}

	public float CurrentRotation {
		get {
			return currentRotation;
		}

		set {
			if (value > MAX_ROTATE) {
				currentRotation = MAX_ROTATE;
			}
			else if (value < -MAX_ROTATE) {
				currentRotation = -MAX_ROTATE;
			}
			else currentRotation = value;
		}
	}


	// Use this for initialization
	void Start() {
		gravityAdjust = Vector3.zero;
		controlAdjust = Vector3.zero;

		gravityBoost = 0f;
		gravityWell = false;

		currentSpeed = BASE_SPEED;
		targetSpeed = BASE_SPEED;
		decel = 0;

		currentRotation = 0;
	}

	// Update is called once per frame
	void Update() {

		//Get user Input
		float forward = Input.GetAxis("Vertical");
		float tilt = Input.GetAxis("Horizontal");

		if (!gravityWell && gravityBoost > 0) {
			decel -= SPEED_LOSS_FACTOR;
            gravityBoost -= decel;
		}

		targetSpeed = BASE_SPEED + (forward * MAX_SPEED_DELTA) + gravityBoost;


		//Add decceleration effect! TODO
		if (targetSpeed != currentSpeed) {
			currentSpeed = (SPEED_OF_CHANGE_IN_SPEED * currentSpeed) + (INV_SPEED_OF_CHANGE * targetSpeed);
		}


		//TODO TIME.DELTATIME!
		transform.Translate(transform.forward * currentSpeed);
		transform.Translate(transform.right * MAX_TRANSLATE * tilt);
		transform.Translate(gravityAdjust);


		//Rotate Ship
		float targetRotation = tilt * MAX_ROTATE;
		if (currentRotation != targetRotation) {
			currentRotation = (SPEED_OF_CHANGE_IN_TILT * currentRotation) + (INV_SPEED_OF_CHANGE_IN_TILT * targetRotation);
		}

		ship.rotation = Quaternion.Euler(0, 0, -currentRotation);

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Planet-Grav3") {
			gravityWell = true;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Planet-Grav1") {
			Vector3 direction = other.transform.position - transform.position;
			gravityAdjust = direction.normalized * GRAVITY1;
			if (gravityBoost < GRAVITY1 * GRAVITY_BOOST) gravityBoost += 3 * BOOST;
		}
		else if (other.tag == "Planet-Grav2") {
			Vector3 direction = other.transform.position - transform.position;
			gravityAdjust = direction.normalized * GRAVITY2;
			if (gravityBoost < GRAVITY1 * GRAVITY_BOOST) gravityBoost += 2 * BOOST;
		}
		else if (other.tag == "Planet-Grav3") {
			Vector3 direction = other.transform.position - transform.position;
			gravityAdjust = direction.normalized * GRAVITY3;
			if (gravityBoost < GRAVITY1 * GRAVITY_BOOST) gravityBoost += 1 * BOOST;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Planet-Grav3") {
			gravityAdjust = Vector3.zero;
			gravityWell = false;
			decel = 0;
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Planet") {
			Debug.Break(); //TODO
		}
	}

}
