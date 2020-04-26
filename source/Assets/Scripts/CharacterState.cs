using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState :MonoBehaviour
{
    // This is a class to define the enhancement and enfeeblement state of charater. 
    // Their transite functions
    // Basic states
    private int healthPoint; // can not be changed directly
    private Hashtable enhancements; //buff 
    private Hashtable enfeeblements;//debuff
    private BasicState basic;
    private Animator animator;

    public CharacterState(Player player) // constructor, initialize the basic value of character stat.
    {
        healthPoint = 100;
        basic = BasicState.ALIVE;
    }

    public void AddEnhacement(Buff _buff)
    {

    }
    public void AddEnfeeblement(Buff _buff)
    {

    }
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Alive", true);
    }

    public void TakeDamage(int damage)
    {
        // API to deal with the health point.
        if (healthPoint < damage)
        {
            basic = BasicState.DIE;
            animator.SetBool("Alive", false);
        }
        else
        {
            healthPoint -= damage;
        }
    }
}
