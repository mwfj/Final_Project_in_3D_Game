using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePassage : MazeCellEdge
{
    // represent the passage from  one cell to another cell
    override public string GetEdgeType()
    {
        return "Passage";
    }
}
