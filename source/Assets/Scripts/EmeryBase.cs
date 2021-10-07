using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Every Trash/Boss should inherit from this class 
**/
public abstract class EmeryBase : MonoBehaviour
{
    public Vector3 move_direction;
    public float rotation_speed;
    public bool isInAttackMode;
    public abstract void attack();
    public abstract void init();
    public void RotateToTarget(GameObject target){
        move_direction = (target.transform.position - this.transform.position).normalized;
        Quaternion lookQuanternion = Quaternion.LookRotation(new Vector3(move_direction.x,0.0f,move_direction.z));
        transform.rotation = lookQuanternion;
    }
}
