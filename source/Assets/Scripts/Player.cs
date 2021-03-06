﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Maze maze;
    CharacterController m_ch;
    public Camera playerCam;
    public Camera mainCam;
    private Vector3 offset;
    private Vector3 cameraVelocity = Vector3.zero;
    private float rotateSpeed = 400;
    private float cameraRoate = 0;
    // private float cameraSpeed = 0;
    private Vector3 velocity = Vector3.zero;

    // The check whether in the look up mode
    private bool isLookUp;

    //Limit the range of field of view for Player Camera
    public float minFov = 30f;
    public float maxFov = 80f;
    //The distance range that the camera moves after a mouse wheel
    public float sensitivity = 10f;

    // Record the distance between camera and player
    float normal_distance;
    // To sign whether camera hit the wall
    bool isHit;
    // Sign the layer for collision, where collision layer is Wall in this game.
    public LayerMask collisionLayer;
    public float gravity = 0.5f;
    public float jumpSpeed = 5f;
    public float moveSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    Animator animator;
    bool grounded = true;
    private float tempTime = 0f;

    public float rotationAngle = 15.0f; // Camera angle rotation per frame

    public float jumpUpTime = 0.5f;
    private bool isJumpingUp = false;

    private float lastGroundTime;
    public float jumpBound=0.05f;


    void Start()
    {

        Spawn();
        SetCamera();
        m_ch = this.GetComponent<CharacterController>();
        normal_distance = Vector3.Distance(this.transform.position, playerCam.transform.position);
        isLookUp = false;
        isHit = false;

    }
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Jump", false);
    }

    // Update is called once per frame
    void Update()
    {
        Control();
        offset = this.transform.position;
        normal_distance = Vector3.Distance(this.transform.position, playerCam.transform.position);
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        UpdateCamera();
    }

    private void Spawn()
    {
        print("spawn");
        //randomly spawn in the maze
        Cordinate cordinate = maze.RandomCordinate;
        this.transform.position = new Vector3(cordinate.x - maze.sizeX * 0.5f + 0.5f, 0f, cordinate.z - maze.sizeZ * 0.5f + 0.5f);
    }
    private void SetCamera()
    {
        playerCam = GameObject.Find("PlayerCam").GetComponent<Camera>();
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerCam.transform.position = Vector3.SmoothDamp(playerCam.transform.position, offset + new Vector3(0, 0.1f, -1), ref cameraVelocity, 0.01f);
        normal_distance = Vector3.Distance(this.transform.position, playerCam.transform.position);
    }
 
    private void Control()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float sr = Mathf.Sin(cameraRoate);
        float cr = Mathf.Cos(cameraRoate);

        if (v != 0 || h != 0)
        {
            animator.SetBool("Running", true);
            animator.SetFloat("x", h);
            animator.SetFloat("y", v);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        moveDirection = new Vector3((v * sr + h * cr)*moveSpeed, 0, (v * cr - h * sr)*moveSpeed);
        if (grounded)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (Input.GetButtonDown("Jump"))
            {
                if (info.IsName("Stand@loop 2 0 0") || info.IsName("Running")){
                    grounded = false;
                    animator.SetBool("Jump", true);
                    animator.SetBool("Running", false);
                    animator.SetBool("Grounded", false);
                    Debug.Log("set Grounded false");
                    isJumpingUp = true;
                    tempTime = Time.time;
                    moveDirection.y = jumpSpeed;
                }
            }
        }
        else
        {
            if (isJumpingUp)
            {
                if (Time.time - tempTime < jumpUpTime)
                {
                    moveDirection.y = jumpSpeed;
                }
                if (Time.time-tempTime <jumpUpTime/3.5f)
                {
                    animator.SetBool("Jump", false);
                }
                if (Time.time - tempTime < jumpUpTime / 1.5)
                {
                    animator.SetBool("Grounded", true);
                }
            }
            /*else
            {
                if (moveDirection.y > 0)
                {
                    moveDirection.y -= gravity * Time.deltaTime;
                    animator.SetBool("Jump", false);
                    Debug.Log("set Jump false");
                }
                else
                {
                    moveDirection.y = 0;

                }
            }*/
        }
        Vector3 direction;
        if (v < 0)
        {
            //moveDirection.x = -moveDirection.x;
            direction = -playerCam.transform.forward;
            direction.y = 0;
        }
        else
        {
            direction = playerCam.transform.forward;
            direction.y = 0;
        }
        m_ch.Move(moveDirection * Time.deltaTime);
 
        this.transform.forward = direction;
 


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("ground"))
        {
            grounded = true;
            //Debug.Log("set Grounded true");
            Debug.Log("Grounded : \t"+animator.GetBool("Grounded"));
            lastGroundTime = Time.time;
        }
    }
   

    // After scrolling the mouse wheel, it field of view for player camer will be changed
    private void ScrollWheel()
    {
        float fov = playerCam.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        playerCam.fieldOfView = fov;
    }
    private void UpdateCamera()
    {
        // use smooth damp to move camera to target position smoothly
        //float k = Mathf.Max((this.transform.position - offset).magnitude,0);
        isHit = false;
        ScrollWheel();
        //When the user clicks the left mouse button, the camera will go from bottom to top to view the character
        //When the user clicks it again, the relative position of camera back to the default angle. 
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed* 4 * Time.deltaTime;
            // playerCam.transform.RotateAround(this.transform.position, Vector3.right, mouseY);
            if (isLookUp)
            {
                isLookUp = false;
            }
            else
            {
                isLookUp = true;
            }
        }
        // The camera will not into the Lookup mode unless it not collide with any Wall
        if (!isHit)
        {
            if (isLookUp)
            {
                playerCam.transform.position -= playerCam.transform.up * 2.3f * Time.deltaTime;
            }
        }
        //Rotate by Y axis
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            playerCam.transform.RotateAround(this.transform.position, Vector3.up, mouseX*30*Time.deltaTime);
        }

        // if(Input.GetAxis("Mouse ScrollWheel")!=0){
        //     playerCam.fieldOfView = playerCam.fieldOfView-Input.GetAxis("Mouse ScrollWheel")*20;
        // }

        cameraRoate = playerCam.transform.eulerAngles.y / 180 * Mathf.PI;
        float sr = Mathf.Sin(cameraRoate);
        float cr = Mathf.Cos(cameraRoate);
        playerCam.transform.position = Vector3.SmoothDamp(playerCam.transform.position, offset + new Vector3(-sr, 1f, -cr), ref velocity, 0.3f);
        mainCam.transform.position += (this.transform.position - offset);
        RaycastHit hit;
        // Use Linecast to detect the collision
        if (Physics.Linecast(this.transform.position + Vector3.up, playerCam.transform.position, out hit, collisionLayer))
        {
            string name = hit.collider.tag;
            if (name != "PlayerCam")
            {
                isHit = true;
                float currentDistance = hit.distance;
                // Debug.Log("NormalDistanec"+normal_distance);
                // Debug.Log("currentDistance"+currentDistance);

                // if Hit the wall, replace the camera position to hit point
                if (currentDistance < normal_distance)
                {
                    // playerCam.transform.position = new Vector3(hit.point.x,playerCam.transform.position.y, hit.point.z);
                    playerCam.transform.position = hit.point;
                }
            }
        }
        playerCam.GetComponent<Transform>().LookAt(this.transform);

        // RaycastHit hit;
        // Vector3 fwd = playerCam.transform.TransformDirection(Vector3.forward);
        // if (Physics.Raycast(playerCam.transform.position,fwd, out hit,0.5f))
        // {
        //     string name = hit.collider.tag;
        //     if (name != "PlayerCam")
        //     {
        //         float distance = hit.distance;

        //         Vector3 correction = Vector3.Normalize(playerCam.transform.TransformDirection(Vector3.forward)) * distance;
        //         playerCam.transform.position += correction;

        //     }
        // }
    }
}
