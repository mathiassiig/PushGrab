using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float MAXHP;
    public float HP;
    public bool Healable;
    public bool Invincible;


    public virtual void Damage(float amount)
    {
        HP -= amount;
        if(HP <= 0)
        {
            Death();
        }
    }

    public virtual void Damage(float amount, bool stuns, GlobalDefinitions.DMGTYPE type, Vector2 direction) //add direction
    {
        Damage(amount);
    }

    public virtual void Death()
    {

    }

    public virtual void Death(GlobalDefinitions.DMGTYPE type, Vector2 direction)
    {

    }
}