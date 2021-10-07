using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : EmeryBase
{
    private GameObject target;
    private NavMeshAgent bossAgent;
    private Animator boss_ani;
    AnimatorStateInfo stateInfo;
    // Start is called before the first frame update
    void Start()
    {
        bossAgent = this.GetComponent<NavMeshAgent>();
        //Disable navmesh agent at first to make sure boss generate in the right position
        bossAgent.enabled = false;
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if(!boss_ani.GetBool("Alive")){
            
        }
        // Debug.LogWarning(transform.position);
        // Debug.LogWarning("Boss: "+bossAgent.enabled);
        stateInfo = boss_ani.GetCurrentAnimatorStateInfo(0);
        // if(stateInfo.IsName("roar")){
        //     boss_ani = this.GetComponent<Animator>();
        //     bossAgent.enabled = true;
        //     boss_ani.Play("walk_RM");
        // }
        if(stateInfo.normalizedTime>1.0f && !bossAgent.enabled){
            boss_ani = this.GetComponent<Animator>();
            bossAgent.enabled = true;
            // boss_ani.Play("walk_RM");
            // boss_ani.SetBool("isRun",true);
            // boss_ani.Play("Run");
        }
        if(bossAgent.enabled){
            // boss_ani.Play("Run",0);
            target = GameObject.FindGameObjectWithTag("Player");
            bossAgent.destination = target.transform.position;
        }
        if(target && bossAgent.enabled){
            // Debug.LogWarning( boss_ani.GetBool("isAttacking") );
            // isRestart = false;
            float distance = Vector3.Distance(this.transform.position,target.transform.position);
            RotateToTarget(target);
            // transform.LookAt(target.transform);
            if(distance > bossAgent.stoppingDistance){
                bossAgent.destination = target.transform.position;
                bossAgent.isStopped = false;
                boss_ani.SetBool("isAttacking", false);
            }else{
                bossAgent.isStopped = true;
                boss_ani.SetBool("isAttacking", true);
            }
        }
    }

    public override void attack(){

    }
    public override void init(){
        target = GameObject.FindGameObjectWithTag("Player");
        
        boss_ani = GetComponent<Animator>();
        // boss_ani.SetBool("isAttacking",false);
        // boss_ani.SetBool("isRun",false);
        rotation_speed = 5.0f;
        isInAttackMode = false;
        // Debug.LogWarning( transform.position);
        // bossAgent.enabled = false;
    }
}
