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

        swivelAngle += x * rotateSpeed * Time.deltaTime;
        tiltAngle += -y * rotateSpeed * Time.deltaTime;

        tiltAngle = Mathf.Clamp(tiltAngle, -45, 45);

        Vector3 eulerRot = new Vector3(tiltAngle, swivelAngle, 0);
        transform.position = Vector3.Lerp(transform.position, target.position, 0.1f);
        transform.rotation = Quaternion.Euler(eulerRot);
    }

}
