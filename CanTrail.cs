using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTrail : MonoBehaviour
{
    public TrailRenderer Trail;


    void Start()
    {
        Trail.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
    
        if (GetComponent<CharacterControl>().canEmit)
        {
            Trail.emitting = true;
        }

        else if(GetComponent<CharacterControl>().canEmit == false)
        {
            Trail.emitting = false;
        }
    }
}
