using UnityEngine;
using System.Collections;

public class SlightCameraFollow : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () 
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		//transform.position = new Vector3 (player.transform.position.x, player.transform.position.y +2 , player.transform.position.z-5);
	}
    
	void LateUpdate()
	{
		Debug.Log("Player's Position: "+player.transform.position);
        Debug.Log("Camera Position: "+gameObject.transform.position);
        if (player.transform.position.x - gameObject.transform.position.x >= 2)
		    transform.position = new Vector3 (player.transform.position.x-2, player.transform.position.y +2 , player.transform.position.z-5);
        else if (player.transform.position.x - gameObject.transform.position.x <= -2)
            transform.position = new Vector3(player.transform.position.x + 2, player.transform.position.y + 2, player.transform.position.z - 5);
        else
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 2, player.transform.position.z - 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
