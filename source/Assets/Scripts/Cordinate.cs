using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cordinate
{
    /**
     * * This struct is used to record the maze cell's relative cordinate
     * */
    public int x, z;
    public Cordinate(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public static Cordinate operator + (Cordinate a, Cordinate b)
    {
        a.x += b.x;
        a.z += b.z;
        return a;
    }
}