using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
    public GameObject player;
    public GameObject temp;
    public GameObject gameOver;
    public GameObject button;
    public GameObject button2;
    bool spawn;
	// Use this for initialization
	void Start () {
        gameOver.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver.activeSelf)
        {
            if (button.GetComponent<UIButtonScript>().WasClicked)
            {
                button.GetComponent<UIButtonScript>().WasClicked = false;
                gameOver.SetActive(false);
                HandleNewScene(temp);
            }
            if (button2.GetComponent<UIButtonScript>().WasClicked)
            {
                Application.LoadLevel("MainMenuScene");
            }
        }
	}
    public void Spawner(GameObject g)
    {
        temp = g;
       temp.GetComponent<CharacterMovementScript>().enabled = false;
        StartCoroutine(Delay(3));
       
    }
    IEnumerator Delay(float x)
    {
        yield return new WaitForSeconds(x);
        gameOver.SetActive(true);
            
    }
    void HandleNewScene(GameObject g)
    {
        GameObject c = (GameObject)Instantiate(player, this.transform.position, Quaternion.identity);
        c.GetComponent<CharacterMovementScript>().HealthBar = g.GetComponent<CharacterMovementScript>().HealthBar;
        c.GetComponent<CharacterMovementScript>().Menu = g.GetComponent<CharacterMovementScript>().Menu;
        c.GetComponent<CharacterMovementScript>().health = 100;

        Destroy(g);
    }
}
