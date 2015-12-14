using UnityEngine;
using System.Collections;

public class ZLock : MonoBehaviour {
    GameObject Player;
    float mainZ;
	// Use this for initialization
	void Start () 
    {
        Player= GameObject.FindGameObjectWithTag("Player");
        mainZ = Player.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, mainZ);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = new Vector3(enemies[i].transform.position.x, enemies[i].transform.position.y, mainZ);
        }
	}
}
