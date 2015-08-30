using UnityEngine;
using System.Collections;


public class CharacterMovementScript : MonoBehaviour {
	public Animator charAnimator; //Access to Agent Controller, the animation controller of the model
	public GameObject player;
	public Camera camera;
	public float movingSpeed;
	bool _moving;
	// Use this for initialization
	void Start () {
		player = this.gameObject;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		player.transform.eulerAngles = new Vector3 (0, 0, 0);
		charAnimator = player.GetComponent<Animator> ();
		charAnimator.SetBool ("moving", false); // Means that the character is not movin, genrally in the idle 
	}
	
	// Update is called once per frame
	void Update () {
		//Handles's Basic movement of the Character
		if (Input.GetButton ("Horizontal")) {

			Debug.Log ("Moving");
			Debug.Log (Input.GetAxis("Horizontal"));
			_moving = true;
			AnimationHandler ();

			if (Input.GetAxis ("Horizontal") > 0) {
				player.transform.eulerAngles = new Vector3 (0, 180, 0);
			} else {
				player.transform.eulerAngles = new Vector3 (0, 0, 0);
			}
			player.transform.Translate (Vector3.left * movingSpeed * -Input.GetAxis ("Horizontal"), camera.transform);
				
			
			//player.transform.Translate(Vector3.left);
		
		} else {
			_moving = false;
			Debug.Log ("Not Moving");
			AnimationHandler ();
		}
}
/// <summary>
	/// Handles all the change of parameters for the animator controller
	/// </summary>
	void AnimationHandler()
	{
		charAnimator.SetBool ("moving", _moving);
	}
}