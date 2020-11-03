using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAroundCenter : MonoBehaviour
{
    public float rotationSpeedMax = 0f;
    public float rotationSpeedMin = 0f;
    private Vector3 axis;
    public float variation = 0f;
    private float rotationSpeed;
    private void Start()
    {
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
        axis = Vector3.Normalize(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)));
    }

    // Update is called once per frame
    void Update()
    {
        axis = Vector3.Normalize(axis + Vector3.Normalize(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1))) * Time.deltaTime * variation);
        transform.RotateAround(transform.position, axis, Time.deltaTime * rotationSpeed);
    }
}
