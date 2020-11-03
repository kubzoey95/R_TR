using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour {

    private Camera cam;
    public float z_multiplier;
    public float min_z;
    private Vector3 temp;
    private float z = 0;
    private rotor ro;
    public float moveToNewRotSpeed = 10f;
    public float moveToBallSpeed = 1f;


    // Use this for initialization
    void Start () {
        ro = gameObject.GetComponent<rotor>();
        cam = Camera.main;
        cam.transform.position = transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), -10);
        cam.transform.RotateAround(transform.position, Vector3.forward, Random.Range(0, 360));
        cam.transform.rotation = Quaternion.identity;
    }

    public bool CameraFocused(float accuracy)
    {
        return Vector2.Distance(new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < accuracy;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        z = -ro.GetVelMagnitude() * z_multiplier - min_z;
        if (ro.GetRotate())
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(ro.GetRot().transform.position.x, ro.GetRot().transform.position.y, z), Time.deltaTime * moveToNewRotSpeed);
        }
        else
        {
            if (Vector3.Distance(new Vector3(cam.transform.position.x, cam.transform.position.y, 0), new Vector3(transform.position.x, transform.position.y, 0)) < 0.1)
            {
                cam.transform.position = new Vector3(transform.position.x, transform.position.y, z);
            }
            else
            {
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(transform.position.x, transform.position.y, z), (Vector3.Magnitude(gameObject.GetComponent<Rigidbody>().velocity) + 10) * Time.deltaTime * moveToBallSpeed);
            }
        }
    }
}
