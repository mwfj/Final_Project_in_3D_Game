using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCollider : MonoBehaviour
{
    public bool isBossCreated;
    private Maze mazeInstance;
    private int sizeX,sizeZ;
    public BackgroundMusicController musicController;
    private BossHealthBar bossHealthBar;
    private GameManager gameManager;



    public void Initialization(int x_axis,int z_axis, Maze maze, BackgroundMusicController _musicController){
        sizeZ = z_axis;
        sizeX = x_axis;
        mazeInstance = maze;
        musicController = _musicController;
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        isBossCreated = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossHealthBar = gameManager.bossHealthBar;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(!isBossCreated){
            if(other.CompareTag("Player")){
                isBossCreated = true;
                mazeInstance.CreateBoss(sizeX,0);
                musicController.ChangeMusicToBoomRoom();
                bossHealthBar.gameObject.SetActive(true);
            }
        }
    }
}
