using UnityEngine;
using System.Collections;

public class GuardWaypoint : MonoBehaviour {
    GameObject parent;
	// Use this for initialization
	void Start () 
    {
        parent = gameObject.transform.parent.gameObject;
        gameObject.transform.parent = null;
	}

    void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject == parent)
        {
            //print("TRIGGERED");
            parent.GetComponent<GuardController>().ChangeDirection();
        }
    }
}
