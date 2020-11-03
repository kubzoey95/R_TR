using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBackground : MonoBehaviour {

    Vector2 lastPos;
    Material mat;
    public float speed;
    public Vector2 defaultMoveDir = new Vector2(0.2f,0.1f);
    public float defaultMoveCoef = 0f;
	void Start () {
        lastPos = new Vector2(this.transform.position.x, this.transform.position.y);
        mat = this.GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 currPos = new Vector2(this.transform.position.x, this.transform.position.y);
        mat.mainTextureOffset = mat.mainTextureOffset + (currPos - lastPos + defaultMoveDir.normalized * defaultMoveCoef) * speed;
        lastPos = currPos;
	}
}
