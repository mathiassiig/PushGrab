using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour
{
    public bool m_Stunned;
    public float StunTime;
    public float Health;
    public float MaxHealth;
    public bool Dead = false;
    public float SpeedDiff = 1000f;
    public Rigidbody2D self_rb2d;

    void Start()
    {
        self_rb2d = GetComponent<Rigidbody2D>();
    }

    public void Damage(float amount, bool stuns, GlobalDefinitions.DMGTYPE type) //add direction
    {
        Health -= amount;
        TakeVisualDamage();
        if(stuns)
            StartCoroutine(StartStun());
        switch (type)
        {
            case GlobalDefinitions.DMGTYPE.BLUNT:
            case GlobalDefinitions.DMGTYPE.PIERCING:
            case GlobalDefinitions.DMGTYPE.SLASHING:
                Bleed();
                break;
        }

        if (Health <= 0)
            Death(type);
    }

    IEnumerator StartStun()
    {
        m_Stunned = true;
        yield return new WaitForSeconds(StunTime);
        m_Stunned = false;
    }

    public void TakeVisualDamage()
    {

    }

    //add direction
    public void Bleed()
    {

    }


    public void Death(GlobalDefinitions.DMGTYPE type)  //add direction
    {
        Dead = true;
        switch (type)
        {
            case GlobalDefinitions.DMGTYPE.BLUNT:
                break;
            case GlobalDefinitions.DMGTYPE.EXPLOSION:
                break;
            case GlobalDefinitions.DMGTYPE.FIRE:
                break;
            case GlobalDefinitions.DMGTYPE.PIERCING:
                break;
        }
        FindObjectOfType<GameMaster>().PlayerDied(gameObject.GetComponent<PlatformerCharacter2D>());
    }

    public void Heal(float amount)
    {
        Health += amount;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //Check for direction
        float speed = self_rb2d.velocity.magnitude;
        Throwable throwable = collision.gameObject.GetComponent<Throwable>();
        float diff = 0;
        if(throwable != null)
        {
            diff = Mathf.Abs(throwable.CurrentVelocityMagnitude - speed);
            if(diff > SpeedDiff && throwable.CurrentVelocityMagnitude > SpeedDiff)
            {
                //Debug.Log(diff * rb2d.mass);
                Damage(diff * throwable.rb2d.mass, true, throwable.type);
            }
        }
    }
}
