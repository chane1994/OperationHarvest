using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMovementScript : MonoBehaviour {
	public Animator charAnimator; //Access to Agent Controller, the animation controller of the model
	public GameObject player;// Will be set to private
	public Camera camera; // Will be set to private
	public Camera fpCamera;
	public float movingSpeed, jumpingSpeed;
	bool _moving, _ground, _running, _climbing;
	Rigidbody rigidBody;
	int currentWeapon;
	public bool paused;
	public Transform muzzleLocation;
	public GameObject currentBullet;
	public GameObject currentGrenade;
	Event shotEvent;
	public float health;
	bool direction;

	GameObject menu;


	bool aimBool;
	float fireRate; //Gives a set fire rate to attacks
	bool canfire; //Used to stop certain attacks from firing into the time is right (mainly used for shooting);
	// Use this for initialization
	void Start () {
		player = this.gameObject;
		paused = false;
		rigidBody = player.GetComponent<Rigidbody> ();
		_moving = false;
		_ground = true;
		_moving = true;
		fpCamera.enabled = false;
		camera = GameObject.Find ("Camera").GetComponent<Camera> ();
		canfire = false;	
				
		
		player.transform.eulerAngles = new Vector3 (0, 180, 0);// This means that the player will start facing left
		charAnimator = player.GetComponent<Animator> ();
		charAnimator.SetBool ("moving", _moving); // Means that the character is not movin, genrally in the idle 
		charAnimator.SetBool ("ground", _ground);// Character is on the ground
		charAnimator.SetBool ("running", _running);
		charAnimator.SetBool ("climbing", _climbing);
		currentWeapon = 2;
		health = 1;

		menu = GameObject.FindGameObjectWithTag ("Menu");
		menu.SetActive (false);
		//Health ();
	}
	void Health()
	{
		Image i = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
		//i.sprite.
	}
	void HandleMenu()
	{

		if (Input.GetButton ("Menu")) {
			paused = !paused;
			menu.SetActive(paused);
		}
	}
	void HandleMovement()
	{
		if (Input.GetButton ("Horizontal")) {
			
			//Debug.Log ("Moving");
			Debug.Log (Input.GetAxis("Horizontal"));
			_moving = true;
			if (Input.GetButton("Running"))
			{
				_running = true;
				charAnimator.SetBool("running",_running);
			}
			else
			{
				AnimationHandler (0);
				_running = false;
				charAnimator.SetBool("running",_running);
			}
			
			if (Input.GetAxis ("Horizontal") > 0) {
				direction = true;
				player.transform.eulerAngles = new Vector3 (0, -270, 0);
			} else {
				direction = false;
				player.transform.eulerAngles = new Vector3 (0, -90, 0);
			}
			if(_running)
			{
				player.transform.Translate (Vector3.left * movingSpeed * 5*-Input.GetAxis ("Horizontal"), camera.transform);
			}
			else
			{
				player.transform.Translate (Vector3.left * movingSpeed * -Input.GetAxis ("Horizontal"), camera.transform);
			}
			
			//player.transform.Translate(Vector3.left);
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		} else {
			_moving = false;
			_running = false;
			charAnimator.SetBool("running",_running);
			//Debug.Log ("Not Moving");
			AnimationHandler (0);
		}
		//Handles the jump of the character
		if (Input.GetButton("Jump"))
		{
			if (_ground)
			{
				//Debug.Log ("Jumping");
				_ground = false;
				AnimationHandler(1);
				rigidBody.AddForce(Vector3.up * jumpingSpeed);
				AnimationHandler(2);
			}
		}
	}
	void HandleAimMode()
	{
		//
	}
	// Update is called once per frame
	void Update () {
		//Handles's Basic movement of the Character
		fireRate += Time.deltaTime;

	// used for aiming mode
		if (Input.GetButton ("Aiming")) {
			aimBool = !aimBool;
		} 

		HandleMenu ();
		if (!aimBool) {
			fpCamera.enabled = false;
			camera.enabled = true;
			HandleMovement ();

		} else {
			fpCamera.enabled = true;
			camera.enabled = false;
			HandleAimMode();
		}


		//Handles Switching wapons
		if (Input.GetButton("switchWeapon")) {
			if (currentWeapon < 2)
			{
				currentWeapon++;
				Debug.Log("current weapon is"+ currentWeapon);
			}
			else
			{
				currentWeapon = 0;
			}
		}
		// Handles Firing weapons
		if (Input.GetButton ("Fire1")&&fireRate > 2.5f) {
		
			if (currentWeapon == 0)
			{
				Debug.Log("MeleeAttack");
				charAnimator.SetTrigger ("canAttack");
				charAnimator.SetInteger("currentWeapon",currentWeapon);
				fireRate = 0;
			}
			if (currentWeapon == 1)
			{
				Debug.Log("GrenadeAttack");
				charAnimator.SetTrigger ("canAttack");
				charAnimator.SetInteger("currentWeapon",currentWeapon);
				fireRate = 0;
			}
			if (currentWeapon == 2)
			{
				Debug.Log("PistolAttack");
				charAnimator.SetTrigger ("canAttack");
				charAnimator.SetInteger("currentWeapon",currentWeapon);
				canfire = true;
				StartCoroutine(Delay(1f));
			


			}
			fireRate = 0;
		}

}



	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Floor") {
			_ground = true;
			AnimationHandler(1);
			//Debug.Log ("Landed on the ground");
		}
		if (col.gameObject.tag == "Climable") {
			Debug.Log ("Try Climbing");
		}
	}

	/*void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Floor") {
			_ground = false;
			AnimationHandler(1);
			//Debug.Log(" Jumped into the air");
		}
	}*/
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Climable") {
			
			if (Input.GetButton ("Vertical")) {
				Debug.Log ("Try Climbing");
				
				player.transform.Translate (Vector3.up * movingSpeed * Input.GetAxis ("Vertical"), camera.transform);
				player.transform.eulerAngles = new Vector3 (0, 0, 0);
			}	
			this.rigidBody.useGravity = !Input.GetButton ("Vertical");
		}
	}
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Climable") {

			if (Input.GetButton ("Vertical")) {
				Debug.Log ("Try Climbing");

				player.transform.Translate (Vector3.up * movingSpeed * Input.GetAxis ("Vertical"), camera.transform);
				player.transform.eulerAngles = new Vector3 (0, 0, 0);
			}	
			this.rigidBody.useGravity = !Input.GetButton ("Vertical");
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Climable") {

			this.rigidBody.useGravity = true;
		}
	}
	void AnimationHandler(int aniState )
	{
        if (aniState == 0)
        {
            charAnimator.SetBool("moving", _moving);
        }
        if (aniState == 1)
        {
            charAnimator.SetBool("ground", _ground);
        }
	}
	public bool Direction
	{
		get {return direction;}
	}
	IEnumerator Delay(float x)
	{
		Debug.Log ("I waited");
	
		yield return new WaitForSeconds(x);
		Instantiate(currentBullet,muzzleLocation.position,muzzleLocation.rotation);
		canfire = false;

	}
}