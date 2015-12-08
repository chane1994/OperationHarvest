using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIButtonScript : MonoBehaviour, IPointerDownHandler {

    bool wasClicked;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        wasClicked = !wasClicked;
    }
    public bool WasClicked
    {
        get { return wasClicked; }
        set { wasClicked = value; }
    }

}
