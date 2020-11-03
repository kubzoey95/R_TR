using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_move : MonoBehaviour {

    public GameObject[] moving_objects;
    public bool from_player_velocity;
    bool already_moving = false;
    rotor player;
    float[] objects_speed;
    int objects_size;
    private orbit orb;

	// Use this for initialization
	void Start () {
        objects_size = moving_objects.Length;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<rotor>();
        objects_speed = new float[objects_size];
        for(int i = 0; i < objects_size; i++)
        {
            objects_speed[i] = moving_objects[i].GetComponent<move_obstacle>().speed;
        }
        orb = gameObject.GetComponent<orbit>();
	}

    private float max_vel()
    {
        return (player.angle_speed / 360f) * (2f * Mathf.PI * orb.GetOrb());
    }
	
	// Update is called once per frame
	void Update () {
        if (already_moving)
        {
            if (from_player_velocity)
            {
                for (int i = 0; i < objects_size; i++)
                {
                    moving_objects[i].GetComponent<move_obstacle>().speed = objects_speed[i] * (player.GetVelMagnitude() / max_vel());
                }
            }
        }
        if (already_moving)
        {
            if (!gameObject.GetComponent<orbit>().GetOrbited())
            {
                already_moving = false;
                foreach (GameObject obj in moving_objects)
                {
                    obj.GetComponent<move_obstacle>().moves = false;
                }
            }
        }
        else
        {
            if (gameObject.GetComponent<orbit>().GetOrbited())
            {
                already_moving = true;
                foreach (GameObject obj in moving_objects)
                {
                    obj.GetComponent<move_obstacle>().moves = true;
                }
            }
        }
	}
}
