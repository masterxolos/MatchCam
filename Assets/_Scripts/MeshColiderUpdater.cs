using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColiderUpdater : MonoBehaviour
{
    SkinnedMeshRenderer skRenderer;
    MeshCollider msCol;
    Mesh bakeMesh;

    private void Start()
    {
        skRenderer = GetComponent<SkinnedMeshRenderer>();
        msCol = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        bakeMesh = new Mesh();
        skRenderer.BakeMesh(bakeMesh);

        msCol.sharedMesh = bakeMesh;
    }
}
