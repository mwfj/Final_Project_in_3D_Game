using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Maze mazePrefab; //reference the prefab of Maze 
    private Maze mazeInstance;
    public Player playerPrefab;
    private Player playerInstance;
    public int switchCount;
    public int size=10;
    public Text text;
    public Text guide;
    public DeathCount deathCountPrefab;
    private DeathCount deathCountInstance;

    void Start()
    {
        BeginGame();
        text.color = Color.red;
        guide.text = "Use wasd to move, right button on mouse to rotate. Find three switches to escape the dungeon.";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            if (size < 50)
            { size += 10; }
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            if (size > 10)
            { size -= 10; }
            RestartGame();
        }

        text.text = switchCount + "/ 3 switches remain";
        if (switchCount == 3)
        {
            mazeInstance.OpenDoor();
            text.text = "The door to the boss room open";
            text.fontSize = 40;
            //text.transform.position = Vector3.zero;
            
        }
    }
    // private void CreateTrashes(Switch sw){

    //     int randSeed = Random.Range(0,3);
    //     Trash m_trash = Instantiate(trash_type_list[ randSeed ]) as Trash;
    //     m_trash.transform.position = sw.transform.position;
    //     trash_instance_list.Add(m_trash);
    // }
    // public void DestoryTrash(){
    //     foreach(Trash trash in trash_instance_list){
    //         Destroy(trash.gameObject);
    //     }
    // }
    private void BeginGame()
    {
        // create maze instance from prefab
        switchCount = 0;
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.sizeX = size;
        mazeInstance.sizeZ = size;
        mazeInstance.Generate();
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.gameObject.name = "unitychan(Clone)";

    }
    private void RestartGame()
    {
        StopAllCoroutines();
        // Destory mobs and navmesh
        Destroy(playerInstance.gameObject);
        if(mazeInstance){
            mazeInstance.DestoryTrash();
            mazeInstance.DestoryNavMesh();
        }
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }
}
