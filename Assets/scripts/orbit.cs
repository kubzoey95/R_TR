using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour {

    //0 core 1 hole 2 finish 3 switch -7 wim -1 fall 4 gravity_switch
    private bool is_orbited = false;
    private float orb = 0;
    public int type;
    private bool already_orbited = false;
    public bool deactivates = false;

    public void SetOrbited(bool is_orbited)
    {
        this.is_orbited = is_orbited;
    }

    public bool GetOrbited()
    {
        return is_orbited;
    }

    public float GetOrb()
    {
        return orb;
    }

    public void SetOrb(float orb)
    {
        this.orb = orb;
    }

	// Use this for initialization
	void Start () {
        orb = Mathf.Max(transform.localScale.x, transform.localScale.y) * 4;
        //gameObject.GetComponent<Light>().range = orb;
    }

    private void Update()
    {
        if (already_orbited)
        {
            if (!is_orbited)
            {
                already_orbited = false;
                if (deactivates)
                {
                    //orb = 0;
                    GetComponent<SphereCollider>().radius = 0;
                    GetComponent<SphereCollider>().enabled = false;
                }
            }
        }
        else
        {
            if (is_orbited)
            {
                already_orbited = true;
            }
        }
            //gameObject.GetComponent<Light>().range = Vector2.MoveTowards(new Vector2(gameObject.GetComponent<Light>().range, 0), new Vector2(orb, 0), Time.deltaTime).x;
    }

}
