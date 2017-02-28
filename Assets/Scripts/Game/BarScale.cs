using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScale : MonoBehaviour {
    //public bool left;
    //public bool cannonLeft;
    public float startVal;
    public Vector3 startPos;
    public GameObject healthTxt;
    public float currVal;
    //private Vector3 scale;


    // Use this for initialization
    void Start()
    {
        //scale = new Vector3(1, 1, 1);
        startPos = GetComponent<Transform>().localPosition;
        currVal = startVal;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().localScale = new Vector3(currVal / startVal, this.GetComponent<Transform>().localScale.y, this.GetComponent<Transform>().localScale.z);
        this.GetComponent<Transform>().localPosition = new Vector3(startPos.x * (currVal / startVal), this.GetComponent<Transform>().localPosition.y, this.GetComponent<Transform>().localPosition.z);
        if(healthTxt) healthTxt.GetComponent<Text>().text = currVal + "/" + startVal;
    }

    public void showBar(float current)
    {
        currVal = current;
    }
}
