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
    public void AddEnhacement(Buff _buff)
    {

    }
    public void AddEnfeeblement(Buff _buff)
    {

    }
    private void Start()
    {
        healthPoint = 100;
        basic = BasicState.ALIVE;
        Debug.Log(transform.gameObject.name + "Health point:\t" + healthPoint);
    }
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Alive", true);
    }
    private void Update()
    {
        // API to deal with the health point.
        if (healthPoint <= 0 )
        {
            basic = BasicState.DIE;
            animator.SetBool("Alive", false);
        }
    }

    public void TakeDamage(int damage)
    {

 
            healthPoint -= damage;
            Debug.Log(transform.gameObject.name + "\t" + healthPoint);
            animator.SetTrigger("GetHit");
    }

    public float getHealthRate()
    {
        return (float)healthPoint / 100f;
    }
}
