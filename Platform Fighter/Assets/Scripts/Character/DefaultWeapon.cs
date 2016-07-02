using UnityEngine;
using System.Collections;

public class DefaultWeapon : MonoBehaviour
{
    public bool Enabled = true;
    public Vector2 Force;
    public GameObject Owner;

    void Awake()
    {
        StartCoroutine(DisableDamage());
    }

    IEnumerator DisableDamage()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == Owner)
            return;
        if(Enabled)
        {
            Rigidbody2D rb2d = collider.gameObject.GetComponent<Rigidbody2D>();
            CharacterHealth character = collider.gameObject.GetComponent<CharacterHealth>();
            if (rb2d != null)
                rb2d.AddForce(Force);
            
        }
    }
}
