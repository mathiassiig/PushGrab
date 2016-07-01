using UnityEngine;
using System.Collections;

public class LayerTouching : MonoBehaviour
{
    public int Layer;
    public bool Touching;
    public int NumberOfColliders;

    void FixedUpdate()
    {
        if (NumberOfColliders == 0)
            Touching = false;
        else
            Touching = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.gameObject.layer == Layer)
        {
            NumberOfColliders++;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == Layer)
        {
            NumberOfColliders--;
        }
    }
}
