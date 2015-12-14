using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HandleTutorialLevel : MonoBehaviour {
    public Sprite WASD;
    public Sprite clickFire;
    public Sprite GPS;
    public Sprite UseComputer;
    public Sprite MissionComplete;
    public Sprite DataServer;
    public Sprite DoorLocked;
    public Image thisImage;
    public Sprite remainStealth;
    public Sprite howtoFinishLevel;
    public Sprite GPSExplanation;
    public Sprite howtoAccessHud;
    public Sprite dataFileExplain;
    public GameObject gpsObject;
    bool removeImage;
    float age;

    void Awake()
    {
        if (Application.loadedLevelName != "CurrentLevel")
        {
            this.enabled = false;
        }
    }
	// Use this for initialization
	void Start () {
       // thisImage= this.gameObject.GetComponent<Image>();
        age = 0;
        gpsObject = GameObject.Find("GPSExplantaion");
        gpsObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (removeImage)
        {
            age += Time.deltaTime;
            if (age > 3)
            {
                age = 0;
                removeImage = false;
            }
        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "DataServer")
        {
            thisImage.sprite = DataServer;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "GPSHandler")
        {
            thisImage.sprite = GPS;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "UseComputer")
        {
            thisImage.sprite = UseComputer;
            Destroy(col.gameObject);
            gpsObject.SetActive(true);
        }
        if (col.gameObject.name == "WASD")
        {
            thisImage.sprite = WASD;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "DoorLocked")
        {
            Destroy(col.gameObject);
            thisImage.sprite = DoorLocked;
        }
        if (col.gameObject.name == "clickFire")
        {
            Destroy(col.gameObject);
            thisImage.sprite = clickFire;
        }
        if (col.gameObject.name == "MissionComplete")
        {
            Destroy(col.gameObject);
            thisImage.sprite = MissionComplete;
        }
        if (col.gameObject.name == "remainStealth")
        {
            Destroy(col.gameObject);
            thisImage.sprite = remainStealth;
        }
        if (col.gameObject.name == "FinishLevel")
        {
            thisImage.sprite = howtoFinishLevel;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "GPSExplantaion")
        {
            thisImage.sprite = GPSExplanation;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "accessHud")
        {
            thisImage.sprite = howtoAccessHud;
            Destroy(col.gameObject);
           
        }
        if (col.gameObject.name == "DataFileExplain")
        {
            thisImage.sprite = dataFileExplain;
            Destroy(col.gameObject);
        }
    }
}
