using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {

	public float moveSpeed = 15;
	public float gravity = -70;
	public float jumpVelocity = 16;
	public float jumpVelocity = 550;

	Light jetLight;
	Vector3 velocity;
	float velocityXSmoothing;
	float accelerationTimeAirborne = .35f;
	float accelerationTimeGrounded = .16f;
	Vector3 startPosition;

	int facingDirection;

	Controller2D playercontroller;

	float dashSpeed = 40f;

	ParticleSystem jetpackParticles;
	GameObject jetpackLight;

	GameObject jetpackFlare;

	jetpackTrailer justInstantiated;


	// Use this for initialization
	void Start () {


		playercontroller = GetComponent<Controller2D> ();
		startPosition = this.transform.localPosition;

		facingDirection = 1;

		jetpackParticles = GetComponentInChildren<ParticleSystem>();
		jetpackLight = GameObject.FindGameObjectsWithTag("jetpackLight")[0];
		jetLight = jetpackLight.GetComponent<Light>();

		jetLight.intensity = 0;

		jetpackFlare = GameObject.FindGameObjectWithTag ("jetpackFlare");

	}

	// Update is called once per frame
	void Update () {

		if(playercontroller.collisions.above || playercontroller.collisions.below) {

			velocity.y = 0;

		}

		if (Input.GetKeyDown(KeyCode.Space) && playercontroller.collisions.below) {
			velocity.y = jumpVelocity;
		}

		if(Input.GetKey(KeyCode.W)) {

			velocity.y = (velocity.y<-10)?-10:velocity.y;
			velocity.y += 2;
			if (velocity.y > 16) { velocity.y = 16; }
			jetpackParticles.emissionRate = 500;
			jetLight.intensity = Random.Range(1f,1.5f);
			jetpackFlare.SetActive(true);
			float xScale = Random.Range (1f, 2f);
			float horScale = Random.Range (.2f, .5f);
			jetpackFlare.transform.localScale = (new Vector3(horScale, xScale, jetpackFlare.transform.localScale.z));

		} else {
			jetpackParticles.emissionRate = 0;
			jetLight.intensity = 0;
			jetpackFlare.SetActive(false);
		}

		if(Input.GetKeyDown (KeyCode.W)) {
			GameObject newTrail = Instantiate(GameObject.FindGameObjectWithTag("rocketTrail"));
			var theScript = newTrail.GetComponent<jetpackTrailer>();
			theScript.toDestruct = true;
			theScript.toEnable = true;
			justInstantiated = theScript;

		} else if (Input.GetKeyUp (KeyCode.W)) {

			justInstantiated.Moving = false;


		}



		if(Input.GetKeyDown(KeyCode.Escape)) {
			playercontroller.setPosition(startPosition);
		}

		if(Input.GetKeyDown(KeyCode.S)) {
			velocity.y = -20;
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			velocity.x = 0;
		}

		if(playercontroller.collisions.left || playercontroller.collisions.right) {

			velocity.x = 0f;
			velocityXSmoothing = 0f;
		}


		Vector2 input = new Vector2(Mathf.Round(Input.GetAxisRaw("Horizontal")), Input.GetAxisRaw("Vertical"));
		float inputDirection = Input.GetAxisRaw("Horizontal");
		if (inputDirection > 0) {facingDirection = 1;} else if (inputDirection < 0) {facingDirection = -1;}




		//Move the player
		transform.localScale = new Vector3 (-facingDirection, transform.localScale.y, transform.localScale.z);

		float targetVelocityX = input.x * moveSpeed;

		print (velocityXSmoothing);

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, playercontroller.collisions.below?accelerationTimeGrounded:accelerationTimeAirborne);
		//velocity.x = Mathf.Lerp (velocity.x, targetVelocityX, playercontroller.collisions.below?0.2f:0.08f);


		velocity.y += gravity*Time.deltaTime;




		playercontroller.Move(velocity*Time.deltaTime);




	}

	public float getVelx() {
		return velocity.x;
	}
}
