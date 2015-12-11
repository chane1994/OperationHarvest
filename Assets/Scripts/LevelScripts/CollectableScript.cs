using UnityEngine;
using System.Collections;
using System.IO;


public class CollectableScript : MonoBehaviour {
    const int NUM_BUTTONS = 10;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        gameObject.transform.Rotate(0f, 1f, 0f);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //col.GetComponent<CharacterMovementScript>().AddLore(gameObject.name);
            UpdateFile();
            Destroy(gameObject);
        }
    }

    void UpdateFile()
    {
        string[,] text = new string[NUM_BUTTONS, 2];
        using (StreamReader file = new StreamReader("./Assets/loreFiles/LoreManager.txt"))
        {
            string line = "";
            for (int i = 0; i < NUM_BUTTONS; i++)
            {
                line = file.ReadLine();
                text[i, 0] = line.Split(' ')[0];
                text[i, 1] = line.Split(' ')[1];
                if (text[i, 0] == gameObject.name)
                {
                    text[i, 1] = "true";
                }
            }
        }

        using (StreamWriter file = new StreamWriter("./Assets/loreFiles/LoreManager.txt"))
        {
            file.Flush();
            for (int i = 0; i < NUM_BUTTONS; i++)
            {
                file.WriteLine(text[i, 0] + " " + text[i, 1]);
            }
        }
        print("File updated");
    }
}
