using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour
{
    [SerializeField] private Transform spider;
    //[SerializeField] private Animation anim;
    public float speed = 5;

    private void Update()
    {
        
        
    }
    private void FixedUpdate()
    {
        //anim.Play();

        spider.Translate(spider.right * speed * Time.deltaTime);
        //spider.Rotate(0, 1, 0);
    }
}
