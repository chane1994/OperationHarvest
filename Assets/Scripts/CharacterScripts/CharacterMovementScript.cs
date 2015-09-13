using UnityEngine;
using System.Collections;


public class CharacterMovementScript : MonoBehaviour {
	public Animator charAnimator; //Access to Agent Controller, the animation controller of the model
	public GameObject player;// Will be set to private
	public Camera camera; // Will be set to private
	public float movingSpeed, jumpingSpeed;
	bool _moving, _ground, _running;
	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		player = this.gameObject;
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
	}
	
	// Update is called once per frame
	void Update () {
		//Handles the jump of the character
		if (Input.GetButton ("Horizontal")) {
			
			Debug.Log ("Moving");
			Debug.Log (Input.GetAxis("Horizontal"));
			_moving = true;
			AnimationHandler (0);

			//_running = Input.GetKey(KeyCode.LeftShift);
			//charAnimator.SetBool("running",_running);

			if (Input.GetAxis ("Horizontal") > 0) {
				player.transform.eulerAngles = new Vector3 (0, -270, 0);
			} else {
				player.transform.eulerAngles = new Vector3 (0, -90, 0);
			}
			player.transform.Translate (Vector3.left * movingSpeed * -Input.GetAxis ("Horizontal"), camera.transform);

			
			//player.transform.Translate(Vector3.left);
			
		} else {
			_moving = false;
			Debug.Log ("Not Moving");
			AnimationHandler (0);
		}
		if (Input.GetButton("Jump"))
		{
			if(_ground)
			{
				Debug.Log ("Jumping");
				_ground = false;
				AnimationHandler(1);
			rigidBody.AddForce(Vector3.up * jumpingSpeed);
				AnimationHandler(2);
			}
		}
		if (Input.GetButton ("Fire1")) {
			Debug.Log("MeleeAttack");
			charAnimator.SetTrigger ("canMelee");
		}

		//Handles's Basic movement of the Character

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
			Debug.Log ("Landed on the ground");
		}
	}
	void OnCollisionStay(Collision col)
	{
		if (col.gameObject.tag == "Floor") {
			_ground = true;
			AnimationHandler(1);
			Debug.Log ("Still on the ground");
		}
	}
	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Floor") {
			_ground = false;
			AnimationHandler(1);
			Debug.Log(" Jumped into the air");
		}
	}
	void AnimationHandler(int aniState )
	{
		if (aniState == 0)
		charAnimator.SetBool ("moving", _moving);
		if (aniState == 1)
		charAnimator.SetBool ("ground", _ground);


	}
}