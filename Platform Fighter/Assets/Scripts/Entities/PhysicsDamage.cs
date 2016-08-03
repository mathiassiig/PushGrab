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
            float overAllSpeed = Mathf.Abs(throwable.CurrentAngularVelocity) / 180f * throwable.CurrentVelocityMagnitude;
            diff = Mathf.Abs(overAllSpeed - speed);
            if (diff > SpeedDiff && throwable.CurrentVelocityMagnitude > SpeedDiff)
            {
                float dmg = diff * 0.05f * throwable.rb2d.mass;
                //Debug.Log(dmg);
                hp.Damage(dmg, true, throwable.type, col.gameObject.transform.position - transform.position);
            }
        }
    }
}
