using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeZ;

    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public float generationStepDelay;
    public MazeWall wallPrefab;
    public Switch switchPrefab;

    private MazeCell[,] cells;
    public Cordinate RandomCordinate 
    {
        // generate random cordinate to set the maze cell
        get
        {
            return new Cordinate(Random.Range(0, sizeX), Random.Range(0, sizeZ));
        }
    }
    public bool WithinMazeScope(Cordinate cordinate)
    {
        // Test whether the cordinate is in the maze base area.
        return cordinate.x >= 0 && cordinate.x < sizeX && cordinate.z >= 0 && cordinate.z < sizeZ;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public MazeCell GetCell(Cordinate cordinate)
    {
        return cells[cordinate.x, cordinate.z];
    }
    public IEnumerator Generate()
    {

        // build up the maze with the maze cell
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[2*sizeX, 2*sizeZ];
        /**
         * Using a list to record the active cells
         * every active cell can expand to one random direction
         * Construct the entire maze in a DFS way.
         */
        List<MazeCell> activeCells = new List<MazeCell>();  // using list to simulate the stack
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
        CreateSwitch(3);
        CreateRoom();
    }
    private void DoFirstGenerationStep(List<MazeCell> activeCells) // add the first cell
    {
        activeCells.Add(CreateCell(RandomCordinate));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        /**
         * Add the new cell into the stack
         * if the cell cannot expand, remove it from the stack.
         * In this way to simulate a DFS process.
         */
        int curIndex = activeCells.Count - 1;
        MazeCell curr = activeCells[curIndex];
        
        if (curr.IsFullyInitialized) //This cell is fully initialized, remove it from stack.
        {
            activeCells.RemoveAt(curIndex);
            return;
        }
        MazeDirection direction = curr.RandomUninitializedDirection;
        Cordinate cordinate = curr.cordinate + direction.toCordinate();
        if (WithinMazeScope(cordinate))
        {
            MazeCell neighbor = GetCell(cordinate);
            if (neighbor == null)
            {
                neighbor = CreateCell(cordinate);
                CreatePassage(curr, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(curr, neighbor, direction);
            }
        }
        else
        {
            CreateWall(curr, null, direction);
        }
 
    }

    /**
     * create instance of wall and passage
     */
    private void CreatePassage(MazeCell source, MazeCell sink, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(source, sink, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(sink, source, direction.GetOpposite());
    }
    private void CreateWall(MazeCell source, MazeCell sink, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(source, sink, direction);
        if(sink != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(sink, source, direction.GetOpposite());
        }
    }

    private MazeCell CreateCell(Cordinate cordinate)
    {
        // Create the cell instance and gameobject
        // location = (x,0,z), x is the numer the of cells in a row and z is column.
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[cordinate.x, cordinate.z] = newCell;
        newCell.cordinate = cordinate;
        newCell.name = "Maze Cell" + cordinate.x + "," + cordinate.z;
        newCell.transform.parent = transform;
        // transform x,z into actual value.
        newCell.transform.localPosition = new Vector3(cordinate.x - sizeX * 0.5f + 0.5f, 0f, cordinate.z - sizeZ * 0.5f + 0.5f);
        return newCell;
    }

    private void CreateSwitch(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Switch m_switch = Instantiate(switchPrefab) as Switch;
            MazeCell cell = GetCell(RandomCordinate);
            m_switch.Initialize(cell);
        }
    }
    public void CreateRoom()
    {
        /** Create room inside the maze
         for (int i = 3; i < 7; i++)
         {
             for(int j = 3; j < 7; j++)
             {
                 //select the middle area to generate boos room, will have more settings to change size and location and number of rooms in next patch.
                 Cordinate cor = new Cordinate(i, j);
                 MazeCell curr = GetCell(cor); // Get maze cell by i,j
                 if (i > 3 && j > 3 && i < 6 && j < 6)
                 {
                     // all the mazecell inside the room, we should delete the edge to the neibour
                     for (int k = 0; k < 4; k++)
                     {
                         MazeCellEdge edge = curr.GetEdge((MazeDirection)k);
                         Debug.Log(i + "," + j + "," + k + "," + edge.GetEdgeType() + "\n");
                         string edgeType = edge.GetEdgeType();
                         if (edgeType.Equals("Wall"))
                         {
                             Debug.Log("deleting"+i+","+j);
                             curr.DeleteEdge((MazeDirection)k);
                         }
                     }
                 }
             }
         }*/



        // Create a boss room outside the maze .
        Cordinate cor;
        MazeCell cell;
        
        for(int i = 0; i < sizeZ; i++)
        {
            for(int j = 0; j < sizeX; j++)
            {
                cor = new Cordinate(sizeZ+i, j);
                CreateCell(cor);
            }
        }
        for (int i = 0; i < sizeZ; i++)
        {
            cor = new Cordinate(sizeZ + i, 0);
            cell = GetCell(cor);
            CreateWall(cell,null,MazeDirection.South);
            cor = new Cordinate(sizeZ + i, sizeX-1);
            cell = GetCell(cor);
            CreateWall(cell, null, MazeDirection.North);
        }
        for (int i = 0; i < sizeX; i++)
        {
            cor = new Cordinate(2*sizeZ-1, i);
            cell = GetCell(cor);
            CreateWall(cell, null, MazeDirection.East);
        }
    }
    public void OpenDoor()
    {
        Cordinate cor = new Cordinate(9, 5);
        MazeCell cell = GetCell(cor);
        cell.DeleteEdge(MazeDirection.East);
    }

    



}
