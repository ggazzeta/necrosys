using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativityAttraction : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D Relativity;
    float speed = 10;
    public float Distance;
    private CharacterControl CharacterCoontrol;
    // Start is called before the first frame update
    void Start()
    {
        Relativity = gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 Direction = Player.transform.position - transform.position;

        if(Distance>=0 && Player.GetComponent<CharacterControl>().Attracting == true)
        {
        Relativity.MovePosition(
        Relativity.position +
        Direction.normalized * speed * Time.deltaTime);
        }
        else
        {

        }
        if (Player.GetComponent<CharacterControl>().repulse == true)
        {
            Relativity.MovePosition(
            Relativity.position +
            Direction.normalized * -1 *speed * Time.deltaTime);
        }
        else
        {

        }
    }
}