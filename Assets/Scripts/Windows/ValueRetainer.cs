using UnityEngine;
using System.Collections;

public class ValueRetainer : MonoBehaviour {
    static public int hull1;
    static public int hull2;
    static public int crew1;
    static public int crew2;
    static public int speed1;
    static public int speed2;
    static public int arm1;
    static public int arm2;
    public int hull1s;
    public int hull2s;
    public int crew1s;
    public int crew2s;
    public int speed1s;
    public int speed2s;
    public int arm1s;
    public int arm2s;
    static public float p1Acc = 0;
    static public float p2Acc = 0;
    static public float p1Cas = 0;
    static public float p2Cas = 0;
    static public bool p1Won = false;
    public float p1Accs = 0;
    public float p2Accs = 0;
    public float p1Cass = 0;
    public float p2Cass = 0;
    public bool p1Wons = false;
    public bool gameOver = false;

    public void setVal(int h1, int h2, int c1, int c2, int s1, int s2, int a1, int a2)
    {
        hull1 = h1;
        hull2 = h2;
        crew1 = c1;
        crew2 = c2;
        speed1 = s1;
        speed2 = s2;
        arm1 = a1;
        arm2 = a2;
        hull1s = hull1;
        hull2s = hull2;
        crew1s = crew1;
        crew2s = crew2;
        speed1s = speed1;
        speed2s = speed2;
        arm1s = arm1;
        arm2s = arm2;
    }

    public void endVal(float AccG, float CasG, bool isp1, bool winner)
    {
        if(isp1)
        {
            p1Acc = AccG;
            p1Cas = CasG;
            p1Accs = p1Acc;
            p1Cass = p1Cas;
            if(winner) p1Won = true;
        }
        else
        {
            p2Acc = AccG;
            p2Cas = CasG;
            p2Accs = p2Acc;
            p2Cass = p2Cas;
            if (winner) p1Won = false;
        }
    }

    void Update()
    {
        hull1s = hull1;
        hull2s = hull2;
        crew1s = crew1;
        crew2s = crew2;
        speed1s = speed1;
        speed2s = speed2;
        arm1s = arm1;
        arm2s = arm2;
    }
}
