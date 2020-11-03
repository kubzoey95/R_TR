using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateSliderValue : MonoBehaviour {

    public GameObject controller;
    private rotor rot;
    private Slider slid;
	void Start () {
        rot = controller.GetComponent<rotor>();
        slid = this.GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        slid.value = (rot.power / rot.maxPower) * slid.maxValue;
	}
}
