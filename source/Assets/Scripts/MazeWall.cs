using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MazeCellEdge
{
    //represent the wall of a cell
    public override string GetEdgeType()
    {
        return "Wall"; 
    }
}
