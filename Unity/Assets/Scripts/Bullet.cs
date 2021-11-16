using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float dmgValue;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DieAfterSeconds());
    }

    IEnumerator DieAfterSeconds()
    {
        yield return new WaitForSeconds(5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().takeDamage(dmgValue);
            Object.Destroy(gameObject);
        }
    }
}
