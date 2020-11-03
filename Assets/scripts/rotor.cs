using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class rotor : MonoBehaviour {

    public bool randomChord = false;
    private GameObject rot;
    private bool rotate = false;
    private int type = 0;
    private Vector3 trans;
    private Vector3 vel = Vector3.zero;
    private float distance = 0;
    private float min_dist;
    public float angle_speed;
    private float closest;
    private float plus_minus = 1;
    private Rigidbody rig;
    private Vector3 initial_pos;
    public float black_hole_loss;
    private bool cam_focused = false;
    private float no_rotate_time = 0;
    private float initial_light;
    private bool collides = false;
    float lastSpeed = 0;
    public float power = 0;
    public float maxPower = 0f;
    public float powerDenom = 100f;
    private float lastPow = 0f;
    public float powerDrainTime = 3f;
    private Vector2 lastRotPos;
    public GameObject scoreText = null;
    private float score;
    HashSet<GameObject> orbits = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rotor"))
        {
            orbits.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("rotor"))
        {
            orbits.Remove(other.gameObject);
        }
    }

    public float GetNoRotateTime()
    {
        return no_rotate_time;
    }
    public GameObject GetRot()
    {
        return rot;
    }

    public int GetTyp()
    {
        return type;
    }

    public int ChordType = 0;

    public void SetTyp(int i)
    {
        if (randomChord && i != -1)
        {
            if (i != type)
            {
                ChordType = UnityEngine.Random.Range(0, 4);
            }
        }
        else
        {
            ChordType = i;
        }
        type = i;
    }

    public float GetVelMagnitude()
    {
        if (rotate)
        {
            return Vector3.Magnitude(vel);
        }
        else
        {
            if (rig != null)
            {
                return Vector3.Magnitude(rig.velocity);
            }
            return 0;
        }
    }

    public Vector3 GetVel()
    {
        if (rotate)
        {
            return vel;
        }
        else
        {
            return rig.velocity;
        }
    }

    public bool GetRotate()
    {
        return rotate;
    }

    private void OnEnable()
    {
        Audio cam_audio =  Camera.main.GetComponent<Audio>();
        cam_audio.controler = gameObject;
        cam_audio.SetPlay(true);
    }


    private void Start()
    {
        initial_light = gameObject.GetComponent<Light>().intensity;
        rig = gameObject.GetComponent<Rigidbody>();
        trans = transform.position;
        lastRotPos = new Vector2(trans.x, trans.y);
        Camera.main.gameObject.GetComponent<Audio>().randomChord = randomChord;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collides = true;
        Camera.main.GetComponent<Audio>().PlayCollisionKick(Mathf.Min(Mathf.Max(GetVelMagnitude() / 10f, 0.1f), 1));
    }

    private void OnCollisionExit(Collision collision)
    {
        collides = false;
    }

    private void FixedUpdate()
    {
        
        gameObject.GetComponent<Light>().intensity = gameObject.GetComponent<Light>().intensity * (1 - Time.deltaTime) + initial_light * Mathf.Max(0, ((gameObject.GetComponent<loose>().no_rotate_time - no_rotate_time) / gameObject.GetComponent<loose>().no_rotate_time)) * Time.deltaTime;
        if (rotate)
        {
            power = power - lastPow * Time.deltaTime / powerDrainTime;
        }
        else
        {
            if (collides)
            {
                no_rotate_time += Time.deltaTime * 0.1f;
            }
            else
            {
                no_rotate_time += Time.deltaTime;
            }
        }
        if (!cam_focused)
        {
            cam_focused = GetComponent<move_camera>().CameraFocused(0.1f);
        }
        else { 
        if (Input.GetMouseButton(0))
        {
            if (!rotate)
            {
                            if (orbits.Count > 0)
                            {
                                lastSpeed = GetVelMagnitude();
                                GameObject obj = null;
                                foreach (GameObject bj in orbits)
                                {
                                    obj = bj;
                                    break;
                                }
                                try
                                {
                                    if (rot.Equals(obj))
                                    {
                                        rotate = true;
                                        obj.GetComponent<orbit>().SetOrbited(true);
                                        rig.useGravity = false;
                                        rig.velocity = Vector3.zero;
                                        min_dist = rot.transform.localScale.x * Mathf.Sqrt(2) / 2 + gameObject.transform.localScale.x;
                                        plus_minus = Mathf.Sign(Vector3.SignedAngle(rot.transform.InverseTransformPoint(trans), rot.transform.InverseTransformPoint(transform.position), Vector3.forward));
                                        SetTyp(rot.GetComponent<orbit>().type);
                                }
                                else
                                    {
                                        rot.GetComponent<orbit>().SetOrbited(false);
                                        rot = obj;
                                        rotate = true;
                                        obj.GetComponent<orbit>().SetOrbited(true);
                                        rig.useGravity = false;
                                        rig.velocity = Vector3.zero;
                                        min_dist = rot.transform.localScale.x * Mathf.Sqrt(2) / 2 + gameObject.transform.localScale.x;
                                        plus_minus = Mathf.Sign(Vector3.SignedAngle(rot.transform.InverseTransformPoint(trans), rot.transform.InverseTransformPoint(transform.position), Vector3.forward));
                                        SetTyp(rot.GetComponent<orbit>().type);
                                    }
                                }
                                catch
                                {
                                    //power = Mathf.Min(maxPower, power + (new Vector2(rot.transform.position.x, rot.transform.position.y) - new Vector2(obj.transform.position.x, obj.transform.position.y)).magnitude / powerDenom);
                                    rot = obj;
                                    rotate = true;
                                    obj.GetComponent<orbit>().SetOrbited(true);
                                    rig.useGravity = false;
                                    rig.velocity = Vector3.zero;
                                    min_dist = rot.transform.localScale.x * Mathf.Sqrt(2) / 2 + gameObject.transform.localScale.x;
                                    plus_minus = Mathf.Sign(Vector3.SignedAngle(rot.transform.InverseTransformPoint(trans), rot.transform.InverseTransformPoint(transform.position), Vector3.forward));
                                    SetTyp(rot.GetComponent<orbit>().type);
                                    
                            }
                            power = Mathf.Min(maxPower, power + (new Vector2(rot.transform.position.x, rot.transform.position.y) - lastRotPos).magnitude / powerDenom);
                            lastRotPos = new Vector2(rot.transform.position.x, rot.transform.position.y);
                            lastPow = power;
                        }
                        
                
                if (!rotate)
                    {
                        if (power > 0f)
                        {
                            Vector3 dragVect3 = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                            dragVect3.x /= Screen.width;
                            dragVect3.y /= Screen.height;
                            dragVect3.z = 0f;
                            rig.velocity += dragVect3 * 2;
                            power = Mathf.Max(0f, power - new Vector2(dragVect3.x, dragVect3.y).magnitude * Time.deltaTime);
                        }
                    }
            }
            else
            {
                    SetTyp(rot.GetComponent<orbit>().type);
                no_rotate_time = 0;
                float orbitDiam = rot.GetComponent<orbit>().GetOrb();
                transform.RotateAround(rot.transform.position, Vector3.forward, plus_minus * angle_speed * Time.deltaTime * Mathf.Max(1f, Mathf.Min(1.5f, power / 3f + 1f)));
                    switch (type)
                    {
                        case 0:
                            transform.position = Vector3.MoveTowards(transform.position, rot.transform.position + Vector3.Normalize(transform.position - rot.transform.position) * orbitDiam, Time.deltaTime);
                            break;
                        case 1:
                            transform.position = Vector3.MoveTowards(transform.position, rot.transform.position + Vector3.Normalize(transform.position - rot.transform.position) * min_dist, Time.deltaTime);
                            transform.localScale = transform.localScale - Vector3.one * (Time.deltaTime * black_hole_loss);
                            break;
                        case 4:
                            transform.position = Vector3.MoveTowards(transform.position, rot.transform.position + Vector3.Normalize(transform.position - rot.transform.position) * orbitDiam, Time.deltaTime);
                            Camera.main.transform.RotateAround(Camera.main.transform.position, Vector3.forward, plus_minus * Time.deltaTime * angle_speed * 0.1f);
                            Physics.gravity = -9.8f * Camera.main.transform.up;
                            break;
                        default:
                            transform.position = Vector3.MoveTowards(transform.position, rot.transform.position + Vector3.Normalize(transform.position - rot.transform.position) * min_dist, Time.deltaTime);
                            break;
                    }
                    vel = (transform.position - trans) / Time.deltaTime;
            }
        }
        else if (rotate)
        {
            rot.GetComponent<orbit>().SetOrbited(false);
            SetTyp(-1);
            rotate = false;
            rig.useGravity = true;
            rig.velocity = vel;
            if (scoreText != null)
            {
                score += (float)((Mathf.Exp(Mathf.Max(power / maxPower, 0)) - 1) * 1000*lastSpeed);
                scoreText.GetComponent<Text>().text = ((int)score).ToString();
            }
            }
        trans = transform.position;
    }
}
}
