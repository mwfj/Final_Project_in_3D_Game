using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeZ;

    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public float generationStepDelay;
    public MazeWall wallPrefab;
    public Switch switchPrefab;
    public BakeNavMesh bakeNavMesh;
    BakeNavMesh bake;
    private MazeCell[,] cells;
    private static int trashCount=0; 

    // A List to store different types of trashes.
    public List<Trash> trash_type_list;
    // Record all the trash instances
    public List<Trash> trash_instance_list;

    private List<Switch> switch_pos_list = new List<Switch>();
    public NavMeshSurface surface;
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
    public void Generate()
    {
        // build up the maze with the maze cell
        
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
            DoNextGenerationStep(activeCells);
        }
        CreateRoom();
        CreateSwitch(3);
        // Debug.LogWarning("Capacity:"+trash_instance_list.Capacity);
        //Create Bake instance
        bake = Instantiate(bakeNavMesh) as BakeNavMesh;
        bake.Initialize();
        // The position of Trash/Mob is based on the position of switch
        foreach(Switch sw in switch_pos_list){
            CreateTrashes(sw);
        }
    }
    // Get all switch instance for get its position
    public List<Switch> getSwitchInstance(){
        foreach(Switch pos in switch_pos_list){
            Debug.LogWarning("get:"+pos);
        }
        return switch_pos_list;
    }
    private void CreateTrashes(Switch m_switch){

        int randSeed = Random.Range(0,3);
        Trash m_trash = Instantiate(trash_type_list[ randSeed ]) as Trash;
        m_trash.transform.position = m_switch.transform.position;
        trashCount++;
        m_trash.gameObject.name = "trash" + trashCount;
        // Create mobs/trashes near the switch
        // Vector3 sw_pos = m_switch.transform.position;
        // if(sw_pos.x>-sizeX || sw_pos.x<sizeX){

        //     m_trash.transform.position = sw_pos+new Vector3(1,0,0);
        // }else if(sw_pos.z>-sizeZ || sw_pos.z<sizeZ){
        //     m_trash.transform.position = sw_pos+new Vector3(0,0,1);
        // }else{
        //     m_trash.transform.position = sw_pos;
        // }
        trash_instance_list.Add(m_trash);
    }


    public void DestoryTrash(){
        foreach(Trash trash in trash_instance_list){
            Destroy(trash.gameObject);
        }
        
    }
    public void DestoryNavMesh(){
        if(bake)
            Destroy(bake.gameObject);
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
        // BakerNavMesh baker = new BakerNavMesh();
        // baker.GenerateNavMeshSurface();
        return newCell;
    }

    private void CreateSwitch(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Switch m_switch = Instantiate(switchPrefab) as Switch;
            MazeCell cell = GetCell(RandomCordinate);
            m_switch.Initialize(cell);
            switch_pos_list.Add(m_switch);
            // CreateTrashes(m_switch);
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
