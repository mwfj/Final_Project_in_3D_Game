using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState :MonoBehaviour
{
    // This is a class to define the enhancement and enfeeblement state of charater. 
    // Their transite functions
    // Basic states
    private int healthPoint; // can not be changed directly
    public Hashtable enhancements; //buff 
    public Hashtable enfeeblements;//debuff
    public BasicState basic;
    public Player playerInstance;

    public CharacterState(Player player) // constructor, initialize the basic value of character stat.
    {
        healthPoint = 100;
        basic = BasicState.ALIVE;
        playerInstance = player;
    }

    public void AddEnhacement(Buff _buff)
    {
        string buffName = _buff.buffName;
        if (enhancements.ContainsKey(buffName)) // If buff exists, refresh the local buff
        {
            Buff localBuff = (Buff)enhancements[buffName];
        }
        enhancements.Add(buffName,_buff);
    }
    public void AddEnfeeblement(Buff _buff)
    {
        string buffName = _buff.buffName;
        if (enfeeblements.ContainsKey(buffName)) // If buff exists, refresh the local buff
        {
            Buff localBuff = (Buff)enfeeblements[buffName];
        }
        Debug.Log("ADD debuff\n");
        enfeeblements.Add(buffName, _buff);
        _buff.gameObject.transform.parent = this.transform;
        _buff.playerInstance = this.playerInstance;
    }

    public void TakeDamage(int damage)
    {
        // API to deal with the health point.
        if (healthPoint < damage)
        {
            basic = BasicState.DIE;
        }
        else
        {
            healthPoint -= damage;
        }
    }
}
