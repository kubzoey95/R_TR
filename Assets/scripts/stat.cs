using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stat : MonoBehaviour {

    private int level = 0;

    public void SaveStats()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }

    public void PlusOne()
    {
        level += 1;
    }

    public void EnableLevel()
    {
        GameObject.Find("Level " + level.ToString()).SetActive(true);
    }

    // Use this for initialization
    void Start () {
        level = PlayerPrefs.GetInt("Level", 0);
	}
	
	// Update is called once per frame
	void Update () {

    }
}
