using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;


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
        /*string[,] text = new string[NUM_BUTTONS, 2];
          StreamReader read = null;
        //TextAsset file1 = Resources.Load("LoreManager") as TextAsset;
            read = new StreamReader("./Assets/Lore/LoreManager.txt");
            string line = "";
            for (int i = 0; i < NUM_BUTTONS; i++)
            {
                line = read.ReadLine();
                text[i, 0] = line.Split(' ')[0];
                text[i, 1] = line.Split(' ')[1];
                if (text[i, 0] == gameObject.name)
                {
                    text[i, 1] = "true";
                }
            }
            read.Close();
            //StringBuilder stringBuilder = new StringBuilder(file1.text);
           // StringWriter write = new StringWriter(stringBuilder);
            using (StreamWriter file = new StreamWriter("./Assets/Lore/LoreManager.txt"))
            {
                file.Flush();
                for (int i = 0; i < NUM_BUTTONS; i++)
                {
                    file.WriteLine(text[i, 0] + " " + text[i, 1]);
                    Debug.Log(text[i, 0] + " " + text[i, 1]);
                }
                //write.Close();
                print("File updated");
            }*/
    }
}
