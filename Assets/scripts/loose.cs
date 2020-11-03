using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loose : MonoBehaviour
{
    public float no_rotate_time;
    public float size;
    private bool lost;
    private bool crashLost = false;
    public bool canLoose = true;

    // Use this for initialization
    void Start()
    {
        lost = false;
    }

    public void setLost()
    {
        crashLost = true;
    }
    
    void Update()
    {
        if ((transform.localScale.x < size || gameObject.GetComponent<rotor>().GetNoRotateTime() > no_rotate_time || crashLost) && canLoose)
        {
            GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject obj in objs)
            {
                if (obj.name == "Lost" && obj.layer == 5 && !lost)
                {
                    Vector3 vel = gameObject.GetComponent<rotor>().GetVel();
                    gameObject.GetComponent<Rigidbody>().useGravity = true;
                    gameObject.GetComponent<rotor>().enabled = false;
                    gameObject.GetComponent<Rigidbody>().velocity = vel;
                    gameObject.GetComponent<TrailRenderer>().enabled = false;
                    obj.SetActive(true);
                    lost = true;
                }
            }
        }
    }
}
