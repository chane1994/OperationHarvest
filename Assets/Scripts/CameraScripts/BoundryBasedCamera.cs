using UnityEngine;
using System.Collections;

public class BoundryBasedCamera : MonoBehaviour
{
    public GameObject player;
    public float zoom;
    public float cameraShift = 0;
	// Use this for initialization
	void Start () 
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		//transform.position = new Vector3 (player.transform.position.x, player.transform.position.y +2 , player.transform.position.z-zoom);
	}
    
	void LateUpdate()
	{

        if ((Input.GetAxis("Horizontal") > 0 || Input.GetKey(KeyCode.Z))&& cameraShift < 2)
        {
            cameraShift += 0.05f;
        }
        else if ((Input.GetAxis("Horizontal") < 0 || Input.GetKey(KeyCode.C)) && cameraShift > -2)
        {
            cameraShift -= 0.05f;
            
        }
        else if (Input.GetAxis ("Horizontal") == 0)
        {
            if (cameraShift < 0.25 && cameraShift > -0.25)
            {
                cameraShift = 0;
            }
            else if (cameraShift > 0)
            {
                cameraShift -= 0.05f;
            }
            else if (cameraShift < 0)
            {
                cameraShift += 0.05f;
            }
        }
        if (!player.GetComponent<CharacterMovementScript>().Crouch)
        transform.position = new Vector3(player.transform.position.x + cameraShift, player.transform.position.y + 2, player.transform.position.z - zoom);
	    
        else
            transform.position = new Vector3(player.transform.position.x + cameraShift, player.transform.position.y, player.transform.position.z - zoom);
	    
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
