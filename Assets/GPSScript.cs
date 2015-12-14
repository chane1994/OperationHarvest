using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPSScript : MonoBehaviour {
    public GameObject goalObj;
    public GameObject player;
    public Vector3 lookPos;
	// Use this for initialization
	void Start () {
        goalObj = GameObject.Find("Goal");
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
         lookPos = new Vector3(goalObj.transform.position.x, goalObj.transform.position.y);
        lookPos = lookPos - player.transform.position;
        
        lookPos.Normalize();

        float rot_z = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
	}
}
