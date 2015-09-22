using UnityEngine;
using System.Collections;


public class CharacterMovementScript : MonoBehaviour {
	public Animator charAnimator; //Access to Agent Controller, the animation controller of the model
	public GameObject player;// Will be set to private
	public Camera camera; // Will be set to private
	public float movingSpeed, jumpingSpeed;
	bool _moving, _ground, _running, _climbing;
	Rigidbody rigidBody;
	int currentWeapon;
	public bool paused;


	// Use this for initialization
	void Start () {
		player = this.gameObject;
		paused = false;
		rigidBody = player.GetComponent<Rigidbody> ();
		_moving = false;
		_ground = true;
		_moving = true;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		player.transform.eulerAngles = new Vector3 (0, 180, 0);// This means that the player will start facing left
		charAnimator = player.GetComponent<Animator> ();
		charAnimator.SetBool ("moving", _moving); // Means that the character is not movin, genrally in the idle 
		charAnimator.SetBool ("ground", _ground);// Character is on the ground
		charAnimator.SetBool ("running", _running);
		charAnimator.SetBool ("climbing", _climbing);
	}
	
	// Update is called once per frame
	void Update () {
		//Handles's Basic movement of the Character

		if (Input.GetButton ("Menu")) {
			paused = !paused;
		}
		if (paused) {
			GameObject x = GameObject.FindWithTag("Menu");
			if(!x.activeSelf)
			{
				x.SetActive(paused);
			}
		}
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
			AnimationHandler (0);



			if (Input.GetAxis ("Horizontal") > 0) {
				player.transform.eulerAngles = new Vector3 (0, -270, 0);
			} else {
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
		if (Input.GetButton ("Fire1")) {
			if (currentWeapon == 0)
			{
			Debug.Log("MeleeAttack");
			charAnimator.SetTrigger ("canAttack");
				charAnimator.SetInteger("currentWeapon",currentWeapon);
			}
			if (currentWeapon == 1)
			{
				Debug.Log("GrenadeAttack");
				charAnimator.SetTrigger ("canAttack");
				charAnimator.SetInteger("currentWeapon",currentWeapon);
			}
			if (currentWeapon == 2)
			{
				Debug.Log("MeleeAttack");
				charAnimator.SetTrigger ("canAttack");
				charAnimator.SetInteger("currentWeapon",currentWeapon);
			}
		}


}
/// <summary>
	/// Handles all the change of parameters for the animator controller
	/// </summary>
	/// // used for determing what the animation that needs to be played is.
	/// <summary>
	///  idle/walking = 0;
	/// jump= 1;
	/// Meele Attack = 2;
	/// </summary>
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
}