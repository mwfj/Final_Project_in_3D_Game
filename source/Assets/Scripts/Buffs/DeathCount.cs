using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCount :Buff
{
    // Start is called before the first frame update
    override public void Initialize()
    {
        this.TTL = 5;
    }
    public DeathCount()
    {
        this.TTL = 5;
    }
    void Start()
    {
        this.TTL = 5;
    }

    // Update is called once per frame
    void Update()
    {
        TTL -= Time.deltaTime;
        if (TTL < 0)
        {
            playerInstance.mCharacterState.basic = BasicState.DIE;
            Debug.Log("DeathCount");
            Destroy(this.gameObject);
        }
    }
}
