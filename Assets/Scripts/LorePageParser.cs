using UnityEngine;
using System.Collections;
using System.IO;


public class LorePageParser : MonoBehaviour {
    GameObject pages;
    int count;
	// Use this for initialization
	void Start () 
    {
        pages = GameObject.Find("Pages");
        //GameObject[] allPages = pages.GetComponentsInChildren<GameObject>();
        count = pages.transform.childCount;
        string[,] text = new string[count,2];
        StreamReader file = new StreamReader("./Assets/loreFiles/LoreManager.txt");
        using (file)
        {
            string line = "";
            for (int i = 0; i < count; i++)
            {
                line = file.ReadLine();
                text[i,0] = line.Split(' ')[0];
                text[i, 1] = line.Split(' ')[1];
            }
        }
        for (int i = 0; i <count; i++)
        {
            print(text[i, 0] + " " + text[i, 1]);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
