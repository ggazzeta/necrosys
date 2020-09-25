using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGun : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            GameObject.FindWithTag("Interface").transform.GetChild(1).gameObject.SetActive(true);
        }
    }

}
