using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class EnemyHealth : MonoBehaviour
{
    public Image Background;
    public Image CurrentHealth;
    Animator myAnimator;
    public GameObject ExplosionVFX;
    public GameObject Stain;
    public Transform Spawner;
    SpriteRenderer myRenderer;

    EnemyAI AI;
    Color Hitted;

 
    [SerializeField]
    float Damage = .35f;

    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<EnemyAI>();
        myAnimator = GetComponentInChildren<Animator>();
        myRenderer = GetComponentInChildren<SpriteRenderer>();
        Background.enabled = false;
        CurrentHealth.enabled = false;
        Hitted = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth.fillAmount < 1)
        {
            Background.enabled = true;
            CurrentHealth.enabled = true;
        }

        if(CurrentHealth.fillAmount <= 0)
        {
            if(GetComponent<EnemyAI>() != null)
            {
                GetComponent<EnemyAI>().sawPlayer = false;
                GetComponent<EnemyAI>().StopChasing();
                CurrentHealth.fillAmount = 0;
                Destroy(gameObject.GetComponentInChildren<EnemyAI>());
                StartCoroutine(Die());
            }

        }

    }

    IEnumerator TakeHit()
    {
        CurrentHealth.fillAmount -= Damage;

        if(AI != null)
        {
            myRenderer.color = Hitted;
            GetComponent<EnemyAI>().moveSpeed = 3f;
            yield return new WaitForSeconds(.2f);
            myRenderer.color = Color.white;
            GetComponent<EnemyAI>().moveSpeed = 5;
        }

    }


    IEnumerator Die()
    {
        myAnimator.enabled = false;
        yield return new WaitForSeconds(0);
        Instantiate(ExplosionVFX, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(Stain, Spawner.position, Spawner.rotation);
        ProCamera2DShake.Instance.Shake("GunShot");
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(TakeHit());
            Destroy(collision.gameObject, .1f);
        }

        if (collision.gameObject.tag == "Water")
        {
            StartCoroutine(Die());
        }

        if (collision.gameObject.tag == "Hazard")
        {
            StartCoroutine(Die());
        }
    }
}
