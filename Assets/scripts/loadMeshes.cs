using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class loadMeshes : MonoBehaviour
{
    public Mesh mesh;
    public Mesh haloMesh;

    //public HashSet<GameObject> objs = new HashSet<GameObject>();

    public Material mat = null;
    public Material haloMat = null;
    MaterialPropertyBlock propBlock;
    public GameObject electron;

    private void Start()
    {
        //propBlock = new MaterialPropertyBlock();
        
        /*for (int i = 0; i < meshes.Count; i++)
        {
            meshToObj.Add(new HashSet<GameObject>());
        }*/
    }

    /*void AddObj(GameObject obj)
    {
        meshToObj[Random.Range(0, meshToObj.Count)].Add(obj);
    }*/

    bool findVisible(GameObject bj)
    {
        if (bj == null)
        {
            return false;
        }
        return true;
    }
    GameObject obj;
    private void Update()
    {
        if (mat != null)
        {
            Transform asteroid;
            List<Matrix4x4> matrices = new List<Matrix4x4>();
            List<Matrix4x4> haloMatrices = new List<Matrix4x4>();

            float electronZ = Camera.main.WorldToScreenPoint(electron.transform.position).z;

            float cameraRectMagnitude = (Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, electronZ)) - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, electronZ))).magnitude;

            
            foreach (Collider collider in Physics.OverlapSphere(electron.transform.position, cameraRectMagnitude / 2))
            {
                obj = collider.gameObject;
                asteroid = obj.transform.Find("asteroid");
                if (asteroid != null)
                {
                    matrices.Add(asteroid.localToWorldMatrix);
                    asteroid = obj.transform.Find("torus");
                    if(asteroid != null)
                    {
                        haloMatrices.Add(asteroid.transform.localToWorldMatrix);
                    }
                        
                }
            }
            Graphics.DrawMeshInstanced(mesh, 0, mat, matrices);
            Graphics.DrawMeshInstanced(haloMesh, 0, haloMat, haloMatrices);
        }
    }
}
