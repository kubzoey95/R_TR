using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crash : MonoBehaviour
{
    public GameObject populator = null;
    public GameObject toInst;
    private populate_cubes pop;
    private void Start()
    {
        pop = populator.GetComponent<populate_cubes>();
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "crasher")
        {
            this.GetComponent<rotor>().power = this.GetComponent<rotor>().power / 2;
            this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity / 2;
            if (populator != null)
            {
                GameObject obj = collision.gameObject;
                for (int i = 0; i < 10; i++)
                {
                    
                    GameObject inst = Instantiate(toInst, Random.insideUnitSphere * collision.bounds.extents.magnitude + obj.transform.position, Random.rotation);
                    inst.transform.localScale = obj.transform.localScale / 3;
                    inst.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                    inst.GetComponent<Rigidbody>().velocity += Random.rotation * (this.GetComponent<rotor>().GetVel() * Random.Range(0f, 1f) + new Vector3(3,3,3));
                    pop.instantiated.Add(inst);
                }
                Destroy(obj);
            }
        }
    }
}
