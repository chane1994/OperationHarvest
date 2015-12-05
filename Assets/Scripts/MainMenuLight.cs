using UnityEngine;
using System.Collections;
using System;


public class MainMenuLight : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (gameObject.transform.position.x < -35)
        {
            gameObject.transform.position = new Vector3(30, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x-0.3f, gameObject.transform.position.y, gameObject.transform.position.z);

	}
}
