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

    private float rotation_speed;
    private Vector3 move_direction;
    public override void Initialize(){
        
    }
    public override void attack(){

    }

    // Start is called before the first frame update
    void Start()
    {
        // 
        target = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
        agent.isStopped = false;
        agent.updateRotation = false;
        trash_ani = GetComponent<Animator>();
        rotation_speed = 5.0f;
    }
    // Make the trash always look at the main character
    public void RotateToTarget(){
        move_direction = (target.transform.position - this.transform.position).normalized;
        Quaternion lookQuanternion = Quaternion.LookRotation(new Vector3(move_direction.x,0.0f,move_direction.z));
        transform.rotation = lookQuanternion;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position,target.transform.position);
        RotateToTarget();
        // transform.LookAt(target.transform);
        if(distance > agent.stoppingDistance){
            agent.destination = target.transform.position;
            agent.isStopped = false;
            trash_ani.SetBool("isAttacking", false);
        }else{
            agent.isStopped = true;
            trash_ani.SetBool("isAttacking", true);
        }
        // this.GetComponent<NavMeshAgent>().destination=target.transform.position;
        // this.GetComponent<NavMeshAgent>().isStopped = false;
    }

}
