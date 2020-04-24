using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This class is for Building the mob or trash
// All the mob/trash should contain this script
// Currently, this script only do one thing, which is create Navmesh Agent and follow the main character
public class Trash : EmeryBase
{
    private GameObject target;
    private NavMeshAgent agent;
    public override void Initialize(){
        
    }
    public override void attack(){

    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        // 
        target = GameObject.FindGameObjectWithTag("Player");
        this.GetComponent<NavMeshAgent>().destination=target.transform.position;
        this.GetComponent<NavMeshAgent>().isStopped = false;
  
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        this.GetComponent<NavMeshAgent>().destination=target.transform.position;
        this.GetComponent<NavMeshAgent>().isStopped = false;
    }

}
