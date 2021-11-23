using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShooters : MonoBehaviour
{
    public GameObject shooterPrefab;
    GameObject[] shooters;
    public float bulletForce = 2f;
    GameObject bullet;





    private void Start()
    {
        StartCoroutine(DieAfterSeconds());
        shooters = GameObject.FindGameObjectsWithTag("Shooters");
        InvokeRepeating("Shoot", 2f, 2f);

    }

    private void Update()
    {

    }

   

    void Shoot()
    {
        foreach (GameObject shooter in shooters)
        {
            bullet = Instantiate(shooterPrefab, shooter.transform.position - transform.forward, shooter.transform.rotation);
            //bullet.GetComponent<Rigidbody>().velocity = shooter.GetComponent<Rigidbody>().velocity;
            bullet.GetComponent<Rigidbody>().AddForce(shooter.transform.forward * -bulletForce, ForceMode.Impulse);
        }
    }

    IEnumerator DieAfterSeconds()
    {
        yield return new WaitForSeconds(5);
        
    }
}
