using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterMovementScript : MonoBehaviour {
	public Animator charAnimator; //Access to Agent Controller, the animation controller of the model
	public GameObject player;// Will be set to private
    private Camera camera; // Will be set to private
    private List<string> obtainedLore = new List<string>();//used to track which lore the player has unlocked

	public float movingSpeed, jumpingSpeed;
	bool _moving, _ground, _running, _climbing, _crouch;
	Rigidbody rigidBody;
	int currentWeapon;
	public bool paused;
	public Transform muzzleLocation;
    public Transform grenadeLocation;
	public GameObject currentBullet;
	public GameObject currentGrenade;
	Event shotEvent;
	public float health;
	bool direction;

    public float soundIntensity;

	GameObject menu;
    Image healthBar;

    public Texture2D cursorTexture;
    public CursorMode cursorMode;
    public Vector2 hotSpot;

    public TestIKScript testIKScript;

	bool aimBool; // Determines whether the game is in aiming mode
	public float fireRate; //Gives a set fire rate to attacks
	bool canfire; //Used to stop certain attacks from firing into the time is right (mainly used for shooting);
	// Use this for initialization
	void Start () {
		player = this.gameObject;
		paused = false;
		rigidBody = player.GetComponent<Rigidbody> ();
		_moving = false;
		_ground = true;
        _crouch = false;
	
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		canfire = false;
        aimBool = false;

        //This Script handles IK Stuff
        testIKScript = this.gameObject.GetComponent<TestIKScript>();

		player.transform.eulerAngles = new Vector3 (0, 180, 0);// This means that the player will start facing left
		charAnimator = player.GetComponent<Animator> ();
		charAnimator.SetBool ("moving", _moving); // Means that the character is not movin, genrally in the idle 
		charAnimator.SetBool ("ground", _ground);// Character is on the ground
		charAnimator.SetBool ("running", _running);// Character is Running
		charAnimator.SetBool ("climbing", _climbing);
        charAnimator.SetBool("crouching", _crouch);
		currentWeapon = 2;
        charAnimator.SetInteger("currentWeapon", currentWeapon);
        //Set from 100->10 to make demo seem difficult.  Maybe make permenant?
		health = 100f;

		menu = GameObject.FindGameObjectWithTag ("Menu");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
		menu.SetActive (false);
		//Health ();
	}
    public List<string> GetLore
    {
        get { return obtainedLore; }
    }
	void Health()
	{
        healthBar.fillAmount = health * .01f;
	}
    public bool Crouch
    {
        get { return _crouch; }
    }
	void HandleMenu()
	{
        Health();
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
			//Debug.Log (Input.GetAxis("Horizontal"));
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
				
                
			} else {
				direction = false;
				player.transform.eulerAngles = new Vector3 (0, -90, 0);
			}
			if(_running)
			{
				player.transform.Translate (Vector3.left * movingSpeed * 5*-Input.GetAxis ("Horizontal"), camera.transform);
                charAnimator.SetBool("running", _running);
			}
			else
			{
				player.transform.Translate (Vector3.left * movingSpeed * -Input.GetAxis ("Horizontal"), camera.transform);
                charAnimator.SetBool("running", _running);
			}
			
			//player.transform.Translate(Vector3.left);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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
                StartCoroutine(jumpDelay(.30f));
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
        testIKScript.ikActive = true;
        
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
        //TakeHit(1.0f);
        if (health <= 0)
        {
            charAnimator.SetBool("dead", true);
            this.enabled = false;
        }
	// used for aiming mode
		

		HandleMenu ();
        if (!paused)
        {
            HandleMovement();

            //Handles Switching wapons
            if (Input.GetButtonDown("switchWeapon"))
            {
                if (currentWeapon < 2)
                {
                    currentWeapon++;
                    //Debug.Log("current weapon is" + currentWeapon);

                }
                else
                {
                    currentWeapon = 0;
                }
            }
            if (currentWeapon == 1)
            {
                aimBool = false;
                Cursor.visible = true;
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            }
            else if (currentWeapon == 0)
            {
                aimBool = false;
            }
            else
            {
                aimBool = true;
            }
            if (_climbing)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
                {
                    charAnimator.speed = 0;
                    this.gameObject.GetComponent<Rigidbody>().Sleep();
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                }
                else
                {
                    charAnimator.speed = 1;
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                }

            }
            else
            {
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            }
            // Handles Firing weapons
            if (Input.GetButton("Fire1") && fireRate > 1.5f)
            {
                fireRate = 0;
                if (currentWeapon == 0)//This is the melee attack, usally with a sword
                {
                    //Debug.Log("MeleeAttack");
                    charAnimator.SetTrigger("canAttack");
                    charAnimator.SetInteger("currentWeapon", currentWeapon);

                    aimBool = false;
                }
                if (currentWeapon == 1)//This is the grenade
                {
                    aimBool = false;
                    //Debug.Log("GrenadeAttack");
                    charAnimator.SetTrigger("canAttack");
                    charAnimator.SetInteger("currentWeapon", currentWeapon);
                    GameObject instance = (GameObject)Instantiate(currentGrenade, grenadeLocation.position, Quaternion.identity);
                    instance.GetComponent<BulletMovement>().SetAttacker(this.gameObject);

                }
                if (currentWeapon == 2)//This is the pistol
                {
                    aimBool = true;
                    //Debug.Log("PistolAttack");
                    //charAnimator.SetTrigger("canAttack");
                    // charAnimator.SetInteger("currentWeapon", currentWeapon);
                    canfire = true;
                    Debug.Log("Fire a shot!");
                    GameObject instance = (GameObject)Instantiate(currentBullet, muzzleLocation.position, muzzleLocation.rotation);
                    instance.GetComponent<BulletMovement>().SetAttacker(this.gameObject);
                    gameObject.GetComponent<AudioSource>().Play();

                }

            }
            if (!aimBool)
            {
                testIKScript.ikActive = false;
            }
            else
            {
                HandleAimMode(fireRate);
            }
        }

}



	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Floor") {
			_ground = true;
            charAnimator.speed = 1;
            AnimationHandler(1);
			Debug.Log ("Landed on the ground");
		}

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Climable" ) {
			
			if (Input.GetButton ("Vertical")  &&Input.GetButton ("Vertical") ) {
				//Debug.Log ("Try Climbing");
				_climbing = true;
				charAnimator.SetBool("climbing",_climbing);
				player.transform.Translate (Vector3.up * movingSpeed * Input.GetAxis ("Vertical"), camera.transform);

			}	
					
		}
	}
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Climable") {

            if (Input.GetButton("Vertical") && Input.GetButton("Vertical"))
            {
				//Debug.Log ("Try Climbing");
				_climbing = true;
				charAnimator.SetBool("climbing",_climbing);
				player.transform.Translate (Vector3.up * movingSpeed * Input.GetAxis ("Vertical"), camera.transform);

			}	
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Climable") {
			_climbing = false;
			charAnimator.SetBool("climbing",_climbing);
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
    //Right=true
    //Left=false
	public bool Direction
	{
		get {return direction;}
        set { direction = value; }
	}
    IEnumerator jumpDelay(float x)
    {
        yield return new WaitForSeconds(x);
        charAnimator.speed = 0;
        rigidBody.AddForce(Vector3.up * jumpingSpeed);
        
    }
    public void TakeHit(float f)
    {
        health -= f;
    }
    public void AddLore(string s)
    {
        print(s+".txt");
        obtainedLore.Add(s+".txt");
    }
}