using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//Used to handle a bunch of basic Handler stuff. Things like sound can be add to it later.
public class HandlerScript : MonoBehaviour, IPointerDownHandler {
    public GameObject globalTimerButton;
    public GameObject mapButton;
    public GameObject computerButton;
    public GameObject inputTimer;
    public Animator parentAnimator;
    int animationState;
    bool activeHandler; // Helps handle the animation and stops it from doing the animations repeately
    bool wasClicked;
    bool isOn;
	// Use this for initialization
	void Start () {
        wasClicked = false;
        parentAnimator = this.transform.parent.GetComponent<Animator>();
        globalTimerButton.SetActive(wasClicked);
        mapButton.SetActive(wasClicked);
        computerButton.SetActive(wasClicked);
        inputTimer.SetActive(false);
        activeHandler = false;
        animationState = 2;
        isOn = false;
        
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(wasClicked){
            StartCoroutine(Yield(1f));
            globalTimerButton.SetActive(true);
           mapButton.SetActive(true);
           computerButton.SetActive(true);
           isOn = true;
        }
        else
        {
            globalTimerButton.SetActive(false);
            mapButton.SetActive(false);
            computerButton.SetActive(false);
            isOn = false;
        }
        if (isOn)
        {
            inputTimer.SetActive(globalTimerButton.GetComponent<UIButtonScript>().WasClicked);
        }
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        wasClicked = !wasClicked;
        parentAnimator.SetBool("wasClicked", wasClicked);
    }
    public bool WasClicked
    {
        get { return wasClicked; }
        set { wasClicked = value; }
    }
    IEnumerator Yield(float x)
    {
        yield return new WaitForSeconds(x);
    }

}
