using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPoint = Input.mousePosition; 
		screenPoint.z = 4.42f;
		this.gameObject.transform.position = Camera.main.ScreenToWorldPoint (screenPoint); 
	}
}
