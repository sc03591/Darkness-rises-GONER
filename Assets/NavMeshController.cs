using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    public NavMeshSurface surface;

    void Start()
    {
        StartCoroutine ("Bake");
    }
    IEnumerator Bake() 
    {
        BuildNavMesh();
            yield return new WaitForSeconds(1f);
    }
  

    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }


}

