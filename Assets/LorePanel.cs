using UnityEngine;
using System.Collections.Generic;

public class LorePanel : MonoBehaviour
{
    GameObject player;
    public GameObject lorePrefab;
    List<string> obtLore;
    RectTransform myRect;
    // Use this for initialization
    void Awake()
    {

    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        obtLore = player.GetComponent<CharacterMovementScript>().GetLore;
        myRect = gameObject.GetComponent<RectTransform>();
        myRect.offsetMin = new Vector2(0, -500);
        myRect.offsetMax = new Vector2(0, 0);

        int numberOfItems = 5;
        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject lore = (GameObject)Instantiate(lorePrefab);
            RectTransform loreRect = lore.GetComponent<RectTransform>();
            lore.transform.SetParent(this.transform,false);
            loreRect.offsetMin = new Vector2(0, -(i * 65f));
            loreRect.offsetMax = new Vector2(0, -(i * 65f));

        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateLoreItems()
    {
        
    }
}
