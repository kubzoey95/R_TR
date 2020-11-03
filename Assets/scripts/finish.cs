using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour {

    private rotor rot;
    private bool fin = false;
    private float t;
    public float cooldown;
    public float dist;
    public float range;

    private void Start()
    {
        rot = gameObject.GetComponent<rotor>();
    }

    // Update is called once per frame
    void Update () {
		if (rot.GetRotate())
        {
            GameObject r = rot.GetRot();
            if (r.name.Contains("finish"))
            {
                if (!fin)
                {
                    t = Time.time;
                    fin = true;
                }
                if (r.GetComponent<Light>().range < range)
                {
                    r.GetComponent<Light>().range += Time.deltaTime * 2;
                    r.GetComponent<orbit>().SetOrb(r.GetComponent<Light>().range);
                }
                if (Time.time - t > cooldown && Vector3.Distance(r.transform.position, transform.position) < dist && r.GetComponent<Light>().range > range)
                {
                    fin = false;
                    GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();
                    foreach (GameObject obj in objs)
                    {
                        if(obj.name == "Finish" && obj.layer == 5)
                        {
                            gameObject.GetComponent<rotor>().SetTyp(-7);
                            GameObject.FindWithTag("level");
                            enabled = false;
                            obj.SetActive(true);
                            gameObject.GetComponent<rotor>().enabled = false;
                            gameObject.GetComponent<TrailRenderer>().enabled = false;
                            stat st = GameObject.FindWithTag("stat").GetComponent<stat>();


                            if (st.GetLevel() <= LevelHandle.GetLoadedLevelId() && st.GetLevel() >= 0)
                            {
                                GameObject.FindWithTag("stat").GetComponent<stat>().PlusOne();
                                GameObject.FindWithTag("stat").GetComponent<stat>().SaveStats();
                            }
                            break;
                        }
                    }

                }
            }
            else
            {
                fin = false;
            }
        }
        else 
        {
            fin = false;
        }
	}
}
