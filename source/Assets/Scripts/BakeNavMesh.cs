
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This class main build for dynamically baking mesh in the current game map
// The mesh is based on Maze prefab
public class BakeNavMesh : MonoBehaviour
{

    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Start()
    {
       
        // surface.BuildNavMesh();
    }
    // Call this function when initializing the NavMeshBaker prefab
    public void Initialize()
    {

    }
    private void OnEnable()
    {
        // surface = new GameObject().AddComponent<NavMeshSurface>();
        surface = GetComponent<NavMeshSurface>();
        // Debug.LogWarning("+++++++++++++++++++++++++++++++++++++++");
        // Debug.LogWarning("Surface Created");
        // Debug.LogWarning("+++++++++++++++++++++++++++++++++++++++");
        surface.BuildNavMesh();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        surface.BuildNavMesh();
    }
}

