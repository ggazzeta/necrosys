using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativityShot : MonoBehaviour
{
    public GameObject RelativitySpeed;
    public GameObject RelativitySlow;
    public GameObject RelativityPause;
    public GameObject RelativityDamage;
    public GameObject Gun;
    public ParticleSystem ShotVFX;
    private CharacterControl PlayerCharacterControl;
    string Bullet;

    public SFX sfx;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        PlayerCharacterControl = GameObject.FindWithTag("Player").GetComponent<CharacterControl>();
    }
    public void Shot()
    {
        Bullet = PlayerCharacterControl.BlackHole;
        if (PlayerCharacterControl.BlackHole != "")
        {
            //sfx.shottingBH();
            BlackHoleSpawn(Bullet);
        }
    }
    public void BlackHoleSpawn(string BlackHoleBullet)
    {
        if (BlackHoleBullet == "Damage" && PlayerCharacterControl.IsDamageFull == true)
        {
            if (PlayerCharacterControl.facingRight == false)
            {
                RelativityDamage.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                RelativityDamage.GetComponent<SpriteRenderer>().flipX = false;
            }
            Instantiate(RelativityDamage, Gun.transform.position, Gun.transform.rotation);
            ShotVFX.Play();
            PlayerCharacterControl.IsDamageFull = false;
        }
            if (BlackHoleBullet == "Speed" && PlayerCharacterControl.IsSpeedFull == true)
        {
            if(PlayerCharacterControl.facingRight == false)
            {
                RelativitySpeed.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                RelativitySpeed.GetComponent<SpriteRenderer>().flipX = false;
            }
            Instantiate(RelativitySpeed, Gun.transform.position, Gun.transform.rotation);
            PlayerCharacterControl.IsSpeedFull = false;
            PlayerCharacterControl.SpeedAnimator.SetBool("IsFull", false);
        }
        if (BlackHoleBullet == "Slow" && PlayerCharacterControl.IsSlowFull == true)
        {
            if (PlayerCharacterControl.facingRight == false)
            {
                RelativitySlow.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                RelativitySlow.GetComponent<SpriteRenderer>().flipX = false;
            }
            Instantiate(RelativitySlow, Gun.transform.position, Gun.transform.rotation);
            PlayerCharacterControl.IsSlowFull = false;
            PlayerCharacterControl.SlowAnimator.SetBool("IsFull", false);
        }
        if (BlackHoleBullet == "Pause" == true && PlayerCharacterControl.IsPauseFull == true)
        {
            if (PlayerCharacterControl.facingRight)
            {
                RelativityPause.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                RelativityPause.GetComponent<SpriteRenderer>().flipX = false;
            }
            Instantiate(RelativityPause, Gun.transform.position, Gun.transform.rotation);
            PlayerCharacterControl.IsPauseFull = false;
            PlayerCharacterControl.PauseAnimator.SetBool("IsFull", false);
        }
    }
}
