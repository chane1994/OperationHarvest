﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMovementScript : MonoBehaviour {
	public Animator charAnimator; //Access to Agent Controller, the animation controller of the model
	public GameObject player;// Will be set to private
	public Camera camera; // Will be set to private
	public Camera fpCamera;
	public float movingSpeed, jumpingSpeed;
	bool _moving, _ground, _running, _climbing, _crouch;
	Rigidbody rigidBody;
	int currentWeapon;
	public bool paused;
	public Transform muzzleLocation;
	public GameObject currentBullet;
	public GameObject currentGrenade;
	Event shotEvent;
	float health;
	bool direction;

	GameObject menu;
    Image healthBar;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

	bool aimBool; // Determines whether the game is in aiming mode
	float fireRate; //Gives a set fire rate to attacks
	bool canfire; //Used to stop certain attacks from firing into the time is right (mainly used for shooting);
	// Use this for initialization
	void Start () {
		player = this.gameObject;
		paused = false;
		rigidBody = player.GetComponent<Rigidbody> ();
		_moving = false;
		_ground = true;
        _crouch = false;
		fpCamera.enabled = false;
		camera = GameObject.Find ("Camera").GetComponent<Camera> ();
		canfire = false;
        aimBool = false;		
		
		player.transform.eulerAngles = new Vector3 (0, 180, 0);// This means that the player will start facing left
		charAnimator = player.GetComponent<Animator> ();
		charAnimator.SetBool ("moving", _moving); // Means that the character is not movin, genrally in the idle 
		charAnimator.SetBool ("ground", _ground);// Character is on the ground
		charAnimator.SetBool ("running", _running);
		charAnimator.SetBool ("climbing", _climbing);
        charAnimator.SetBool("crouching", _crouch);
		currentWeapon = 2;
        charAnimator.SetInteger("currentWeapon", currentWeapon);
		health = 100f;

		menu = GameObject.FindGameObjectWithTag ("Menu");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
		menu.SetActive (false);
		//Health ();
	}
   
	void Health()
	{
        healthBar.fillAmount = health * .01f;
	}
	void HandleMenu()
	{
        Cursor.visible = paused;
        Cursor.SetCursor(null, hotSpot, cursorMode);
		if (Input.GetButtonDown ("Menu")) {
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

        if (Input.GetAxis("Vertical") < 0 && _ground)
        {
            _crouch = true;
            charAnimator.SetBool("crouching", _crouch);
            //Y Size of the Box collider on the player is equal to 15.2215, center y is = 7.1102
            Vector3 temp = this.gameObject.GetComponent<BoxCollider>().size;          
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(temp.x, 10f, temp.z);
            temp = this.gameObject.GetComponent<BoxCollider>().center;
            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(temp.x, 4.5f, temp.z);
        }
        else if (Input.GetAxis("Vertical") >0)
        {
            _crouch = false;
            charAnimator.SetBool("crouching", _crouch);
            Vector3 temp = this.gameObject.GetComponent<BoxCollider>().size;
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(temp.x, 15.2215f, temp.z);
            temp = this.gameObject.GetComponent<BoxCollider>().center;
            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(temp.x, 7.1102f, temp.z);
        }
	}
	void HandleAimMode(float fireRate)
	{
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        if (Input.GetButton ("Fire1"))
        {
          if (currentWeapon == 2 && fireRate> 2f)
          {
             
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15);
            position = Camera.main.ScreenToWorldPoint(position);
            GameObject instance = (GameObject)Instantiate(currentBullet,fpCamera.transform.position,Quaternion.identity);
            instance.transform.LookAt(position);
            instance.GetComponent<BulletMovement>().Position = position;
             Debug.Log("Banana:" + position);
             fireRate = 0;
              
          }
        }
       


        //Debug.Log(mousePos);
	}
    public bool AimMode
    {
        get { return aimBool; }
        set { aimBool = value; }
    }
	// Update is called once per frame
	void Update () {
		//Handles's Basic movement of the Character
		fireRate += Time.deltaTime;
        Health();
       
        if (health <= 0)
        {
            charAnimator.SetBool("dead", true);
        }
	// used for aiming mode
		if (Input.GetButtonDown ("Aiming")) {
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
			HandleAimMode(fireRate);
		}

        if (!aimBool)
        {
            //Handles Switching wapons
            if (Input.GetButtonDown("switchWeapon"))
            {
                if (currentWeapon < 2)
                {
                    currentWeapon++;
                    Debug.Log("current weapon is" + currentWeapon);
                }
                else
                {
                    currentWeapon = 0;
                }
            }
            // Handles Firing weapons
            if (Input.GetButton("Fire1") && fireRate > 2.5f)
            {

                if (currentWeapon == 0)//This is the melee attack, usally with a sword
                {
                    Debug.Log("MeleeAttack");
                    charAnimator.SetTrigger("canAttack");
                    charAnimator.SetInteger("currentWeapon", currentWeapon);
                    fireRate = 0;
                }
                if (currentWeapon == 1)//This is the grenade
                {
                    Debug.Log("GrenadeAttack");
                    charAnimator.SetTrigger("canAttack");
                    charAnimator.SetInteger("currentWeapon", currentWeapon);
                    fireRate = 0;
                }
                if (currentWeapon == 2)//This is the pistol
                {
                    Debug.Log("PistolAttack");
                    charAnimator.SetTrigger("canAttack");
                    charAnimator.SetInteger("currentWeapon", currentWeapon);
                    canfire = true;
                    StartCoroutine(Delay(1f));
                }
                fireRate = 0;
            }
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