using UnityEngine;
using System.Collections;

public class TempTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Light Manager").GetComponent<LightManager>().SetAlarm(true);
        }
    }
}
