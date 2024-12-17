using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    public NavMeshSurface surface;

    void Start()
    {

    }

  

    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }
}
