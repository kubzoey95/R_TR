using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updatePower : MonoBehaviour
{
    public GameObject controller;
    private rotor rot;
    private Text pow;
    public int maxSlashes = 15;
    void Start()
    {
        rot = controller.GetComponent<rotor>();
        pow = this.GetComponent<Text>();
    }
    void OnGUI()
    {
        pow.text = "POWER:" + new System.String('\\', Mathf.CeilToInt((rot.power / rot.maxPower) * maxSlashes));
    }
}
