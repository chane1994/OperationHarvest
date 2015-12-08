using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class DataFileScript : MonoBehaviour {
    const int NUM_BUTTONS=10;
    public Sprite notObtained;
    public GameObject words;
	// Use this for initialization
	void Start () 
    {
        words.GetComponent<Text>().text = "";
        StreamReader file = new StreamReader("./Assets/loreFiles/LoreManager.txt");
        string[,] text = new string[NUM_BUTTONS, 2];
       
        
        //Requires that File be in the same order as the objects
        string line = "";
        for (int i = 0; i < NUM_BUTTONS; i++)
        {
            line = file.ReadLine();
            text[i, 0] = line.Split(' ')[0];
            text[i, 1] = line.Split(' ')[1];
            if (text[i, 1] != "true")
            {
                //gameObject.transform.GetChild(0).GetChild(i+1).GetComponent<Image>().sprite=notObtained;
                //Allows the file to be out of order if needed
                gameObject.transform.GetChild(0).FindChild(text[i, 0]).GetComponent<Image>().sprite = notObtained;
            }
        }
	}

    // Update is called once per frame
    void Update() 
    {
        for (int i = 1; i < NUM_BUTTONS+1; i++)
        {   //TODO:change the file to only contain true values
            if (gameObject.transform.GetChild(0).GetChild(i).GetComponent<UIButtonScript>().WasClicked && gameObject.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite != notObtained)
            {
                gameObject.transform.GetChild(0).GetChild(i).GetComponent<UIButtonScript>().WasClicked = false;
                words.GetComponent<Text>().text = "";
                using (StreamReader file = new StreamReader("./Assets/loreFiles/" + gameObject.transform.GetChild(0).GetChild(i).name + ".txt"))
                {
                    string line;
                    do
                    {
                        line = file.ReadLine();
                        words.GetComponent<Text>().text += line + "\n";
                    } while (line != null);
                }
            }
        }
	}
}
