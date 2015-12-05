using UnityEngine;
using System.Collections;

public class MainMenuCharacterCrawling : MonoBehaviour {

    public Animator charAnimator;
    public GameObject player;
    public float timer;
    bool walk;
	// Use this for initialization
	void Start () 
    {
        charAnimator.SetBool("crouching", true);
        // Means that the character is not movin, genrally in the idle 
        charAnimator.SetBool("ground", true);// Character is on the ground
        charAnimator.SetBool("running", false);// Character is Running
        charAnimator.SetBool("climbing", false);
        walk = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        if (timer > 1 && !walk)
        {
            timer = 0;
            walk = !walk;
        }
       
        charAnimator.SetBool("moving", walk);
        //Y Size of the Box collider on the player is equal to 15.2215, center y is = 7.1102
        /*Vector3 temp = this.gameObject.GetComponent<BoxCollider>().size;
        this.gameObject.GetComponent<BoxCollider>().size = new Vector3(temp.x, 10f, temp.z);
        temp = this.gameObject.GetComponent<BoxCollider>().center;
        this.gameObject.GetComponent<BoxCollider>().center = new Vector3(temp.x, 4.5f, temp.z);*/
	}
}
