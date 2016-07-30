using UnityEngine;
using System.Collections;

public class PhysicsDamage : MonoBehaviour
{
    public Rigidbody2D self_rb2d;
    public Health hp;
    public float SpeedDiff = 8.5f;

    void OnCollisionEnter2D(Collision2D col)
    {
        float speed = self_rb2d.velocity.magnitude;
        Throwable throwable = col.gameObject.GetComponent<Throwable>();
        float diff = 0;
        if (throwable != null)
        {
            diff = Mathf.Abs(throwable.CurrentVelocityMagnitude - speed);
            if (diff > SpeedDiff && throwable.CurrentVelocityMagnitude > SpeedDiff)
            {
                hp.Damage(diff * 0.05f * throwable.rb2d.mass, true, throwable.type, col.gameObject.transform.position - transform.position);
            }
        }
    }
}
