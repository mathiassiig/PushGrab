using UnityEngine;
using System.Collections;

public static class DamageDefinitions
{
    public enum DMGTYPE
    {
        FIRE,
        PIERCING,
        BLUNT,
        EXPLOSION,
        SLASHING
    };
}

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

    public void Damage(float amount, bool stuns, DamageDefinitions.DMGTYPE type) //add direction
    {
        Health -= amount;
        TakeVisualDamage();
        if(stuns)
            StartCoroutine(StartStun());
        switch (type)
        {
            case DamageDefinitions.DMGTYPE.BLUNT:
            case DamageDefinitions.DMGTYPE.PIERCING:
            case DamageDefinitions.DMGTYPE.SLASHING:
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

    public void Bleed()
    {

    } //add direction


    public void Death(DamageDefinitions.DMGTYPE type)  //add direction
    {
        Dead = true;
        switch (type)
        {
            case DamageDefinitions.DMGTYPE.BLUNT:
                break;
            case DamageDefinitions.DMGTYPE.EXPLOSION:
                break;
            case DamageDefinitions.DMGTYPE.FIRE:
                break;
            case DamageDefinitions.DMGTYPE.PIERCING:
                break;
        }
        Destroy(gameObject);
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
