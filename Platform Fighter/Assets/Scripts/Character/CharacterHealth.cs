using UnityEngine;
using System.Collections;

public class CharacterHealth : Health
{
    public bool m_Stunned;
    public float StunTime;
    public bool Dead = false;
    public float SpeedDiff = 1000f;
    public Rigidbody2D self_rb2d;
    public CharacterVisuals visuals;

    void Start()
    {
        self_rb2d = GetComponent<Rigidbody2D>();
        visuals = GetComponent<CharacterVisuals>();
    }

    public override void Damage(float amount, bool stuns, GlobalDefinitions.DMGTYPE type, Vector2 direction)
    {
        TakeVisualDamage();
        if (stuns)
            StartCoroutine(StartStun());
        Bleed();
        HP -= amount;
        if (HP <= 0)
        {
            visuals.VisualDeath(type, direction, Mathf.Abs(HP));
            Death(type, direction);
        }
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


    public override void Death(GlobalDefinitions.DMGTYPE type, Vector2 direction)  //add direction
    {
        Dead = true;
        FindObjectOfType<GameMaster>().PlayerDied(gameObject.GetComponent<PlatformerCharacter2D>());
    }

    public void Heal(float amount)
    {
        HP += amount;
        if (HP > MAXHP)
            HP = MAXHP;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for direction
        float speed = self_rb2d.velocity.magnitude;
        Throwable throwable = collision.gameObject.GetComponent<Throwable>();
        float diff = 0;
        if (throwable != null)
        {
            diff = Mathf.Abs(throwable.CurrentVelocityMagnitude - speed);
            if (diff > SpeedDiff && throwable.CurrentVelocityMagnitude > SpeedDiff)
            {
                //Debug.Log(diff * rb2d.mass);
                Damage(diff * 0.05f*throwable.rb2d.mass, true, throwable.type, collision.gameObject.transform.position-transform.position);
            }
        }
    }
}
