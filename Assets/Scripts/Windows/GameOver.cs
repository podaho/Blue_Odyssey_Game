using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : GenericWindow {

    public Text p1stat;
    public Text p2stat;
    public Text p1Acc;
    public Text p2Acc;
    public Text p1Cas;
    public Text p2Cas;
    private int p1hit;
    private int p2hit;
    private int p1miss;
    private int p2miss;
    private float p1ic;
    private float p2ic;
    private float p1ec;
    private float p2ec;
    //public GameObject stat;

    public void ClearText()
    {
        p1stat.text = "";
        p2stat.text = "";
        p1Acc.text = "";
        p2Acc.text = "";
        p1Cas.text = "";
        p2Cas.text = "";
    }

    public override void Open()
    {
        ClearText();
        base.Open();
    }

    public void btnRestart()
    {
        Application.LoadLevel("Options");
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("winner")==0)
        {
            p1stat.text = "P1 Wins";
            p2stat.text = "P2 Loses";
        }
        else
        {
            p1stat.text = "P1 Loses";
            p2stat.text = "P2 Wins";
        }
        p1hit = PlayerPrefs.GetInt("p1hit");
        p2hit = PlayerPrefs.GetInt("p2hit");
        p1miss = PlayerPrefs.GetInt("p1miss");
        p2miss = PlayerPrefs.GetInt("p2miss");
        p1ic = PlayerPrefs.GetInt("p1ic");
        p2ic = PlayerPrefs.GetInt("p2ic");
        p1ec = PlayerPrefs.GetInt("p1ic");
        p2ec = PlayerPrefs.GetInt("p2ic");
    }

    void Update()
    {
        Debug.Log("p1h = " + p1hit);
        Debug.Log("p1m = " + p1miss);
        Debug.Log("p2h = " + p2hit);
        Debug.Log("p2m = " + p2miss);
        Debug.Log("p1ic = " + p1ic);
        Debug.Log("p1ec = " + p1ec);
        Debug.Log("p2ic = " + p2ic);
        Debug.Log("p2ec = " + p2ec);
        p1Acc.text = Mathf.FloorToInt((p1hit / (p1hit + p1miss)) * 100).ToString() + "%";
        p2Acc.text = Mathf.FloorToInt((p2hit / (p2hit + p2miss)) * 100).ToString() + "%";
        p1Cas.text = Mathf.FloorToInt(((p1ic - p1ec) / p1ic) * 100).ToString() + "%";
        p2Cas.text = Mathf.FloorToInt(((p1ic - p1ec) / p1ic) * 100).ToString() + "%";
    }
}
