using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyCrasher : MonoBehaviour
{
    public bool destroy = false;
    public float timeToDestroy = 0.2f;

    // Update is called once per frame
    void Update()
    {
        
        if (destroy)
        {
            Destroy(this);
            Debug.Log("destroys");
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy < 0)
            {
                Debug.Log("destroys");
                Destroy(this);
            }
        }
    }
}
