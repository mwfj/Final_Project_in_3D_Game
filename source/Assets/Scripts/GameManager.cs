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
    public BossRoomCollider bossRoomColliderPrefab;
    private BossRoomCollider colliderInstance;
    private int count = 0;
    public int switchCount;
    public int size=10;
    public Text text;
    public Text guide;
    public DeathCount deathCountPrefab;
    private DeathCount deathCountInstance;
    public BossHealthBar bossHealthBar;

    public BackgroundMusicController musicController;

    private BackgroundMusicController musicContollerInstance;



    
    // The Collider will store in this parameter for detect whether main character is in the boss room
    private Collider bossRoomCollider;
    private bool isColliderCreated;

    void Start()
    {
        BeginGame();
        isColliderCreated = false;
        text.color = Color.red;
        guide.text = "Find three switches to escape the dungeon.";
        bossHealthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
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
        if (switchCount >= 3)
        {
            mazeInstance.OpenDoor();
            text.text = "The door to the boss room open";
            text.fontSize = 40;
            //text.transform.position = Vector3.zero;
            
        }
        // Boss room collider only create once, when boss room open but main character not enter the boss room
        if(mazeInstance.GetIsDoorOpen() && isColliderCreated == false){
            isColliderCreated = true;
            Debug.LogWarning(isColliderCreated);
            CreateBossRoomCollider();
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
        musicContollerInstance = Instantiate(musicController) as BackgroundMusicController;
    }
    private void RestartGame()
    {
        Destroy(musicContollerInstance.gameObject);
        // musicController.ChangeToMazeMusic();
        isColliderCreated = false;
        StopAllCoroutines();
        if(colliderInstance){
            Destroy(colliderInstance.gameObject);
        }
        // Destory mobs and navmesh
        Destroy(playerInstance.gameObject);
        if(mazeInstance){
            mazeInstance.DestoryTrash();
            mazeInstance.DestroyBoss();
            mazeInstance.DestoryNavMesh();
        }
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }
    public void CreateBossRoomCollider(){
        Debug.LogWarning("Create collider");
        colliderInstance = Instantiate(bossRoomColliderPrefab) as BossRoomCollider;
        colliderInstance.gameObject.name = "BossRoomCollider(clone)";
        colliderInstance.transform.position = new Vector3(size/2,0.5f,0.5f);
        colliderInstance.Initialization(size,size,mazeInstance,musicContollerInstance);
        bossRoomCollider = colliderInstance.GetComponent<BoxCollider>();
        bossRoomCollider.isTrigger = true;
        // isColliderCreated = true;
    }
}
