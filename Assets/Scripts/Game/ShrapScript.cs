using UnityEngine;
using System.Collections;

public class ShrapScript : MonoBehaviour {
    public Vector3 origin;
    public float life = 1f;
    public float speed = 0.5f;
    public Vector3 scatter;
    public bool recycled = false;
    public float counter;
    public float destroyCounter = 0;
    public bool frameskip = false;

    private Animator animator;
    private ObjectPool pool;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        scatter = new Vector3((Random.value*0.25f + .75f)*Mathf.Pow(-1,Mathf.Floor(Random.value+0.5f)), (Random.value * 0.5f + 1f) * Mathf.Pow(-1, Mathf.Floor(Random.value + 0.5f)), 0f);
        pool = GameObject.Find("ShrapnelObjPool").GetComponent<ObjectPool>();
        life += Random.value * .25f;
    }
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.activeSelf == true)
        {
            transform.position += scatter * speed * Time.deltaTime;
            if (counter > life)
            {
                speed = 0f;
                animator.SetBool("Reset", false);
                animator.SetBool("Shrap1Sink", true);
                animator.SetBool("Shrap2Sink", true);
                destroyCounter += Time.deltaTime;
            }
            counter += Time.deltaTime;
            if (destroyCounter >= 2f)
            {
                recycled = true;
                counter = 0f;
                destroyCounter = 0f;
                pool.Destroy(this.gameObject);
            }
        }
    }

    public void recycling()
    {
        if (recycled)
        {
            transform.position = origin;
            scatter.x = (Random.value * 0.25f + .75f) * Mathf.Pow(-1, Mathf.Floor(Random.value + 0.5f));
            scatter.y = (Random.value * 0.5f + 1f) * Mathf.Pow(-1, Mathf.Floor(Random.value + 0.5f));
            scatter.z = 0f;
            life = 1f + Random.value * .25f;
            recycled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            recycled = true;
            counter = 0f;
            destroyCounter = 0f;
            pool.Destroy(this.gameObject);
        }
    }
}
