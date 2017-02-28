using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour {
    public GameObject cannonBall;
    public float delayTime = 6f;
    public bool left = true;
    public bool leftFire = false;
    public bool rightFire = false;
    public float fireSpeed = 1f;
    public bool isP1;
    public bool main;

    private Vector3 spawnPos;
    private Animator animator;
    private GameObject gun;
    private float delay = 0f;
    private ObjectPool pool;
    private GameObject ball1;
    private GameObject leftCannonChargeBar;
    private GameObject rightCannonChargeBar;
    private GameObject ship;
    private float leftFireAxis;
    private float rightFireAxis;
    //private GameObject options;
    private float crew;
    private int arm;


    // Use this for initialization
    void Start () {
        //options = GameObject.Find("Values");
        cannonBall = Resources.Load<GameObject>("Prefabs/Cannon Ball");
        animator = GetComponent<Animator>();
        isP1 = transform.parent.gameObject.GetComponent<Movement>().isP1;
        pool = GameObject.Find("CannonObjPool").GetComponent<ObjectPool>();
        crew = this.gameObject.GetComponentInParent<Movement>().crew;
        if (isP1)
        {
            arm = PlayerPrefs.GetInt("armp1");
            fireSpeed += arm * 0.1f;
            delayTime = 6 * 50 / (50 + crew);
            if (left)
            {
                leftCannonChargeBar = GameObject.Find("P1 Canvas/Cannon Charge Left/Cannon Load Bar");
                leftCannonChargeBar.GetComponent<BarScale>().startVal = delayTime;
            }
            else
            {
                rightCannonChargeBar = GameObject.Find("P1 Canvas/Cannon Charge Right/Cannon Load Bar");
                rightCannonChargeBar.GetComponent<BarScale>().startVal = delayTime;
            }
            ship = GameObject.Find("Ship");
        }
        else
        {
            arm = PlayerPrefs.GetInt("armp2");
            fireSpeed += arm * 0.1f;
            delayTime = 6 * 50 / (50 + crew);
            if (left)
            {
                leftCannonChargeBar = GameObject.Find("P2 Canvas/Cannon Charge Left/Cannon Load Bar");
                leftCannonChargeBar.GetComponent<BarScale>().startVal = delayTime;
            }
            else
            {
                rightCannonChargeBar = GameObject.Find("P2 Canvas/Cannon Charge Right/Cannon Load Bar");
                rightCannonChargeBar.GetComponent<BarScale>().startVal = delayTime;
            }
            ship = GameObject.Find("Pirate");
        }
    }

    // Update is called once per frame
    void Update() {
        crew = this.gameObject.GetComponentInParent<Movement>().crew;
        delayTime = 6 * 50 / (50+crew);
        if(ship.GetComponent<Movement>().health > 0)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            if (isP1)
            {
                leftFireAxis = Input.GetAxis("FireLeft");
                rightFireAxis = Input.GetAxis("FireRight");
            }
            else
            {
                leftFireAxis = Input.GetAxis("FireLeft 2");
                rightFireAxis = Input.GetAxis("FireRight 2");
            }
            if (leftFireAxis == 1 && left)
            {
                if (delay <= 0)
                {
                    //Debug.Log("Fire");
                    spawnPos = new Vector3(transform.position.x - 0.04f, transform.position.y, transform.position.z);
                    ball1 = pool.NextObject(spawnPos, this.isP1);
                    ball1.GetComponent<CannonBalls>().isP1 = this.isP1;
                    if (transform.parent.gameObject.GetComponent<Movement>().inputVelocity > 0)
                    {
                        ball1.GetComponent<CannonBalls>().vel = -transform.right + transform.up * transform.parent.gameObject.GetComponent<Movement>().inputVelocity * 0.5f;
                    }
                    else
                    {
                        ball1.GetComponent<CannonBalls>().vel = -transform.right;
                    }
                    ball1.GetComponent<CannonBalls>().speed = fireSpeed;
                    ball1.GetComponent<CannonBalls>().range = 1.5f + 0.1f * arm;
                    ball1.GetComponent<CannonBalls>().hit = false;
                    if (ball1.GetComponent<Animator>())
                    {
                        ball1.GetComponent<Animator>().SetBool("Reset", true);
                        ball1.GetComponent<Animator>().SetBool("Splashed", false);
                    }
                    ball1.GetComponent<CannonBalls>().recycling();
                    ball1.SetActive(true);
                    leftFire = true;
                    delay += delayTime;
                    ship.GetComponent<Movement>().ammo -= 1;
                }
                else
                {
                    leftFire = false;
                }
            }

            if (rightFireAxis == 1 && !left)
            {
                if (delay <= 0)
                {
                    spawnPos = new Vector3(transform.position.x + 0.04f, transform.position.y, transform.position.z);
                    ball1 = pool.NextObject(spawnPos, this.isP1);
                    ball1.GetComponent<CannonBalls>().isP1 = this.isP1;
                    if (transform.parent.gameObject.GetComponent<Movement>().inputVelocity > 0)
                    {
                        ball1.GetComponent<CannonBalls>().vel = transform.right + transform.up * transform.parent.gameObject.GetComponent<Movement>().inputVelocity * 0.5f;
                    }
                    else
                    {
                        ball1.GetComponent<CannonBalls>().vel = transform.right;
                    }
                    ball1.GetComponent<CannonBalls>().speed = fireSpeed;
                    ball1.GetComponent<CannonBalls>().range = 1.5f + 0.1f * arm;
                    ball1.GetComponent<CannonBalls>().hit = false;
                    if (ball1.GetComponent<Animator>())
                    {
                        ball1.GetComponent<Animator>().SetBool("Reset", true);
                        ball1.GetComponent<Animator>().SetBool("Splashed", false);
                    }
                    ball1.GetComponent<CannonBalls>().recycling();
                    ball1.SetActive(true);
                    rightFire = true;
                    delay += delayTime;
                    ship.GetComponent<Movement>().ammo -= 1;
                }
                else
                {
                    rightFire = false;
                }
            }

            if (left)
            {
                animator.SetBool("leftGunFire", leftFire);
                leftCannonChargeBar.GetComponent<BarScale>().showBar(delayTime - delay);
                if (isP1)
                {
                    if (delay <= 0)
                    {
                        GameObject.Find("P1 Canvas/LGunTxt").GetComponent<Text>().text = "Ready";
                    }
                    else
                    {
                        GameObject.Find("P1 Canvas/LGunTxt").GetComponent<Text>().text = "";
                    }
                }
                else
                {
                    if (delay <= 0)
                    {
                        GameObject.Find("P2 Canvas/LGunTxt").GetComponent<Text>().text = "Ready";
                    }
                    else
                    {
                        GameObject.Find("P2 Canvas/LGunTxt").GetComponent<Text>().text = "";
                    }
                }
                leftCannonChargeBar.GetComponent<BarScale>().startVal = delayTime;
            }
            else
            {
                animator.SetBool("rightGunFire", rightFire);
                rightCannonChargeBar.GetComponent<BarScale>().showBar(delayTime - delay);
                if (isP1)
                {
                    if (delay <= 0)
                    {
                        GameObject.Find("P1 Canvas/RGunTxt").GetComponent<Text>().text = "Ready";
                    }
                    else
                    {
                        GameObject.Find("P1 Canvas/RGunTxt").GetComponent<Text>().text = "";
                    }
                }
                else
                {
                    if (delay <= 0)
                    {
                        GameObject.Find("P2 Canvas/RGunTxt").GetComponent<Text>().text = "Ready";
                    }
                    else
                    {
                        GameObject.Find("P2 Canvas/RGunTxt").GetComponent<Text>().text = "";
                    }
                }
                rightCannonChargeBar.GetComponent<BarScale>().startVal = delayTime;
            }

            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
        }
        else
        {
            //Debug.Log("fucked");
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
