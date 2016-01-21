using UnityEngine;
using System.Collections;

public class CollectedData : MonoBehaviour {
    public GameObject button;
    public GameObject door;
    public GameObject winScreen;
    public GameObject[] shutters;
	// Use this for initialization
	void Start () {
        shutters = GameObject.FindGameObjectsWithTag("Shutters");
        //winScreen = GameObject.Find("Winscreen");
        if(this.gameObject.name == "Goal")
        winScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider col)
    {
        
        if (col.gameObject.tag == "Player" && button.GetComponent<UIButtonScript>().WasClicked  && this.gameObject.name ==  "Goal" )
        {
            winScreen.SetActive(true);
            StartCoroutine(Delay(3f));
            //GameObject.FindGameObjectWithTag("Bonus Collectable").GetComponent<BonusCollectable>().UpdateFile();
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
    IEnumerator Delay(float x)
    {
        yield return new WaitForSeconds(x);
        Application.LoadLevel("MainMenuScene");
    }
}
