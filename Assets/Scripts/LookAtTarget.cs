using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {
    public Transform lookAt;
    public Vector3 oldPos;
    public bool lookForTarget;
	// Use this for initialization
	void Start () {
        oldPos = this.transform.localPosition;
        lookAt = GameObject.FindGameObjectWithTag("LookAtObj").transform;
        lookForTarget = true;
	}
	
	// Update is called once per frame
    void Update()
    {


        this.transform.localPosition = Vector3.zero;
        transform.LookAt(lookAt.position);
        transform.Rotate(new Vector3(1.0f, 0, 0), -90);
    }
}
