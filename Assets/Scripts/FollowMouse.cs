using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPoint = Input.mousePosition; 
		screenPoint.z = Mathf.Abs(Camera.main.transform.position.z) + GameObject.FindGameObjectWithTag("Player").transform.position.z ;
		this.gameObject.transform.position = Camera.main.ScreenToWorldPoint (screenPoint); 
	}
}
