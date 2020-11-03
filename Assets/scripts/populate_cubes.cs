using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class populate_cubes : MonoBehaviour
{

    public GameObject[] objs;
    public GameObject around;
    public List<GameObject> instantiated = new List<GameObject>();
    public float max_range = 20f;
    public float min_range = 5f;
    private float nextActionTime = 0f;
    private Vector3 lastInstPos;
    private Quaternion lastInstRot;
    private GameObject lastInst;
    public float ResetDistance = 5f;
    private int ResetDistanceDiv;
    public int AsteroidsInterval = 5;
    public GameObject AsteroidObj = null;

    void Start()
    {
        GameObject obj = objs[0];
        Vector3 posVect = new Vector3(Random.Range(-max_range, max_range), Random.Range(min_range, max_range), 0f);
        GameObject inst = Instantiate(obj, around.transform.position + posVect, obj.transform.rotation);
        inst.transform.localScale = inst.transform.localScale * posVect.magnitude / 10f;
        instantiated.Add(inst);
        lastInstPos = inst.transform.position;
        lastInst = inst;
        ResetDistanceDiv = (int)(around.transform.position.y / ResetDistance);
        for (int i = 0; i < 24; i++)
        {
            addNew();
        }

        //this.GetComponent<loadMeshes>().objs.Add(inst);
    }

    void add(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject obj = objs[Random.Range(0, objs.Length)];
            Vector3 posVect = new Vector3(Random.Range(-max_range, max_range), Random.Range(min_range, max_range), 0f);
            CircleCollider2D collider;
            if (lastInst.TryGetComponent<CircleCollider2D>(out collider))
            {
                posVect.y += collider.bounds.extents.y * 2;
            }
            else
            {
                BoxCollider collider0;
                if (lastInst.TryGetComponent<BoxCollider>(out collider0))
                {
                    posVect.y += collider0.bounds.extents.y * 2;
                }
            }
            GameObject inst = Instantiate(obj, lastInstPos + posVect, Random.rotation);
            inst.transform.localScale = inst.transform.localScale * posVect.magnitude / 10f;
            instantiated.Add(inst);
            lastInst = inst;
            lastInstPos = inst.transform.position;
            //this.GetComponent<loadMeshes>().objs.Add(inst);
        }
    }

    void addNew()
    {
        if (instantiated.Count < 25)
        {
            GameObject obj = objs[Random.Range(0, objs.Length)];
            Vector3 posVect = new Vector3(Random.Range(-max_range, max_range), Random.Range(min_range, max_range), 0f) * Mathf.Min(around.GetComponent<rotor>().GetVelMagnitude() + 1, 2f);
            CircleCollider2D collider;
            if (lastInst != null && lastInst.TryGetComponent<CircleCollider2D>(out collider))
            {
                posVect.y += collider.bounds.extents.y * 2;
            }
            else
            {
                BoxCollider collider0;
                if (lastInst != null && lastInst.TryGetComponent<BoxCollider>(out collider0))
                {
                    posVect.y += collider0.bounds.extents.y * 2;
                }
            }
            GameObject inst = Instantiate(obj, lastInstPos + posVect, obj.transform.rotation);
            inst.transform.localScale = inst.transform.localScale * Mathf.Min(posVect.magnitude / 10f, 5f);
            instantiated.Add(inst);
            lastInst = inst;
            lastInstPos = inst.transform.position;
            //this.GetComponent<loadMeshes>().objs.Add(inst);
        }
    }

    bool findVisible(GameObject bj)
    {
        if (bj == null)
        {
            return false;
        }
        Vector3 screenCoor = Camera.main.WorldToScreenPoint(bj.transform.position);
        if (bj.transform.position.y < around.transform.position.y && !(screenCoor.x >= 0 && screenCoor.x <= Camera.main.pixelWidth && screenCoor.y >= 0 && screenCoor.y <= Camera.main.pixelHeight))
        {
            Destroy(bj);
            //this.GetComponent<loadMeshes>().objs.Remove(bj);
            return false;
        }
        return true;
    }

    float timeToDelete = 0;

    void deleteOld()
    {
        if (instantiated.Count > 24)
        {
            /*
            RaycastHit[] below = Physics.BoxCastAll(Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.WorldToScreenPoint(around.transform.position).z)), new Vector3(9999999999999999999, 0, 9999999999999999999), Physics.gravity);
            foreach (RaycastHit belowObj in below)
            {
                if (instantiated.Contains(belowObj.collider.gameObject))
                {
                    instantiated.Remove(belowObj.collider.gameObject);
                    Destroy(belowObj.collider.gameObject);
                }
            }
            */
            //Camera.main.ScreenToWorldPoint(new Vector3(0, -Camera.main.pixelHeight, 0))
            //Debug.Log(around.transform.position);

            GameObject first = instantiated.First();

            if (first != null && first.transform.position.y < around.transform.position.y - 40f)
            {
                instantiated = instantiated.FindAll(findVisible);
                //Debug.Log("deletes");
            }
        }

    }

    float AsteroidTime = 0f;

    // Update is called once per frame
    void Update()
    {
        AsteroidTime += Time.deltaTime;
        /*if (!(!around.GetComponent<rotor>().GetRotate() && around.GetComponent<rotor>().GetNoRotateTime() > 5))
        {
            AsteroidTime = 0f;
        }*/
        if (AsteroidObj != null && AsteroidTime > 2f && !around.GetComponent<rotor>().GetRotate() && around.GetComponent<rotor>().GetNoRotateTime() > 2 && Random.Range(around.GetComponent<rotor>().maxPower / 3f, around.GetComponent<rotor>().maxPower) < around.GetComponent<rotor>().power)
        {
            AsteroidTime = 0f;
            int randNum = (int)(Random.Range(0, 20) * around.GetComponent<rotor>().power / 2f);
            float AroundZ = Camera.main.WorldToScreenPoint(around.transform.position).z;
            for (int i=0;i<randNum; i++)
            {
                GameObject newAsteroid = Instantiate(AsteroidObj, Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(-Camera.main.pixelWidth / 2f, Camera.main.pixelWidth + Camera.main.pixelWidth / 2f), Camera.main.pixelHeight + Random.Range(0f, Camera.main.pixelHeight), AroundZ)), Random.rotation);
                newAsteroid.GetComponent<flyToelectron>().flyVect = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Camera.main.pixelWidth), 0, AroundZ)) - newAsteroid.transform.position;
                newAsteroid.GetComponent<flyToelectron>().speed = Random.Range(0, 100f);
                newAsteroid.transform.localScale *= Random.Range(0.1f, around.GetComponent<rotor>().GetVelMagnitude() / 50f);
                instantiated.Add(newAsteroid);
            }
            
        }
        if (ResetDistanceDiv < (int)(around.transform.position.y / ResetDistance))
        {
            ResetDistanceDiv = (int)(around.transform.position.y / ResetDistance);
            for (int i = 0; i < 25; i++)
            {
                addNew();
            }
            deleteOld();
        }
        //addNew();
    }
}
