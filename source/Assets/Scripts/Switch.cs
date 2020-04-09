using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager game;
    public Player unitychan;
    public Text text;
    public void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        unitychan = GameObject.Find("unitychan(Clone)").GetComponent<Player>();
        text = GameObject.Find("Text").GetComponent<Text>();
    }
    public void Initialize(MazeCell cell)
    {
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
    }
    private void OnMouseDown()
    {
        if (Vector3.Distance(this.transform.position, unitychan.transform.position) < 0.5)
        {
            this.GetComponent<Renderer>().material.color = Color.green;
            game.switchCount++;
        }
        else
        {
            text.text = "\n Too far from the object";
        }
    }
}
