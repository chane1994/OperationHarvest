using UnityEngine;
using System.Collections;

public class Computer : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        
        print("collided");
        if (col.gameObject.tag == "Player") ;
            GameObject.FindGameObjectWithTag("Light Manager").GetComponent<LightManager>().SetAlarm(false);
    }
}
