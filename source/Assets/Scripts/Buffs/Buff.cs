using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BasicState { ALIVE, DIE };

public abstract class Buff :MonoBehaviour
{
    public string buffName;
    public float TTL;
    abstract public void Initialize();
    public Player playerInstance;
}
