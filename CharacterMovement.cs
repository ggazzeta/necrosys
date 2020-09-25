using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    Rigidbody2D MyRigidbody;

    private void Awake()
    {
        MyRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Movement(float direction, float speed)
    {
        MyRigidbody.velocity = new Vector2(direction * speed, MyRigidbody.velocity.y);
    }
    public void Jump(float Jumpforce, bool onFloor)
    {
        if (onFloor == true)
        {
            MyRigidbody.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
        }
    }
    public void Rotate(bool facingRight)
    {
        if(facingRight == true)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (facingRight == false)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

    }
}
