using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y +2 , player.transform.position.z-5);

	}

	void FixedUpdate()
	{
		Debug.Log(player.transform.position);
		transform.position = new Vector3 (0, player.transform.position.y +2 , player.transform.position.z-5);

	}
	// Update is called once per frame
	void Update () {


	}
}
