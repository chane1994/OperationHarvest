﻿using UnityEngine;
using System.Collections;

public class CollectedData : MonoBehaviour {
    public GameObject button;
    public GameObject door;
    public GameObject[] shutters;
	// Use this for initialization
	void Start () {
        shutters = GameObject.FindGameObjectsWithTag("Shutters");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" && button.GetComponent<UIButtonScript>().WasClicked  && this.gameObject.name ==  "Goal" )
        {
            Application.LoadLevel("MainMenuScene");
        }
        if (col.gameObject.tag == "Player" && button.GetComponent<UIButtonScript>().WasClicked && this.gameObject.name != "Goal")
        {
            button.GetComponent<UIButtonScript>().WasClicked = false;
            Debug.Log("It worked!");
            door.SetActive(false);
            if (shutters.Length != 0)
            {
                foreach (GameObject g in shutters)
                {
                    g.SetActive(false);
                }
            }
        }
    }
}
