using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MazeDirection
{
    North, East, South, West
}
public static class MazeDirections
{

    public static MazeDirection RandomDirection
    {
        get
        {
            return (MazeDirection)Random.Range(0, 4); //generate random direction
        }
    }
    private static Cordinate[] vectors =
    {
        //Using vectors to express the diretion is easy to calculate
        new Cordinate(0,1),//north
        new Cordinate(1,0),//east
        new Cordinate(0,-1),//south
        new Cordinate(-1,0)//west
    };
    private static MazeDirection[] opposites = {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
    };

    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return opposites[(int)direction];
    }
    public static Cordinate RandomVectorDirection
    {
        get
        {
            return vectors[(int)RandomDirection];  //generate random direction in vector form.
        }
    }
    public static Cordinate toCordinate(this MazeDirection direction)
    {
        return vectors[(int)direction];
    }
    public static MazeDirection Inverse(this MazeDirection direction)
    {
        return (MazeDirection)(((int)direction + 2) % 5);
    }
    private static Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,90f,0f),
        Quaternion.Euler(0f,180f,0f),
        Quaternion.Euler(0f,270f,0f)
    };
    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return rotations[(int)direction];
    }




}
