using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;

    [SerializeField]
    float agroRange;
    float Jumpforce = 10;
    float jumpForceBelow = 20;
    bool canJump = false;
    bool isGrounded;

    [SerializeField]
    float attackRange;

    public float direction;
    public ParticleSystem questionMark;

    [HideInInspector]
    public float moveSpeed;

    public bool isFacingRight = true;
    public bool sawPlayer = false;

    private Rigidbody2D myRB;
    private Animator myAnimator;
    private CharacterMovement MyEnemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        MyEnemyMovement = GetComponent<CharacterMovement>();
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 myDistance = transform.position - Player.transform.position;
        //float distToPlayer = Vector2.Distance(transform.position, Player.transform.position);
        float distToPlayer = Mathf.Abs(myDistance.x);
        float distToPlayerY = Mathf.Abs(myDistance.y);

        if (distToPlayer < agroRange && distToPlayer > attackRange)
        {
            sawPlayer = true;
            ChasePlayer();
            MyEnemyMovement.Movement(direction, moveSpeed);
            MyEnemyMovement.Rotate(isFacingRight);
        }

        else if (distToPlayer < attackRange)
        {
            myAnimator.SetBool("inRange", true);
            AttackPlayer();
        }

        else if (distToPlayer > attackRange && distToPlayer > agroRange)
        {
            StopChasing();
        }

        else if (distToPlayerY > 2 && distToPlayerY <= 10 && distToPlayer > attackRange)
        {
            JumpAttack();
        }

    }

    void AttackPlayer()
    {
        myAnimator.SetTrigger("Attack");
        MyEnemyMovement.Movement(direction, moveSpeed);

    }

    void JumpAttack()
    {
        if (canJump && isGrounded)
        {
            myAnimator.SetBool("inRange", true);
            myAnimator.SetTrigger("Attack");
            myRB.velocity = Vector2.up * jumpForceBelow;
        }
    }


    void ChasePlayer()
    {
        myAnimator.SetBool("inRange", false);
        myAnimator.SetBool("inSight", true);

        if (transform.position.x < Player.transform.position.x)
        {
            direction = 1;
            isFacingRight = false;

            if(canJump && isGrounded)
            {
                myRB.velocity = Vector2.up * Jumpforce;
            }
        }
        else if (transform.position.x > Player.transform.position.x)
        {
            direction = -1;
            isFacingRight = true;

            if (canJump && isGrounded)
            {
                MyEnemyMovement.Jump(Jumpforce, isGrounded);
            }
        }

        else if (transform.position.x == Player.transform.position.x)
        {
            AttackPlayer();
        }
    }

    public void StopChasing()
    {
        if(sawPlayer)
        {
            questionMark.Play();
            sawPlayer = false;
        }
        myRB.isKinematic = false;
        myAnimator.SetBool("inSight", false);
        myRB.velocity = new Vector2(0, myRB.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = false;
        }
        if (collision.gameObject.tag == "Water")
        {
            myRB.isKinematic = false;
        }
    }

}
