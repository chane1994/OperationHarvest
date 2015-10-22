using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
    Image healthBar;
    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        healthBar.fillAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovementScript>().health * 0.01f;
	}
}
