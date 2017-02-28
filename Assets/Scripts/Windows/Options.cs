using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : GenericWindow {

    public Text H1;
    public Text H2;
    public Text C1;
    public Text C2;
    public Text S1;
    public Text S2;
    public Text A1;
    public Text A2;
    public Text pts1;
    public Text pts2;
    public int p1Points = 20;
    public int p2Points = 20;
    public int hull1 = 0;
    public int hull2 = 0;
    public int crew1 = 0;
    public int crew2 = 0;
    public int speed1 = 0;
    public int speed2 = 0;
    public int arm1 = 0;
    public int arm2 = 0;

    public bool gameStart = false;


    public void SwitchOptions()
    {
        Debug.Log("Switch");
        eventSystem.SetSelectedGameObject(firstSelectedP2);
    }

    public void AllDone()
    {
        PlayerPrefs.SetInt("hullp1", hull1);
        PlayerPrefs.SetInt("hullp2", hull2);
        PlayerPrefs.SetInt("crewp1", crew1);
        PlayerPrefs.SetInt("crewp2", crew2);
        PlayerPrefs.SetInt("speedp1", speed1);
        PlayerPrefs.SetInt("speedp2", speed2);
        PlayerPrefs.SetInt("armp1", arm1);
        PlayerPrefs.SetInt("armp2", arm2);
        var val = GameObject.Find("Values");
        //Debug.Log("DONE");
        //val.GetComponent<ValueRetainer>().setVal(hull1, hull2, crew1, crew2, speed1, speed2, arm1, arm2);
        eventSystem.SetSelectedGameObject(startGame);
    }

    public void addH1()
    {
        if(p1Points > 0)
        {
            hull1 += 1;
            p1Points -= 1;
        }
    }

    public void minH1()
    {
        if (hull1 > 0)
        {
            hull1 -= 1;
            p1Points += 1;
        }
    }

    public void addC1()
    {
        if (p1Points > 0)
        {
            crew1 += 1;
            p1Points -= 1;
        }
    }

    public void minC1()
    {
        if (crew1 > 0)
        {
            crew1 -= 1;
            p1Points += 1;
        }
    }

    public void addS1()
    {
        if (p1Points > 0)
        {
            speed1 += 1;
            p1Points -= 1;
        }
    }

    public void minS1()
    {
        if (speed1 > 0)
        {
            speed1 -= 1;
            p1Points += 1;
        }
    }

    public void addA1()
    {
        if (p1Points > 0)
        {
            arm1 += 1;
            p1Points -= 1;
        }
    }

    public void minA1()
    {
        if (arm1 > 0)
        {
            arm1 -= 1;
            p1Points += 1;
        }
    }

    public void addH2()
    {
        if (p2Points > 0)
        {
            hull2 += 1;
            p2Points -= 1;
        }
    }

    public void minH2()
    {
        if (hull2 > 0)
        {
            hull2 -= 1;
            p2Points += 1;
        }
    }

    public void addC2()
    {
        if (p2Points > 0)
        {
            crew2 += 1;
            p2Points -= 1;
        }
    }

    public void minC2()
    {
        if (crew2 > 0)
        {
            crew2 -= 1;
            p2Points += 1;
        }
    }

    public void addS2()
    {
        if (p2Points > 0)
        {
            speed2 += 1;
            p2Points -= 1;
        }
    }

    public void minS2()
    {
        if (speed2 > 0)
        {
            speed2 -= 1;
            p2Points += 1;
        }
    }

    public void addA2()
    {
        if (p2Points > 0)
        {
            arm2 += 1;
            p2Points -= 1;
        }
    }

    public void minA2()
    {
        if (arm2 > 0)
        {
            arm2 -= 1;
            p2Points += 1;
        }
    }

    public void StartGame()
    {
        //manager.Open(1);
        gameStart = true;
    }

    void Update()
    {
        H1.text = hull1.ToString();
        C1.text = crew1.ToString();
        S1.text = speed1.ToString();
        A1.text = arm1.ToString();
        H2.text = hull2.ToString();
        C2.text = crew2.ToString();
        S2.text = speed2.ToString();
        A2.text = arm2.ToString();
        pts1.text = p1Points.ToString();
        pts2.text = p2Points.ToString();
    }
}
