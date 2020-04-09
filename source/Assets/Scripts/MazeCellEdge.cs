using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour
{
    /**
     * This class is used to represent the Edge between two cells
     */
    public MazeCell source, sink;
    public Cordinate direction;
    public abstract string GetEdgeType();
    public void Initialize(MazeCell source, MazeCell sink, MazeDirection direction)
    {
        this.source = source;
        this.sink = sink;
        this.direction = direction.toCordinate();
        source.SetEdge(direction, this);
        transform.parent = source.transform;
        transform.localPosition = Vector3.zero;
        transform.rotation = direction.ToRotation();
    }
}
