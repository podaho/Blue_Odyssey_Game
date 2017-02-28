using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {
    private GameObject ship;
    public bool isP1;
    // Use this for initialization
    void Start () {
        if (isP1)
        {
            ship = GameObject.Find("Ship");
        }
        else
        {
            ship = GameObject.Find("Pirate");
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(ship.GetComponent<Movement>().health <= 0f)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
	}
}
