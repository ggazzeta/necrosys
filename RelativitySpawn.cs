using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativitySpawn : MonoBehaviour
{
    public GameObject GunShot;
    public GameObject RelativityBH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(RelativityBH, GunShot.transform.position, GunShot.transform.rotation);
    }
}
