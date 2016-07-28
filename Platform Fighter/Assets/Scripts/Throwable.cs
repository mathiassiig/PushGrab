using UnityEngine;
using System.Collections;

public class Throwable : MonoBehaviour
{
    public GlobalDefinitions.DMGTYPE type;
    public float CurrentVelocityMagnitude;
    public Rigidbody2D rb2d;
    public Camera2DFollow cam;
    public PlatformerCharacter2D LastOwner;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera2DFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        float LastVelocity = CurrentVelocityMagnitude;
        CurrentVelocityMagnitude = rb2d.velocity.magnitude;
        CheckForScreenShake(LastVelocity, CurrentVelocityMagnitude);
    }

    public void SetLastOwner(PlatformerCharacter2D character)
    {
        LastOwner = character;
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
