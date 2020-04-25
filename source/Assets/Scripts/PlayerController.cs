using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 movement;
    Rigidbody playerRigidbody;
    Animator animator;

    // Player speed
    public float speed = 6f;

    private float x;
    private float y;
    private float xSpeed = 2;
    private float ySpeed = 2;
    private Quaternion direct;

    // wheather the character is on the ground
    private bool grounded = true;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded == true)
            {
                animator.SetBool("Jump", true);
                playerRigidbody.velocity += new Vector3(0, 10, 0); 
                playerRigidbody.AddForce(Vector3.up * 20); 
                grounded = false;
            }
        }
    
    }
    void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("Jump", false);
        grounded = true;
    }
}
