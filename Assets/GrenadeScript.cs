using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour {
    Rigidbody rigid;
    public float age;
    public float explosiveForce;
    public float explosiveRadius;
	// Use this for initialization
	void Start () {
        rigid = this.gameObject.GetComponent<Rigidbody>();
        explosiveForce = 10000;
        explosiveRadius = 10000;
	}
	
	// Update is called once per frame
	void Update () {
        age -= Time.deltaTime;
        if (age <= 0)
        {
           rigid.AddExplosionForce(explosiveForce,this.transform.position,explosiveRadius);
           Destroy(this.gameObject);
        }
	}
}
