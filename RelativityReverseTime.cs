using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativityReverseTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.tag != "Floor")
        { 
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = -5;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Floor")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
        }
    }
}
