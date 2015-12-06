using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIButtonScript : MonoBehaviour, IPointerDownHandler {

    bool wasClicked;
    public GameObject lorePanel;
	// Use this for initialization
	void Start () {
        lorePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.name == "Lore" && wasClicked)
        {
            lorePanel.SetActive(wasClicked);
        }
        
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
