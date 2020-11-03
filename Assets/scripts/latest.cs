using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class latest : MonoBehaviour {

    private void OnEnable()
    {
        gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = "LATEST (" + GameObject.Find("statistics").GetComponent<stat>().GetLevel() + ")";
    }
}
