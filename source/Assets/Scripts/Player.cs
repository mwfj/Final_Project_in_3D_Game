using System.Collections;
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
    void Start()
    {
        Spawn();
        SetCamera();
        m_ch = this.GetComponent<CharacterController>();
        isLookUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        Control();
        UpdateCamera();
        offset = this.transform.position;
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
    }
    private void Control() 
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float sr = Mathf.Sin(cameraRoate);
        float cr = Mathf.Cos(cameraRoate);
        Vector3 moveDirection = new Vector3((v * sr + h * cr), 0, (v * cr - h * sr));
        m_ch.Move(moveDirection * Time.deltaTime);
        Vector3 direction = playerCam.transform.forward;
        direction.y = 0;
        this.transform.forward = direction;
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }


    }

     // After scrolling the mouse wheel, it field of view for player camer will be changed
    private void ScrollWheel(){
        float fov = playerCam.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        playerCam.fieldOfView = fov;
    }
    private void UpdateCamera()
    {
        // use smooth damp to move camera to target position smoothly
        //float k = Mathf.Max((this.transform.position - offset).magnitude,0);
       

        //Rotate by Y axis
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            playerCam.transform.RotateAround(this.transform.position, Vector3.up, mouseX);
        }

        //When the user clicks the left mouse button, the camera will go from bottom to top to view the character
        //When the user clicks it again, the relative position of camera back to the default angle. 
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            // float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed* 4 * Time.deltaTime;
            // playerCam.transform.RotateAround(this.transform.position, Vector3.right, mouseY);
            if(isLookUp){
                isLookUp=false;
            }else{
                isLookUp=true;
            }
        }
        if(isLookUp){
            playerCam.transform.position += -playerCam.transform.up *2.5f*Time.deltaTime;
        }
        // if(Input.GetAxis("Mouse ScrollWheel")!=0){
        //     playerCam.fieldOfView = playerCam.fieldOfView-Input.GetAxis("Mouse ScrollWheel")*20;
        // }
        ScrollWheel();
        
        cameraRoate = playerCam.transform.eulerAngles.y / 180 * Mathf.PI;
        float sr = Mathf.Sin(cameraRoate);
        float cr = Mathf.Cos(cameraRoate);
        playerCam.transform.position = Vector3.SmoothDamp(playerCam.transform.position, offset + new Vector3(-sr, 1f, -cr), ref velocity, 0.3f);
        playerCam.GetComponent<Transform>().LookAt(this.transform);
        mainCam.transform.position += (this.transform.position - offset);
        RaycastHit hit;
        Vector3 fwd = playerCam.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(playerCam.transform.position,fwd  , out hit,0.5f))
        {
            string name = hit.collider.tag;
            if (name != "PlayerCam")
            {
                float distance = hit.distance;
                
                    Vector3 correction = Vector3.Normalize(playerCam.transform.TransformDirection(Vector3.forward)) * distance;
                    playerCam.transform.position += correction;
                
            }
        }

    }
}
