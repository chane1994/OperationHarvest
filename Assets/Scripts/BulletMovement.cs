using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {


	float age;
	GameObject player;
	bool direction;
	// Use this for initialization
	void Start () {

		Debug.Log ("My direction is" + direction);
		age = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		direction = player.GetComponent<CharacterMovementScript> ().Direction;
	}

	// Update is called once per frame
	void Update () {
		age += Time.deltaTime;
		if (this.gameObject.tag == "Bullet") {
			if (direction){
				this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right*50);
				Debug.Log ("Banana");
			}
			else 
				this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left*50);
		}
		if (this.gameObject.tag == "Grenade") {

				if (direction)
					this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right);
				else
					this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left);
			}
		if (age > 8)
			Destroy (this.gameObject);
	

}
}
