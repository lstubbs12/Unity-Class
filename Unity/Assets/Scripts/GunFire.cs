using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{

    [SerializeField] private GameObject bulletpre;
    [SerializeField] private Transform gunTip;
    [SerializeField] private float bulletForce;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletpre, gunTip.position, gunTip.rotation);
            bullet.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
            bullet.GetComponent<Rigidbody>().AddForce(gunTip.forward * bulletForce, ForceMode.Impulse);
        }
    }
}
