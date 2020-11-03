using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivate_on_enabled : MonoBehaviour {

    public GameObject[] objs;

    private void OnEnable()
    {
        foreach(GameObject obj in objs)
        {
            obj.SetActive(false);
        }
    }
}
