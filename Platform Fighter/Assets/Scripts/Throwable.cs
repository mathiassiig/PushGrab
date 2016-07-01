using UnityEngine;
using System.Collections;

public class Throwable : MonoBehaviour
{
    public DamageDefinitions.DMGTYPE type;
    public float CurrentVelocityMagnitude;
    public Rigidbody2D rb2d;
    public CustomCamera cam;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<CustomCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        float LastVelocity = CurrentVelocityMagnitude;
        CurrentVelocityMagnitude = rb2d.velocity.magnitude;
        CheckForScreenShake(LastVelocity, CurrentVelocityMagnitude);
    }

    void CheckForScreenShake(float last, float current)
    {
        if (rb2d.mass > 150)
        {
            if (last > 5)
            {
                float velDiff = Mathf.Abs(current - last);
                if (velDiff > 5)
                {
                    cam.ShakeAmount = (rb2d.mass / 300) * (velDiff / 40);
                }
            }
        }
    }
}
