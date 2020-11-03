using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distort : MonoBehaviour
{
    float pow = 0;
    float prob = 0.2f;
    float actionTime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actionTime > 3) {
            if (pow <= 0)
            {
                float rand = Random.Range(0f, 1000f);
                if (rand < 1000 * prob)
                {
                    pow = 10 * rand / 1000 / prob;
                }
            }
            actionTime = 0;
        }
        else
        {
            if (pow > 0)
            {
                //this.GetComponent<MeshRenderer>().material.SetFloat("DistortionPow", pow);
                pow -= Time.deltaTime;
            }
            actionTime += Time.deltaTime;
        }
        
    }
}
