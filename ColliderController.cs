using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderController : MonoBehaviour
{
    public Slider Fuel;
    public bool dead;
    public SFX sfx;
    CharacterControl Controller;
    public ParticleSystem WetParticle;

    private void Start()
    {
        Controller = GetComponent<CharacterControl>();
    }

    void Update() {
        if(Fuel.value <= 0f)
            dead = true;
    }

    IEnumerator Hit()
    {
        Fuel.value -= 5f;
        Controller.topSpeed = 3;
        yield return new WaitForSeconds(.2f);
        Controller.topSpeed = 10;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            Controller.topSpeed = 3;
            WetParticle.Stop();
            //Fuel.value = 0f;
            //sfx.Death_Sound();
            //dead = true;
        }
        if (other.gameObject.tag == "EnemyHit")
        {
            StartCoroutine (Hit());
        }
        if (other.gameObject.tag == "Hazard")
        {
            Fuel.value = 0f;
            sfx.Death_Sound();
            dead = true;
        }

        if (other.gameObject.tag == "Heal")
        {
            Fuel.value += 10f;
            Destroy(other.gameObject);
        }

    }//END ON TRIGGER ENTER

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Controller.topSpeed = 10;
            WetParticle.Play();
        }
    }

}
