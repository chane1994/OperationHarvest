using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
    public GameObject player;
    public GameObject currentBullet;
    public Transform muzzleLocation;
    float health = 20;
    bool canfire=true;
    int movingSpeed;
    // Use this for initialization
    void Start()
    {
        gameObject.tag = "Enemy";
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

            
    }
    //Will add the direction into a seperate method later
   
    void Attack()
    {
        if (movingSpeed > 0)
        {
           // this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
           // StartCoroutine(Delay(1f));
        }
        else
        {
           // this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
           // StartCoroutine(Delay(1f));
        }
    }
    public void TakeHit(float f)
    {
        health -= f;
        print(health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator Delay(float x)
    {
        //Debug.Log ("I waited");

        //Original Bullet 
        /*yield return new WaitForSeconds(x);
        Instantiate(currentBullet,muzzleLocation.position,muzzleLocation.rotation); 
        canfire = false;*/

        //New Bullet
        yield return new WaitForSeconds(x);
        GameObject instance = (GameObject)Instantiate(currentBullet, muzzleLocation.position, muzzleLocation.rotation);
        //instance.GetComponent<BulletMovement>().Position = position;
        instance.GetComponent<BulletMovement>().SetAttacker(this.gameObject);
        canfire = false;

    }
}

