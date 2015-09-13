using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    //Used to trigger walking animation
    bool _move;
    //Used to trigger firing animation
    bool _seePlayer;
    //Animation of the enemy
    public Animator animate;
    public GameObject player;
    public float movingSpeed;
    public int count = 0;
    // Use this for initialization
    void Start()
    {
        _move = false;
        _seePlayer = false;
        animate = this.gameObject.GetComponent<Animator>();
        animate.SetBool("seePlayer", _seePlayer);
        animate.SetBool("move", _move);
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x - this.gameObject.transform.position.x <= 30 && movingSpeed > 0) || (this.gameObject.transform.position.x - player.transform.position.x <= 30 && movingSpeed < 0))
        {
			Debug.Log("banana");
           _move = false;
           _seePlayer = true;
           Attack();
           animate.SetBool("seePlayer", _seePlayer);

       }
       else
       {
           _move = true;
           _seePlayer = false;
            Move();
       }
        


    }
    //Will add the direction into a seperate method later
    void Move()
    {
        count++;
        //handles the direction of the enemy
        if (movingSpeed > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            this.gameObject.transform.Translate(new Vector3(-movingSpeed, 0, 0));
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            this.gameObject.transform.Translate(new Vector3(movingSpeed, 0, 0));
        }
        if (count >= 500)
        {
            movingSpeed *= -1;
            count = 0;
        }
    }
    void Attack()
    {
        if (movingSpeed > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}

