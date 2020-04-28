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
    private Animator trash_ani;

    
    
    public override void attack(){

    }
     public override void init(){
        target = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        // agent.destination = target.transform.position;
        // agent.isStopped = false;
        // agent.updateRotation = false;
        trash_ani = GetComponent<Animator>();
        rotation_speed = 5.0f;
        isInAttackMode = false;

     }
    // Start is called before the first frame update
    void Start()
    {
        init();
    }
    // Make the trash always look at the main character
    

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(!agent.enabled){
            agent.enabled = true;
        }
        target = GameObject.FindGameObjectWithTag("Player");
        agent.destination = target.transform.position;
        agent.isStopped = false;
        if(target){
            // isRestart = false;
            float distance = Vector3.Distance(this.transform.position,target.transform.position);
            RotateToTarget(target);
            // transform.LookAt(target.transform);
            if(distance > agent.stoppingDistance){
                agent.destination = target.transform.position;
                agent.isStopped = false;
                trash_ani.SetBool("isAttacking", false);
            }else{
                agent.isStopped = true;
                trash_ani.SetBool("isAttacking", true);
            }
        }
        // this.GetComponent<NavMeshAgent>().destination=target.transform.position;
        // this.GetComponent<NavMeshAgent>().isStopped = false;
    }


}
