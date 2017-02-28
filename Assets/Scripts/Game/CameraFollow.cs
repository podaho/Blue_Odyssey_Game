using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform toFollow;
    public float x;
    public float y;
    public float z;
    public float mapSizeWidth = 15.795f;
    public float mapSizeHeight = 10f;
    private float height;
    private float width;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        height = 2f * this.GetComponent<Camera>().orthographicSize;
        width = height * this.GetComponent<Camera>().aspect;
        //Debug.Log(this.GetComponent<Camera>().aspect);
        if (Mathf.Abs(toFollow.position.x) >= mapSizeWidth-width/2)
        {
            if(toFollow.position.x > 0)
            {
                x = mapSizeWidth - width / 2;
            } else
            {
                x = -(mapSizeWidth - width / 2);
            }
        }
        else
        {
            x = toFollow.position.x;
        }
        if (Mathf.Abs(toFollow.position.y) >= mapSizeHeight-height/2)
        {
            if (toFollow.position.y > 0)
            {
                y = mapSizeHeight - height / 2;
            }
            else
            {
                y = -(mapSizeHeight - height / 2);
            }
        }
        else
        {
            y = toFollow.position.y;
        }
        transform.position = new Vector3(x, y, toFollow.position.z-10f);
	}
}
