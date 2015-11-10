using UnityEngine;
using System.Collections;

public class GuardController : MonoBehaviour
{
    //Used to trigger walking animation
    bool _move;
    //Used to trigger firing animation
    bool _seePlayer;
    //Animation of the enemy
    public Animator animate;
    public GameObject player;
    public GameObject currentBullet;
    public Transform muzzleLocation;
    public float movingSpeed;
    public int count = 0;
    float health = 10;
    bool canfire=true;
    static bool alarmActive = false;
    // Use this for initialization
    void Start()
    {
        _move = false;
        _seePlayer = false;
        animate = this.gameObject.GetComponent<Animator>();
        animate.SetBool("seePlayer", _seePlayer);
        animate.SetBool("move", _move);
        gameObject.tag = "Enemy";
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.77f);
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            //Need to add a way to make sure they are on the same vertical plane
            /*if (((player.transform.position.x - gameObject.transform.position.x < 4 && player.transform.position.x > gameObject.transform.position.x && movingSpeed < 0) || (gameObject.transform.position.x - player.transform.position.x < 4 && player.transform.position.x < gameObject.transform.position.x && movingSpeed > 0)))
            {
                if (canfire)
                {
                    _move = false;
                    _seePlayer = true;
                    Attack();
                    animate.SetBool("seePlayer", _seePlayer);
                    animate.SetBool("move", _move);
                    canfire = false;
                }
            }*/
            
            int direction = 0;
            RaycastHit hit;
            if (movingSpeed > 0)
                direction = -1;
            else
                direction = 1;
            Debug.DrawRay(muzzleLocation.position, new Vector3(7 * direction, 0, 0), Color.blue);
           // if (Physics.Raycast(transform.position, new Vector3(direction, 0, 0), 7.0f))
            
            if(Physics.Raycast(muzzleLocation.position, new Vector3(direction,0,0),out hit,7.0f)&&hit.collider.tag=="Player"&&player.GetComponent<CharacterMovementScript>().health>0)
            {
                print("Player hp " + player.GetComponent<CharacterMovementScript>().health);
                if (canfire)
                {
                    _move = false;
                    _seePlayer = true;
                    Attack();
                    animate.SetBool("seePlayer", _seePlayer);
                    animate.SetBool("move", _move);
                    canfire = false;
                }
            }
            else
            {
                _move = true;
                _seePlayer = false;
                Move();
                animate.SetBool("move", _move);
                animate.SetBool("seePlayer", _seePlayer);
            }
            
        }
        //print("Can fire: " + canfire);

    }
    //Will add the direction into a seperate method later
    void Move()
    {
        count++;
        //handles the direction of the enemy
        //NOTE: Had to modify due to the scene being on a different angle that was originally.  I think?
        if (movingSpeed < 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            this.gameObject.transform.Translate(new Vector3(0, 0, -movingSpeed));
            //this.gameObject.transform.Translate(new Vector3(0, -movingSpeed, 0));
            //print("Walk a");
            //gameObject.transform.Translate(Vector3.right * movingSpeed);
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
            this.gameObject.transform.Translate(new Vector3(0, 0, movingSpeed));
            //this.gameObject.transform.Translate(new Vector3(0, movingSpeed, 0));
            //print("Walk b");
            //gameObject.transform.Translate(Vector3.left * movingSpeed);
        }
        if (count >= 100)
        {
            movingSpeed *= -1;
            count = 0;
        }
        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0.07f, this.gameObject.transform.position.z);
        
    }
    void Attack()
    {
        
        if (health > 0)
        {
            ActivateAlarm();
            if (movingSpeed < 0)
            {
                this.gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                StartCoroutine(Delay(1f));
            }
            else
            {
                this.gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
                StartCoroutine(Delay(1f));
            }
            
            canfire = true;
        }
    }
    public void TakeHit(float f)
    {
        health -= f;
        if (health <= 0)
        {
            animate.SetBool("dead", true);
        }
    }
    IEnumerator Delay(float x)
    {
        //Debug.Log ("I waited");

        //Original Bullet 
        /*yield return new WaitForSeconds(x);
        Instantiate(currentBullet,muzzleLocation.position,muzzleLocation.rotation); 
        canfire = false;*/

        //New Bullet
        yield return new WaitForSeconds(x);
        GameObject instance= new GameObject();
        if (movingSpeed > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
            StartCoroutine(Delay(1f));
            if(health>0)
                instance = (GameObject)Instantiate(currentBullet, muzzleLocation.position+new Vector3(-1,0,0), muzzleLocation.rotation);
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            StartCoroutine(Delay(1f));
            if(health>0)
                instance = (GameObject)Instantiate(currentBullet, muzzleLocation.position+new Vector3(1,0,0), muzzleLocation.rotation);
        }
        gameObject.GetComponent<AudioSource>().Play();
        //original
        //GameObject instance = (GameObject)Instantiate(currentBullet, muzzleLocation.position, muzzleLocation.rotation);
        //instance.GetComponent<BulletMovement>().Position = position;
        instance.GetComponent<BulletMovement>().SetAttacker(this.gameObject);      
    }
    void ActivateAlarm()
    {
        GameObject.FindGameObjectWithTag("Light Manager").GetComponent<LightManager>().SetAlarm(true);
    }
}

