using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
    private GameObject p1;
    private GameObject p2;
    private Vector3 p1Pos;
    private Vector3 p2Pos;
    public bool isP1;

    // Use this for initialization
    void Start () {
        if (isP1)
        {
            p1 = GameObject.Find("Ship");
        }
        else
        {
            p2 = GameObject.Find("Pirate");
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(isP1)
        {
            p1Pos = new Vector3(p1.GetComponent<Transform>().position.x / 38f, p1.GetComponent<Transform>().position.y / 38f, -1f);
            transform.localPosition = p1Pos;
        }
        else
        {
            p2Pos = new Vector3(p2.GetComponent<Transform>().position.x / 38f, p2.GetComponent<Transform>().position.y / 38f, -1f);
            transform.localPosition = p2Pos;
        }
	}
}
