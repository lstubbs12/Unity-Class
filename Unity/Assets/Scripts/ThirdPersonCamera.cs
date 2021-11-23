using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target;
    public float rotateSpeed = 3;

    public float swivelAngle, tiltAngle;

    private float x, y;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    private void FixedUpdate()
    {

        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        transform.RotateAround(target.transform.position, -Vector3.up, x * rotateSpeed); 
        transform.RotateAround(Vector3.zero, transform.right, y * rotateSpeed);
    }

}
