using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    public GameObject prefab1;
    public GameObject prefab2 = null;

    private List<GameObject> poolInstances = new List<GameObject>();

    private GameObject CreateInstance(Vector3 pos)
    {
        if(prefab2 == null)
        {
            GameObject clone = Instantiate(prefab1, pos, Quaternion.identity) as GameObject;
            clone.transform.parent = transform;

            poolInstances.Add(clone);

            return clone;
        }
        else
        {
            if (Random.value > 0.5)
            {
                GameObject clone = Instantiate(prefab1, pos, Quaternion.identity) as GameObject;
                clone.transform.parent = transform;

                poolInstances.Add(clone);

                return clone;
            }
            else
            {
                GameObject clone = Instantiate(prefab2, pos, Quaternion.identity) as GameObject;
                clone.transform.parent = transform;

                poolInstances.Add(clone);

                return clone;
            }
        }
    }


    public GameObject NextObject(Vector3 pos, bool isp1)
    {
        foreach (GameObject item in poolInstances)
        {
            if(item.activeSelf == false)
            {
                if (prefab2 == null) {
                    item.GetComponent<CannonBalls>().isP1 = isp1;
                    item.GetComponent<CannonBalls>().origin = pos;
                }
                else
                {
                    item.GetComponent<ShrapScript>().origin = pos;
                }
                return item;
            }
        }
        return CreateInstance(pos);
    }

    public void Destroy(GameObject input)
    {
        if (poolInstances.Contains(input))
        {
            input.SetActive(false);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
