using UnityEngine;
using System.Collections;

public class CollectableScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        gameObject.transform.Rotate(0f, 1f, 0f);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<CharacterMovementScript>().AddLore(gameObject.name);
            Destroy(gameObject);
        }
    }
}
