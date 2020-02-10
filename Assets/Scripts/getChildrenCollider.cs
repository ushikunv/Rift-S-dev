using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getChildrenCollider : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        MeshFilter[] childrenMeshFilter = GetComponentsInChildren<MeshFilter>();
        foreach(MeshFilter meshFilter in childrenMeshFilter){
            MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh=meshFilter.mesh;
            meshCollider.convex=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
