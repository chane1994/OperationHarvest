using UnityEngine;
using System.Collections;

public class RagDollScript : MonoBehaviour {
    GameObject player;
    float age;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());
        age = 0;
	}
	
	// Update is called once per frame
	void Update () {
        age += Time.deltaTime;
        if (age > 3)
            Destroy(this.gameObject);
	}
}
