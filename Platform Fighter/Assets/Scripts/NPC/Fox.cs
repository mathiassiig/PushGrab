using UnityEngine;
using System.Collections.Generic;

public class Fox : Health
{
    public List<Transform> Path = new List<Transform>();
    public float MaxSpeed;
    public float DamageAmount;
    public Rigidbody2D m_Rigidbody2D;
    private int PathTarget = 0;
    private Health KillableTarget;

    private bool Killing = false;

    // Update is called once per frame
    void Update()
    {
        if (!KillableTarget && Killing == true)
        {
            Killing = false;
            PathTarget++;
        }
        if (PathTarget < Path.Count)
        {
            if (KillableTarget)
            {
                KillableTarget.Damage(DamageAmount);
            }
            else
            {
                FollowPath();
            }
        }
    }

    private void Move(int direction)
    {
        m_Rigidbody2D.velocity = new Vector2(direction * MaxSpeed, m_Rigidbody2D.velocity.y);
    }

    private void FollowPath()
    {
        Transform target = Path[PathTarget];
        float dis = target.position.x - transform.position.x;
        if (Mathf.Abs(dis) > 1.6f)
        {
            int dir = FindTargetDirection(dis);
            Move(dir);
        }
        else
        {
            var health = target.gameObject.GetComponent<Health>();
            if (health == null)
            {
                PathTarget++;
            }
            else
            {
                KillableTarget = health;
                Killing = true;
            }
        }
    }

    public override void Damage(float amount, bool stuns, GlobalDefinitions.DMGTYPE type, Vector2 direction)
    {
        HP -= amount;
        if (HP <= 0)
        {
            Death(type, direction);
        }
    }

    public override void Death(GlobalDefinitions.DMGTYPE type, Vector2 direction)
    {
        Destroy(gameObject);
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
    private int FindTargetDirection(float dis)
    {
        int dir = 1;
        if (dis < 0)
            dir = -1;
        return dir;
    }
}
