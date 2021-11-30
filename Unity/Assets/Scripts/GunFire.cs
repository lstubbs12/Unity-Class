using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunFire : MonoBehaviour
{

    [SerializeField] private GameObject bulletpre;
    [SerializeField] private Transform gunTip;
    [SerializeField] private float bulletForce;
    [SerializeField] private GameObject player;
    public Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = camera.GetComponent<Camera>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //Debug.Log("Hit " + hit.collider.name);
        }

        

        if (Input.GetMouseButtonDown(0))
        {
            
            GameObject bullet = Instantiate(bulletpre, gunTip.position, gunTip.rotation);
            //bullet.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
            bullet.GetComponent<Rigidbody>().AddForce((hit.point - gunTip.position) * bulletForce, ForceMode.Impulse);
            
        }

    }
}
