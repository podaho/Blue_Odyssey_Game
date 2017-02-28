using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public string scene;
    public GameObject options;

    private bool loadLock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(options.GetComponent<Options>().gameStart == true && !loadLock)
        {
            loadLock = true;
            LoadScene();
        }
	}

    void LoadScene()
    {
        Application.LoadLevel(scene);
    }
}
