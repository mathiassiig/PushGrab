using UnityEngine;
using System.Collections;

public class GrabScript : MonoBehaviour
{
    public AttackModule Owner;
    public PlatformerCharacter2D OwnerPlatform;
    public float MaxWeight = 200f;
    public bool Lifting = false;
    Rigidbody2D LiftedObject;

    void Awake()
    {
        StartCoroutine(AutoDestroy());

    }

    public void TryLifting(Rigidbody2D rb2d)
    {
        //Check if it's a character, not implemented yet
        var player = rb2d.gameObject.GetComponent<PlatformerCharacter2D>();
        if (rb2d.mass <= MaxWeight && player == null)
        {
            Lift(rb2d);
        }
        else
        {
            //It's too heavy animation
            GiveUp();
        }
    }

    private void Lift(Rigidbody2D rb2d)
    {
        OwnerPlatform.Grabbing = true;
        rb2d.gameObject.GetComponent<Throwable>().SetLastOwner(OwnerPlatform);
        LiftedObject = rb2d;
        OwnerPlatform.m_TotalMass += LiftedObject.mass;
        var joint = LiftedObject.gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = Owner.gameObject.GetComponent<Rigidbody2D>();
        joint.enableCollision = false;
        Owner.CurrentlyGrabbing = true;
    }

    public void Release(float x = 40, float y = 40)
    {
        Destroy(LiftedObject.GetComponent<FixedJoint2D>());
        int offset = 1;
        if (!OwnerPlatform.m_FacingRight)
            offset = -1;
        LiftedObject.AddForce(new Vector2(x * LiftedObject.mass * offset, y * LiftedObject.mass));
        OwnerPlatform.m_TotalMass -= LiftedObject.mass;
        OwnerPlatform.Grabbing = false;
        GiveUp();
    }

    public void Throw()
    {
        Release(400);
    }

    public void GiveUp()
    {
        Owner.CurrentlyGrabbing = false;
        LiftedObject = null;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(OwnerPlatform == null)
        {
            OwnerPlatform = Owner.GetComponent<PlatformerCharacter2D>();
        }
        if (!Lifting)
        {
            var rb2d = collider.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                Lifting = true;
                TryLifting(rb2d);
            }
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (!Lifting)
            Destroy(gameObject);
    }
}
