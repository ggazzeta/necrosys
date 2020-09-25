using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfWorkingTrue : MonoBehaviour
{
    public bool Working;
    public bool Default;
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
        if (collision.gameObject.tag == "Speed")
        {
            Working = true;
        }
        else if (collision.gameObject.tag == "Pause")
        {
            Working = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Speed")
        {
            Working = Default;
        }
        else if (collision.gameObject.tag == "Pause")
        {
            Working = Default;
        }
    }
}
