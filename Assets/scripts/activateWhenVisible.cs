using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateWhenVisible : MonoBehaviour
{

    private void Start()
    {
        if (GetComponent<MeshRenderer>().isVisible)
        {
            transform.parent.GetComponent<SphereCollider>().enabled = true;
            transform.parent.GetComponent<Light>().enabled = true;
            transform.parent.GetComponent<orbit>().enabled = true;

            GetComponent<BoundRadiusSetAndOther>().enabled = true;
            GetComponent<distort>().enabled = true;

            transform.parent.Find("asteroid").gameObject.SetActive(true);
        }
    }

    private void OnBecameVisible()
    {
        transform.parent.GetComponent<SphereCollider>().enabled = true;
        transform.parent.GetComponent<Light>().enabled = true;
        transform.parent.GetComponent<orbit>().enabled = true;

        GetComponent<BoundRadiusSetAndOther>().enabled = true;
        GetComponent<distort>().enabled = true;

        transform.parent.Find("asteroid").gameObject.SetActive(true);

    }
    private void OnBecameInvisible()
    {
        transform.parent.gameObject.GetComponent<SphereCollider>().enabled = false;
        transform.parent.gameObject.GetComponent<Light>().enabled = false;
        transform.parent.gameObject.GetComponent<orbit>().enabled = false;

        GetComponent<BoundRadiusSetAndOther>().enabled = false;
        GetComponent<distort>().enabled = false;

        transform.parent.Find("asteroid").gameObject.SetActive(false);
    }
}
