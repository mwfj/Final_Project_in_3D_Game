using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeCell : MonoBehaviour
{
    //This class is used to discripe the maze unit.\
    public Cordinate cordinate;
    private MazeCellEdge[] edges = new MazeCellEdge[4];
    private int initializedEdgeCount;
    public MazeCellEdge GetEdge(MazeDirection direction)
    {
        return edges[(int)direction];
    }
    /**
     * if 4 edges are intialized, this cell is fully initialized
     */
    public bool IsFullyInitialized
    {
        get
        {
            return initializedEdgeCount == 4;
        }
    }
    public void DeleteEdge(MazeDirection direction)
    {
        MazeCellEdge target = edges[(int)direction];
        if (target)
        {
            // edge exist
            Destroy(target.gameObject);
            edges[(int)direction] = null;
        }
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount++;
    }
    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, 4 - initializedEdgeCount);
            for (int i = 0; i < 4; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips--;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        } 
    }
}
