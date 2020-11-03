using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour {
    Material mat;
    public float speed;
	void Start () {
        mat = this.GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        mat.mainTextureOffset = new Vector2(mat.mainTextureOffset.x, (mat.mainTextureOffset.y + Time.deltaTime * speed) % 1f);
	}
}
