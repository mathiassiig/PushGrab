using UnityEngine;
using System.Collections.Generic;

public class Fox : MonoBehaviour
{
    public List<Transform> Path = new List<Transform>();
    public float MaxSpeed;
    public float Damage;
    public Rigidbody2D m_Rigidbody2D;
    private int PathTarget = 0;
    private Health KillableTarget;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (KillableTarget)
        {
            KillableTarget.Damage(0.1f);
        }
        else
        {
            FollowPath();
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
            var health = target.gameObject.GetComponent<Dirtmound>();
            if (health == null)
            {
                PathTarget++;
            }
            else
            {
                KillableTarget = health;
                PathTarget++;
            }
        }
    }

    private int FindTargetDirection(float dis)
    {
        int dir = 1;
        if (dis < 0)
            dir = -1;
        return dir;
    }
}
