using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class CharacterControl : MonoBehaviour
{
    public bool canEmit = false;
    public float RegularSpeed = 7;
    public float speed = 7;
    float Jumpforce = 15;
    public int PlayerLife = 100;
    public Rigidbody2D Rigidbody;
    public bool onfloor = false;
    public bool Attracting = false;
    public bool repulse = false;
    public bool facingRight = true;
    private Animator myAnimator;
    public string BlackHole;
    private RelativityShot GetRelativityShotBH;
    public int CountSpeed;
    public bool IsSpeedFull;
    public int CountSlow;
    public bool IsSlowFull;
    public int CountPause;
    public bool IsPauseFull;
    public int CountDamage;
    public bool IsDamageFull;
    public Animator SpeedAnimator;
    public Animator SlowAnimator;
    public Animator PauseAnimator;
    public Slider LifeSlider;
    private Vector2 Checkpoint;
    public GameObject SpeedBH;
    public GameObject SlowBH;
    public GameObject PauseBH;
    private CharacterMovement MyCharacterMovement;
    private bool spawnDust;
    public Transform groundCheck;
    public GameObject DustEffect;

    public GameObject[] Emitters;
    public GameObject myLight;

    public ParticleSystem Dust;

    private float JumpPressed = 0;
    [SerializeField]
    private float JumpPressedTimer = .3f;

    [HideInInspector]
    public float topSpeed = 10;

    float runSpeed;

    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenTurning = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float CutJumpHeight = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        onfloor = true;
        runSpeed = 0;
        myLight.SetActive(false);
        MyCharacterMovement = GetComponent<CharacterMovement>();
        LifeSlider.maxValue = PlayerLife;
        Rigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        GetRelativityShotBH = GetComponent<RelativityShot>();
        IsSpeedFull = false;
        IsSlowFull = false;
        IsPauseFull = false;
        CountSpeed = 0;
        CountSlow = 0;
        CountPause = 0;
        SpeedBH.SetActive(false);
        SlowBH.SetActive(false);
        PauseBH.SetActive(false);
        BlackHole = "Damage";

    }

    void FixedUpdate()
    {

        if(PlayerLife<=0)
        {
            Time.timeScale = 0;
        }

        float eixoX = Rigidbody.velocity.x;
        eixoX = Input.GetAxis("Horizontal");

        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f)
        {
            eixoX *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(Input.GetAxis("Horizontal")) != Mathf.Sign(eixoX))
        {
            eixoX *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            eixoX *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);
        }

        //if(Input.GetButtonDown("Run"))
        //{
        //    if(onfloor)
        //    {
        //        runSpeed = .05f;
        //    }
        //}

        //else if(Input.GetButtonUp("Run"))
        //{
        //    runSpeed = 0;
        //}

        if (facingRight == false && onfloor && eixoX > 0)
        {
            PlayDust();
        }

        else if(facingRight && onfloor && eixoX < 0)
        {
            PlayDust();
        }
        
        if (eixoX > 0)
        {
            facingRight = true;
        }
        else if (eixoX < 0)
        {
            facingRight = false;
        }
        MyCharacterMovement.Movement(eixoX, topSpeed);
        MyCharacterMovement.Rotate(facingRight);

        if (eixoX > 0.01f || eixoX < 0)
        {
            myAnimator.SetBool("isWalking", true);
        }
        else if(eixoX < 0.01f || eixoX > 0)
        {
            myAnimator.SetBool("isWalking", false);
        }

        if(CountSpeed < 500 && IsSpeedFull != true)
        {
            CountSpeed += 1;
           
        }
        else if (SpeedBH.activeSelf == true)
        {
            SpeedAnimator.SetBool("IsFull", true);
            IsSpeedFull = true;
            CountSpeed = 0;
        }
        if (CountPause < 508 && IsPauseFull != true)
        {
            CountPause += 1;

        }
        else if (PauseBH.activeSelf == true)
        {
            PauseAnimator.SetBool("IsFull", true);
            IsPauseFull = true;
            CountPause = 0;
        }
        if (CountSlow < 500 && IsSlowFull != true)
        {
            CountSlow += 1;

        }
        else if (SlowBH.activeSelf == true)
        {
            SlowAnimator.SetBool("IsFull", true);
            IsSlowFull = true;
            CountSlow = 0;
        }
        if (CountDamage < 100 && IsDamageFull != true)
        {
            CountDamage += 1;

        }
        else
        {
            IsDamageFull = true;
            CountDamage = 0;
        } 
    }

    private void Update()
    {
        JumpPressed -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            if(onfloor)
            {
                myAnimator.SetTrigger("TakeOff");
            }
            JumpPressed = JumpPressedTimer;
        }

        if (Input.GetButtonUp("Jump"))
        {
            onfloor = false;
            spawnDust = false;
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y * CutJumpHeight);
        }

        if (JumpPressed > 0 && onfloor)
        {
            JumpPressed = 0;
            PlayDust();
            MyCharacterMovement.Jump(Jumpforce, onfloor);
        }

        if (canEmit)
        {
            Emitters[0].GetComponent<TrailRenderer>().emitting = true;
            Emitters[1].GetComponent<TrailRenderer>().emitting = true;
        }

        else if (!canEmit)
        {
            Emitters[0].GetComponent<TrailRenderer>().emitting = false;
            Emitters[1].GetComponent<TrailRenderer>().emitting = false;
        }

        if(onfloor)
        {
            if (spawnDust == true)
            {
                ProCamera2DShake.Instance.Shake("Landing");
                Instantiate(DustEffect, groundCheck.position, Quaternion.identity);
                spawnDust = false;
            }
        }
        else
        {
            spawnDust = true;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && SpeedBH.activeSelf == true)
        {
            BlackHole = "Speed";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && SlowBH.activeSelf == true)
        {
            BlackHole = "Slow";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && PauseBH.activeSelf == true)
        {
            BlackHole = "Pause";
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BlackHole = "Damage";
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(DecreaseBullets());
        }
    }

    IEnumerator DecreaseBullets()
    {
        GetRelativityShotBH.Shot();
        if (onfloor)
        {
            topSpeed = 3;
            yield return new WaitForSeconds(.1f);
            topSpeed = 10;
        }
        myLight.SetActive(true);
        yield return new WaitForSeconds(.1f);
        myLight.SetActive(false);
    }

    IEnumerator WaitToLeave()
    {
        yield return new WaitForSeconds(.1f);
        onfloor = false;
    }

    public void PlayDust()
    {
        Dust.Play();
    }

    private void OnTriggerStay2D(Collider2D objetoDeColisao)
    {
        if (objetoDeColisao.CompareTag("Floor"))
        {
            canEmit = false;
            myAnimator.SetBool("isJumping", false);
            onfloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D objetoDeColisao)
    {
        if (objetoDeColisao.CompareTag("Floor"))
        {
            canEmit = true;
            myAnimator.SetBool("isJumping", true);
            StartCoroutine(WaitToLeave());
        }
    }
    private void OnTriggerEnter2D(Collider2D objetoDeColisao)
    {
        if (objetoDeColisao.CompareTag("Checkpoint"))
        {
            Checkpoint = transform.position;
        }
        if (objetoDeColisao.CompareTag("Fall"))
        {
            transform.position = Checkpoint;
        }

    }

}