using UnityEngine;
using System.Collections;

public class NextRoom : MonoBehaviour {

	public GameObject player;
	public int nextRoom;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisonEnter(Collision col)
	{
		if (col.gameObject.tag == player.tag) {
			Application.LoadLevel(nextRoom);
		}
	}
}
