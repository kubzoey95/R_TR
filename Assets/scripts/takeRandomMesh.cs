using UnityEngine;

public class takeRandomMesh : MonoBehaviour
{
    public Vector3 RandSeed;
    void Start()
    {
        /*GameObject from = GameObject.Find("cube_spawner");
        this.GetComponent<MeshFilter>().mesh = from.GetComponent<loadMeshes>().meshes[Random.Range(0, from.GetComponent<loadMeshes>().meshes.Count)];
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        MeshCollider coll;
        if (this.TryGetComponent<MeshCollider>(out coll))
        {
            coll.sharedMesh = mesh;
        }
        */

        /*Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        MeshCollider coll;
        if (this.TryGetComponent<MeshCollider>(out coll))
        {
            coll.sharedMesh = mesh;
        }*/

        RandSeed = new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
    }

}
