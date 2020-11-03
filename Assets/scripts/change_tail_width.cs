using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_tail_width : MonoBehaviour {

    private TrailRenderer trail;

	// Use this for initialization
	void Start () {
        trail = gameObject.GetComponent<TrailRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        trail.widthMultiplier = transform.localScale.x;
	}
}
