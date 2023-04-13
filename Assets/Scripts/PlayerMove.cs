using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _controller;
    private Camera _camera;
    private Transform _cameraTransform;
    private Vector3 movement;

    public int speed = 5;
    public float yRot = 2f; // turn our gameobject side by side
    // looking up and down
    public float xRot = 1f;
    public float xRotValue = 0f;
    public int FOV = 60;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _cameraTransform = GameObject.Find("Camera").transform;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = new Vector3(speed * Input.GetAxis("Horizontal"), 0, 2 * speed * Input.GetAxis("Vertical"));
            _controller.Move(transform.TransformDirection(movement * Time.deltaTime));
            _camera.fieldOfView = FOV + 10;
        }
        else
        {
            movement = new Vector3(speed * Input.GetAxis("Horizontal"), 0, speed * Input.GetAxis("Vertical"));
            _controller.Move(transform.TransformDirection(movement * Time.deltaTime));
            _camera.fieldOfView = FOV;
        }
        transform.Rotate(0, yRot * Input.GetAxis("Mouse X"), 0); // -1 to 1

        

       

        xRotValue += xRot * -Input.GetAxis("Mouse Y"); // -1 to 1
        _cameraTransform.rotation = Quaternion.Euler(Mathf.Clamp(xRotValue, -90, 90), _cameraTransform.eulerAngles.y, _cameraTransform.eulerAngles.z);


        // quaternion, eulerangles (suffers from gimbal lock problem) 
    }
}

