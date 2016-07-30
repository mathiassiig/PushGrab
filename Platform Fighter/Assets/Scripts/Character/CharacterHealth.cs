using UnityEngine;
using System.Collections;

public class CharacterHealth : Health
{
    public bool m_Stunned;
    public float StunTime;
    public bool Dead = false;
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
            if(visuals)
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
        
        FindObjectOfType<GameMaster>().PlayerDied();
    }

    public void Heal(float amount)
    {
        HP += amount;
        if (HP > MAXHP)
            HP = MAXHP;
    }
}
