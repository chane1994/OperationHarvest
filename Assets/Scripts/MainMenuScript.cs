using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    public GameObject start;
    public GameObject credits;
    public GameObject TutorialLevelButton;
    public GameObject startImages;
    public GameObject LevelImages;
    public GameObject creditImages;
    public GameObject dataFileButton;
    public int nextStage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (start.GetComponent<UIButtonScript>().WasClicked)
        {
            LevelImages.SetActive(true);
            startImages.SetActive(false);
        }
        if (credits.GetComponent<UIButtonScript>().WasClicked)
        {
            Application.LoadLevel("Credits");
        }
        if (TutorialLevelButton.GetComponent<UIButtonScript>().WasClicked)
        {
            Application.LoadLevel("CurrentLevel");
        }
        if (dataFileButton.GetComponent<UIButtonScript>().WasClicked)
        {
            Application.LoadLevel("DataFiles");
        }
	}
}
