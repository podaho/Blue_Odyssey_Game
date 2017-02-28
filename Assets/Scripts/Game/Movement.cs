using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
    public float moveCap = 100f;
    public float rotationCap = 100f;
    public float windForce = 10;
    public float mass = 100;
    public int armor = 1000;
    public bool gas = false;
    public bool tester = false;
    public bool isP1 = true;
    public float inputVelocity;
    public float inputRotation;
    public PolygonCollider2D theShip;
    public float inputStrength;
    public int damage = 20;
    public int health;
    public float crew;
    public int ammo;
    public float speed;
    public GameObject AmmoNo;
    public GameObject SpeedNo;
    public GameObject CrewNo;
    public int hitShots;
    public int missShots;
    public int initCrew;
    public int endCrew;
    //public GameObject options;

    private Vector3 spawnPos;
    private bool hit = false;
    private Animator animator;
    private GameObject shrap;
    private GameObject healthBar;
    private ObjectPool pool;
    private float accel = 0f;
    public float curVel = 0f;
    private bool sunk = false;
    private bool gameOver = false;
    private float deathCounter = 0f;
    private float deathAnimCounter = 0f;

    // Use this for initialization
    void Start () {
        theShip = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        pool = GameObject.Find("ShrapnelObjPool").GetComponent<ObjectPool>();
        ammo = 400;
        if (isP1) {
            crew = PlayerPrefs.GetInt("crewp1");
            speed = PlayerPrefs.GetInt("speedp1");
            armor = 1000 + 50*PlayerPrefs.GetInt("hullp1");
            //GUN GET VALS CREW
            windForce += 0.1f * crew;
            mass += crew;
            //GUN GET VALS ARMAMENT
            damage += PlayerPrefs.GetInt("armp2")*2;
            healthBar = GameObject.Find("P1 Canvas/Health/Health Bar");
            healthBar.GetComponent<BarScale>().startVal = armor;
            initCrew = Mathf.FloorToInt(50 + 5 * crew);
        }
        else
        {
            crew = PlayerPrefs.GetInt("crewp2");
            speed = PlayerPrefs.GetInt("speedp2");
            armor = 1000 + 50 * PlayerPrefs.GetInt("hullp2");
            //GUN GET VALS CREW
            windForce += 0.05f * crew;
            mass += crew;
            //GUN GET VALS ARMAMENT
            damage += PlayerPrefs.GetInt("armp1") * 2;
            healthBar = GameObject.Find("P2 Canvas/Health/Health Bar");
            healthBar.GetComponent<BarScale>().startVal = armor;
            initCrew = Mathf.FloorToInt(50 + 5 * crew);
        }
        health = armor;
        sunk = false;
        animator.SetBool("dead", false);
        PlayerPrefs.SetInt("missShotsP1", 0);
        PlayerPrefs.SetInt("hitShotsP1", 0);
        PlayerPrefs.SetInt("missShotsP2", 0);
        PlayerPrefs.SetInt("hitShotsP2", 0);
    }
	
	// Update is called once per frame
	void Update () {
        moveCap = ((100f + speed) * (50 + 5 * crew)/100)*0.0003f;
        healthBar.GetComponent<BarScale>().showBar(health);
        //Debug.Log(health);
        if(tester)
        {
            moveCap = 0f;
            rotationCap = 0f;
        }
        if(isP1)
        {
            inputVelocity = Input.GetAxis("Vertical");
            //if (inputVelocity != 0) Debug.Log("P1 Vertical detected");
        }
        else
        {
            //Debug.Log("P2 Vertical");
            inputVelocity = Input.GetAxis("Vertical 2");
            //if (inputVelocity != 0) Debug.Log("P2 Vertical detected");
        }
        if(inputVelocity > 0)
        {
            gas = true;
            accel = (windForce * inputVelocity) / mass;
        }
        if(inputVelocity <= 0)
        {
            gas = false;
            if (curVel > 0)
            {
                accel = 0f;
                curVel -= Time.deltaTime*0.05f;
            }
            else
            {
                curVel = 0f;
            }
        }
        if(curVel < moveCap)
        {
            curVel = curVel + accel * Time.deltaTime;
        }
        transform.position += transform.up * curVel;
        animator.SetBool("speedingUp", gas);
        inputStrength = inputVelocity;

        if(isP1)
        {
            inputRotation = Input.GetAxis("Horizontal");
            //if (inputRotation != 0) Debug.Log("P1 Rotation detected");
        }
        else
        {
            //Debug.Log("P2 Horizontal");
            inputRotation = Input.GetAxis("Horizontal 2");
            //if (inputRotation != 0) Debug.Log("P2 Rotation detected");
        }
        transform.Rotate(Vector3.back, rotationCap * inputRotation * Time.deltaTime);
        if (hit == true)
        {
            //Debug.Log("Spawn Shrap");
            hit = false;
            spawnPos = new Vector3(transform.position.x, transform.position.y, 1f);
            shrap = pool.NextObject(spawnPos, isP1);
            shrap.GetComponent<ShrapScript>().speed = 0.5f;
            shrap.GetComponent<ShrapScript>().counter = 0f;
            shrap.GetComponent<ShrapScript>().destroyCounter = 0f;
            shrap.GetComponent<ShrapScript>().recycling();
            shrap.SetActive(true);
            shrap.GetComponent<Animator>().SetBool("Shrap1Sink", false);
            shrap.GetComponent<Animator>().SetBool("Shrap2Sink", false);
            shrap.GetComponent<Animator>().SetBool("Reset", true);
        }
        //sunk = false;
        if (health <= 0 && !sunk)
        {
            //Debug.Log("anim");
            tester = true;
            animator.SetBool("dead", true);
            sunk = true;
            health = 0;
            endCrew = Mathf.FloorToInt(50 + 5 * crew);
            //gameOver = true;
            deathCounter += 1f;
            deathAnimCounter += 3f;
            //End();
            //if(isP1)
            //{
            //    GameObject.Find("Pirate").GetComponent<Movement>().End();
            //} else
            //{
            //    GameObject.Find("Ship").GetComponent<Movement>().End();
            //}
            //GameObject.Find("Values").GetComponent<ValueRetainer>().gameOver = true;
            //Application.LoadLevel("Options");
        }
        AmmoNo.GetComponent<Text>().text = ammo.ToString();
        SpeedNo.GetComponent<Text>().text = Mathf.FloorToInt(moveCap/0.0003f).ToString();
        CrewNo.GetComponent<Text>().text = Mathf.FloorToInt(50 + 5 * crew).ToString();
        if (deathAnimCounter > 0 && sunk)
        {
            //Debug.Log("Minus");
            //Debug.Log(deathAnimCounter);
            deathAnimCounter -= Time.deltaTime;

        }
        if (deathAnimCounter <= 0 && sunk)
        {
            if(isP1)
            {
                Debug.Log("P1 END");
                PlayerPrefs.SetInt("p1hit", hitShots);
                PlayerPrefs.SetInt("p1miss", missShots);
                PlayerPrefs.SetInt("p1ic", initCrew);
                PlayerPrefs.SetInt("p1ec", endCrew);
                PlayerPrefs.SetInt("p2hit", GameObject.Find("Pirate").GetComponent<Movement>().hitShots);
                PlayerPrefs.SetInt("p2miss", GameObject.Find("Pirate").GetComponent<Movement>().missShots);
                PlayerPrefs.SetInt("p2ic", GameObject.Find("Pirate").GetComponent<Movement>().initCrew);
                PlayerPrefs.SetInt("p2ec", GameObject.Find("Pirate").GetComponent<Movement>().endCrew);
                if (health <= 0)
                {
                    PlayerPrefs.SetInt("winner", 1);
                }
            }
            else
            {
                Debug.Log("P2 END");
                PlayerPrefs.SetInt("p2hit", hitShots);
                PlayerPrefs.SetInt("p2miss", missShots);
                PlayerPrefs.SetInt("p2ic", initCrew);
                PlayerPrefs.SetInt("p2ec", endCrew);
                PlayerPrefs.SetInt("p1hit", GameObject.Find("Ship").GetComponent<Movement>().hitShots);
                PlayerPrefs.SetInt("p1miss", GameObject.Find("Ship").GetComponent<Movement>().missShots);
                PlayerPrefs.SetInt("p1ic", GameObject.Find("Ship").GetComponent<Movement>().initCrew);
                PlayerPrefs.SetInt("p1ec", GameObject.Find("Ship").GetComponent<Movement>().endCrew);
                if (health <= 0)
                {
                    PlayerPrefs.SetInt("winner", 0);
                }
            }
            Application.LoadLevel("GameOver");
        }
        //if (gameOver && deathCounter>0 && sunk)
        //{
        //    Debug.Log("Minus");
        //    Debug.Log(deathCounter);
        //    deathCounter -= Time.deltaTime;

        //}
        //if(gameOver && deathCounter <= 0 && sunk)
        //{
        //    Debug.Log("Next");
        //    Application.LoadLevel("GameOver");
        //}
    }

    void PlayExplosionSound()
    {
        GetComponent<AudioSource>().Play();
    }

    //void End()
    //{
    //    if(isP1)
    //    {
    //        if(health == 0)
    //        {
    //            GameObject.Find("Values").GetComponent<ValueRetainer>().endVal(0f, 0f, true, false);
    //        }
    //        else
    //        {
    //            GameObject.Find("Values").GetComponent<ValueRetainer>().endVal(0f, 0f, true, true);
    //        }
    //        //GameObject.Find("Pirate").GetComponent<Movement>().End();
    //    } 
    //    else
    //    {
    //        if (health == 0)
    //        {
    //            GameObject.Find("Values").GetComponent<ValueRetainer>().endVal(0f, 0f, false, false);
    //        }
    //        else
    //        {
    //            GameObject.Find("Values").GetComponent<ValueRetainer>().endVal(0f, 0f, false, true);
    //        }
    //        //GameObject.Find("Ship").GetComponent<Movement>().End();
    //    }
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cannon Balls") && other.gameObject.GetComponent<CannonBalls>().isP1 != this.isP1 && other.GetComponent<CannonBalls>().recycled == false)
        {
            //Debug.Log("Triggered");
            hit = true;
            health -= damage;
            if(crew > 0) crew -= (Random.value)*0.1f;
            if(speed > 0) speed -= (Random.value)*0.1f;
        }
        else
        {
            hit = false;
        }
    }
}
