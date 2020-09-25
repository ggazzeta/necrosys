using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class Relativitybullet : MonoBehaviour
{
    Rigidbody2D BulletRigidbody;
    float BulletSpeed = 30;
    public bool CharacterfacingRight;
    private GameObject Player;
    public GameObject GunShot;
    public GameObject Relativity;
    public GameObject HitVFX;
    public GameObject HitEnemyVFX;

    string RelativityTag;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        BulletRigidbody = GetComponent<Rigidbody2D>();
        CharacterfacingRight = Player.GetComponent<CharacterControl>().facingRight;
        RelativityTag = Player.GetComponent<CharacterControl>().BlackHole;
        if(RelativityTag != "Damage")
        {
            Destroy(GameObject.FindWithTag(RelativityTag));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);

        if (CharacterfacingRight == true)
        {
            BulletRigidbody.velocity = new Vector2(BulletSpeed, BulletRigidbody.velocity.y);
        }
        else if (CharacterfacingRight == false)
        {
            BulletRigidbody.velocity = new Vector2(-BulletSpeed, BulletRigidbody.velocity.y);
        }
        if (distance >= 5 && RelativityTag != "Damage")
        {
            SpawnRelativity(RelativityTag, CharacterfacingRight);
        }
        else if (distance >= 15)
        {
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D ColliderObject)
    {
        if (ColliderObject.tag == "Wall")
        {
            ProCamera2DShake.Instance.Shake("PlayerHit");
            Instantiate(HitVFX, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (ColliderObject.tag == "Floor")
        {
            ProCamera2DShake.Instance.Shake("PlayerHit");
            Instantiate(HitVFX, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if(ColliderObject.tag == "Enemy")
        {
            ProCamera2DShake.Instance.Shake("PlayerHit");
            Instantiate(HitEnemyVFX, gameObject.transform.position, gameObject.transform.rotation);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void SpawnRelativity(string SpawnRelativitytag, bool PlayerShootingRight)
    {
        if (PlayerShootingRight == false)
        {
            Relativity.GetComponent<SpriteRenderer>().flipX = true;
            //ControlaAudio.instancia.PlayOneShot(RelativityShotSFX);
        }
        else
        {
            Relativity.GetComponent<SpriteRenderer>().flipX = false;
        }
     
        Instantiate(Relativity, GunShot.transform.position, GunShot.transform.rotation);
        Destroy(gameObject);
    }

}
