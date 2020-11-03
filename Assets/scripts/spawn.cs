using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject obj = GameObject.FindWithTag("spawn").transform.parent.gameObject;
        Debug.Log(obj.name);
        Vector3 this_bounds = gameObject.GetComponent<Collider>().bounds.extents;
        Vector3 spawn_bounds = obj.GetComponent<Collider>().bounds.extents;
        Vector3 spawn_pos = obj.GetComponent<Transform>().position;
        transform.position = obj.transform.position + Vector3.up * (Mathf.Max(this_bounds.x, this_bounds.y) + Mathf.Max(spawn_bounds.x, spawn_bounds.y));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
