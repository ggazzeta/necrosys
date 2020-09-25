using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePowerUp : MonoBehaviour
{
    public GameObject Power;
    public GameController gameController;
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
        if (collision.gameObject.tag == "Player")
        {
            Power.SetActive(true);
            Destroy(gameObject);
            gameController.explaningBH();
        }

    }
}
