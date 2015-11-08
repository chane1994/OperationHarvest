using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {


	float age;
	GameObject attacker;
	bool direction; //if direction is true, bullet goes right. Else, it goes right
    bool aimMode;
    Vector3 position;
	// Use this for initialization
	void Start () {
		age = 0;
		//attacker = GameObject.FindGameObjectWithTag ("Player");
		//direction = attacker.GetComponent<CharacterMovementScript>().Direction;
        //aimMode = attacker.GetComponent<CharacterMovementScript>().AimMode;
        //Debug.Log("My direction is" + direction);
      //  Debug.Log("Aimmode is currently" + aimMode);
	}
    public void SetAttacker(GameObject g)
    {
        attacker = g;
        if (attacker.gameObject.tag == "Player")
        {
            direction = attacker.GetComponent<CharacterMovementScript>().Direction;
            aimMode = attacker.GetComponent<CharacterMovementScript>().AimMode;
        }
        else if (attacker.GetComponent<GuardController>())
        {
            if (attacker.GetComponent<GuardController>().movingSpeed < 0)
                direction = true;
            else
                direction = false;
        }
        
    }
	// Update is called once per frame
    public Vector3 Position
    {
        set { position = value; }
    }
	void Update () 
    {
        if (!aimMode)
        {
            
            age += Time.deltaTime;
            if (this.gameObject.tag == "Bullet")
            {
                if (direction)
                {
                    this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 50);
                   // Debug.Log("Banana");
                }
                else
                    this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 50);
            }
            if (this.gameObject.tag == "Grenade")
            {

                if (direction)
                    this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right);
                else
                    this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left);
            }
            if (age > 8)
                Destroy(this.gameObject);
        }
        else
        {
          //  Debug.Log("I going towards "+ position);
            this.transform.LookAt(position);
            age += Time.deltaTime;
            this.transform.Translate(Vector3.forward);
            
            if (age > 6)
                Destroy(this.gameObject);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.77f);
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject != attacker)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<CharacterMovementScript>().TakeHit(10f);
            }
            else if (col.gameObject.tag == "Enemy")
            {

                if (col.gameObject.GetComponent<GuardController>())
                {
                    col.gameObject.GetComponent<GuardController>().TakeHit(10f);
                }
                else if (col.gameObject.GetComponent<TurretController>())
                {
                    col.gameObject.GetComponent<TurretController>().TakeHit(10f);

                }
            }
            Destroy(gameObject);
        }
       
    }
    //this prevents the bullet from spawning on the model it comes from
    IEnumerable startAsTrigger()
    {
        this.GetComponent<Rigidbody>().detectCollisions = false;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Rigidbody>().detectCollisions = true;
    }
}
