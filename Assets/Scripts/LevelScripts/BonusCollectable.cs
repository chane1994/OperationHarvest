using UnityEngine;
using System.Collections;
using System.IO;

public class BonusCollectable : MonoBehaviour {
    const int NUM_BUTTONS = 10;
    bool alarmHasBeenTriggered = false;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
 	void Update () 
    {
	
	}

    public void UpdateFile()
    {
        if (!alarmHasBeenTriggered)
        {
            string[,] text = new string[NUM_BUTTONS, 2];
            using (StreamReader file = new StreamReader("./Assets/Lore/LoreManager.txt"))
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

            using (StreamWriter file = new StreamWriter("./Assets/Lore/LoreManager.txt"))
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

    public void AlarmWasTriggered()
    {
        alarmHasBeenTriggered = true;
    }
}
