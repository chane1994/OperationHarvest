using UnityEngine;
using System.Collections;

public class EnemyCamera : MonoBehaviour {
	public Light light1;
	public Light light2;
	public Light light3;
	public Light light4;
	public Light light5;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    public AudioSource audio5;
	public Light cameraLight;
	public GameObject player;
    public bool currentlyPlaying = false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.transform.position.y >= 6 && player.transform.position.x>-2.7 && player.transform.position.x<6.2 && !currentlyPlaying) 
		{
			cameraLight.color = Color.red;
			light1.intensity = 8;
			light2.intensity = 8;
			light3.intensity = 8;
			light4.intensity = 8;
			light5.intensity = 8;
            audio1.Play();
            audio2.Play();
            audio3.Play();
            audio4.Play();
            audio5.Play();
            currentlyPlaying = true;
		}
		
	}
}