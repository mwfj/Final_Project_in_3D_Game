using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAttack = false;
    private int combo = 0;
    public float coolDown= 0.2f;
    private float tempTime=0;
    public int damage = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (isAttack)
        {
            
            if (other.tag.Equals("Emery"))
            {
                if (Time.time-tempTime > coolDown || tempTime==0)
                {
                    other.GetComponent <CharacterState>().TakeDamage(damage);
                    tempTime = Time.time;
                }
            }
        }
    }
}
