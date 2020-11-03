using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pixelScale : MonoBehaviour
{
    public Camera cam;
    void Start()
    {
        Vector3 origin = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 right = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, 0));
        Vector3 down = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, 0));
        transform.localScale = new Vector3((origin - right).magnitude, (origin - down).magnitude);
    }
}
