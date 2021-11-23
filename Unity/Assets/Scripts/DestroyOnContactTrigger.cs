using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContactTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
