using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populate_cubes1 : MonoBehaviour {

    public GameObject obj;
    public GameObject around;
    public List<GameObject> instantiated = new List<GameObject>();
    public float max_range = 20f;
    public float min_range = 5f;
    private float nextActionTime = 0f;

    void Start () {
        if (instantiated.Count < 30)
        {
            Vector3 posVect = new Vector3(Random.Range(-max_range, max_range), Random.Range(min_range, max_range), 0f);
            if (instantiated.Count == 0)
            {
                GameObject inst = Instantiate(obj, around.transform.position + posVect, obj.transform.rotation);
                inst.transform.localScale = inst.transform.localScale * posVect.magnitude / 10f;
                instantiated.Add(inst);
            }
            else
            {
                GameObject inst = Instantiate(obj, instantiated[instantiated.Count - 1].transform.position + posVect, obj.transform.rotation);
                inst.transform.localScale = inst.transform.localScale * posVect.magnitude / 10f;
                instantiated.Add(inst);
            }

        }
    }

    void addNew()
    {
        if (instantiated.Count < 30)
        {
            Vector3 posVect = new Vector3(Random.Range(-max_range, max_range), Random.Range(min_range, max_range), 0f);
            if (instantiated.Count == 0)
            {
                GameObject inst = Instantiate(obj, around.transform.position + posVect, obj.transform.rotation);
                inst.transform.localScale = inst.transform.localScale * posVect.magnitude / 10f;
                instantiated.Add(inst);
            }
            else
            {
                GameObject inst = Instantiate(obj, instantiated[instantiated.Count - 1].transform.position + posVect, obj.transform.rotation);
                inst.transform.localScale = inst.transform.localScale * posVect.magnitude / 10f;
                instantiated.Add(inst);
            }
            
        }
    }

    bool findVisible(GameObject bj)
    {
        MeshRenderer msh = new MeshRenderer();
        bool succ = false;
        succ = bj.TryGetComponent<MeshRenderer>(out msh);
        if (!succ)
        {
            msh = bj.transform.Find("asteroid").GetComponent<MeshRenderer>();
        }
        if (!msh.isVisible && bj.transform.position.y < around.transform.position.y - 20f) {
            Destroy(bj);
            return false;
        }
        return true;
    }

    void deleteOld()
    {
        if (instantiated.Count > 10)
        {
            instantiated = instantiated.FindAll(findVisible);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
    }
}
