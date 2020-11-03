using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateWhenVisible1 : MonoBehaviour
{

    private void Start()
    {
        if (GetComponent<MeshRenderer>().isVisible) {
            SphereCollider coll;
            if (TryGetComponent<SphereCollider>(out coll))
            {
                coll.enabled = true;
            }

            GetComponent<distort>().enabled = true;
            GetComponent<BoundRadiusSetAndOther>().enabled = true;
            GetComponent<rotateAroundCenter>().enabled = true;

            transform.parent.gameObject.GetComponent<move_obstacle>().enabled = true;
        }
    }

    private void OnBecameVisible()
    {
        SphereCollider coll;
        if (TryGetComponent<SphereCollider>(out coll))
        {
            coll.enabled = true;
        }

        GetComponent<distort>().enabled = true;
        GetComponent<BoundRadiusSetAndOther>().enabled = true;
        GetComponent<rotateAroundCenter>().enabled = true;

        transform.parent.gameObject.GetComponent<move_obstacle>().enabled = true;

    }
    private void OnBecameInvisible()
    {
        SphereCollider coll;
        if (TryGetComponent<SphereCollider>(out coll))
        {
            coll.enabled = false;
        }

        GetComponent<distort>().enabled = false;
        GetComponent<BoundRadiusSetAndOther>().enabled = false;
        GetComponent<rotateAroundCenter>().enabled = false;

        transform.parent.gameObject.GetComponent<move_obstacle>().enabled = false;
    }
}
