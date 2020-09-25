using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receivingPauseEffect : MonoBehaviour
{
    private Rigidbody2D Rb;
    private float waitTime = 2f;
    WaitForSecondsRealtime waitForSecondsRealtime;


    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pause")
        {
            Rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }//END ON TRIGGER STAY

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pause")
        {
            Rb.constraints = RigidbodyConstraints2D.None;
        }
    }//END ON TRIGGER EXIT

    IEnumerator restartMovement()
    {
        waitTime = 2f;
        if (waitForSecondsRealtime == null)
            waitForSecondsRealtime = new WaitForSecondsRealtime(waitTime);
        else
            waitForSecondsRealtime.waitTime = waitTime;
        Debug.Log(waitForSecondsRealtime);
        yield return waitForSecondsRealtime;
        Rb.constraints = RigidbodyConstraints2D.None;
    }
}
