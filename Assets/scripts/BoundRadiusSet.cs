using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundRadiusSet : MonoBehaviour
{
    public bool scaleIt = false;
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        if (scaleIt)
        {
            this.GetComponent<MeshRenderer>().material.SetFloat("BoundRadius", (Vector3.Scale(mesh.bounds.extents, this.transform.localScale)).magnitude);
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.SetFloat("BoundRadius", mesh.bounds.extents.magnitude);
        }
        this.GetComponent<MeshRenderer>().material.SetVector("BoundCenter", mesh.bounds.center);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
