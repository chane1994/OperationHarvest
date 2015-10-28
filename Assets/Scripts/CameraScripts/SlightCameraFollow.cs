using UnityEngine;
using System.Collections;

public class SlightCameraFollow : MonoBehaviour {
    public GameObject player;
    public float zoom;
	// Use this for initialization
	void Start () 
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y +2 , player.transform.position.z-zoom);
	}
    
	void LateUpdate()
	{
		//Debug.Log("Player's Position: "+player.transform.position);
       // Debug.Log("Camera Position: "+gameObject.transform.position);
        if (player.transform.position.x - gameObject.transform.position.x >= 2)
		    transform.position = new Vector3 (player.transform.position.x-2, player.transform.position.y +2 , player.transform.position.z-zoom);
        else if (player.transform.position.x - gameObject.transform.position.x <= -2)
            transform.position = new Vector3(player.transform.position.x + 2, player.transform.position.y + 2, player.transform.position.z - zoom);
        else
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 2, player.transform.position.z - zoom);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
