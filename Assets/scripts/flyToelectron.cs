using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyToelectron : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 flyVect;
    public float speed = 1;

    private void Start()
    {
        flyVect = Vector3.Normalize(flyVect) * speed;
    }
    void Update()
    {
        transform.position += flyVect * Time.deltaTime;
    }
}
