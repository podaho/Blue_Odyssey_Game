using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonBalls : MonoBehaviour {

    public Vector3 origin;
    public Vector3 vel;
    public float range = 1.5f;
    public float speed;
    public Vector3 scatter;
    public bool isP1;
    public bool hit = false;
    public bool recycled = false;
    public AudioClip splashy;

    private Animator animator;
    private float destroyCounter = 0;
    private ObjectPool pool;
    private float timeCount;
    private GameObject ship;


    // Use this for initialization
    void Start () {
        origin = transform.position;
        animator = GetComponent<Animator>();
        scatter = new Vector3(vel.x + Random.value * .25f - .125f, vel.y + Random.value * .25f - .125f, vel.z);
        pool = GameObject.Find("CannonObjPool").GetComponent<ObjectPool>();
        range += Random.value * .25f;
        timeCount = 0f;
        if(isP1)
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
        if (this.gameObject.activeSelf == true)
        {
            transform.position += scatter * speed * Time.deltaTime;
            GetComponent<TrailRenderer>().time = 0.1f;
            if (hit == true)
            {
                //Debug.Log("Hit");
                speed = 0f;
                animator.SetBool("Reset", false);
                animator.SetBool("Hit", true);
                destroyCounter += Time.deltaTime;
            }
            else if (Vector3.Distance(origin, transform.position) > range)
            {
                //Debug.Log("Origin = (" + origin.x + ", " + origin.y + ", " + origin.z + "), Position = (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + "), Distance = " + Vector3.Distance(origin, transform.position) + ", Range = " + range);
                speed = 0f;
                animator.SetBool("Reset", false);
                animator.SetBool("Splashed", true);
                destroyCounter += Time.deltaTime;
                ship.GetComponent<Movement>().missShots += 1;
            }
            if (destroyCounter >= 2f)
            {
                //Debug.Log("Out of Range");
                destroyCounter = 0;
                recycled = true;
                GetComponent<TrailRenderer>().time = 0f;
                pool.Destroy(this.gameObject);
            }
            timeCount += Time.deltaTime;
        }
	}

    public void recycling()
    {
        if(recycled)
        {
            transform.position = origin;
            scatter.x = vel.x + Random.value * .25f - .125f;
            scatter.y = vel.y + Random.value * .25f - .125f;
            scatter.z = vel.z;
            //range = 1.5f + Random.value * .25f;
            recycled = false;
            hit = false;
            timeCount = 0f;
        }
    }

    void PlaySplashSound()
    {
        AudioSource.PlayClipAtPoint(splashy, transform.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ship") && other.gameObject.GetComponent<Movement>().isP1 != this.isP1 && this.gameObject.activeSelf == true && timeCount > 0.1f)
        {
            Debug.Log("Hit");
            ship.GetComponent<Movement>().hitShots += 1;
            hit = true;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Land")) //DOESN'T WORK
        {
            //Debug.Log("Triggered 2");
            hit = true;
        }
        else
        {
            hit = false;
        }
    }
}
