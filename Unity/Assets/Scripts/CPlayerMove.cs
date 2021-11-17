using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMove : MonoBehaviour
{
  
    public Transform foot;
    public float checkRadius = .2f;
    public float speed = 10;
    public float x, z;
    public float jumpForce = 15;
    public LayerMask floorLayers;
    public int numCollectedKeys;
    private Rigidbody rb;
    private bool doJump;
    public float camSpeed = 500;
    public Transform camera;
    Vector3 velocity;


    public bool isGrounded;

    // Start is called before the first frame update

    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        velocity = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {


        isGrounded = Physics.CheckSphere(foot.position, checkRadius, floorLayers);

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (!doJump && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            doJump = true;
        }
       


    }
    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + movement);
        //transform.position += transform.forward * z * speed + transform.right * x * speed;


        Quaternion cam = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up);
        Vector3 movement = new Vector3(x * Time.deltaTime * speed, 0, z * Time.deltaTime * speed);
        movement = cam * movement;
        rb.AddForce(movement * speed * Time.deltaTime, ForceMode.VelocityChange);




        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * camSpeed);

        if (doJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            doJump = false;
        }
        
    }

    
}
