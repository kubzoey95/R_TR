using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_obstacle : MonoBehaviour {

    private Vector3 initial_position;
    public bool moves;
    public Vector3 axis;
    public float how_far;
    public float cycle_time;
    private float time;
    public float speed = 1;

    void Start() {
        initial_position = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (moves)
        {
            transform.position = initial_position + Vector3.Normalize(transform.InverseTransformDirection(axis)) * how_far * Mathf.Sin((2 * Mathf.PI / cycle_time) * time);
            time = (time + Time.deltaTime * speed) % cycle_time;
        }
    }
}
