using UnityEngine;
using System.Collections;

public class InteractableScripts : MonoBehaviour {

    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0, -1, 0));
        if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) < 10 && this.tag == "Briefcase")
        {
            Destroy(this.gameObject);
        }
	}
}
