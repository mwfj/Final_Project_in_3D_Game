using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Every Trash/Boss should inherit from this class 
**/
public abstract class EmeryBase : MonoBehaviour
{
    public bool isInAttackMode;
    public abstract void attack();

    public abstract void Initialize();
}
