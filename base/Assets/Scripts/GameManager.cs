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

    // Start is called before the first frame update
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
        if (switchCount == 3)
        {
            text.text = "Finish!\n Press Space to restart!";
            text.fontSize = 60;
            //text.transform.position = Vector3.zero;
            
        }
    }
    private void BeginGame()
    {
        // create maze instance from prefab
        switchCount = 0;
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.sizeX = size;
        mazeInstance.sizeZ = size;
        StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
    }
    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        Destroy(playerInstance.gameObject);
        BeginGame();
    }
}
