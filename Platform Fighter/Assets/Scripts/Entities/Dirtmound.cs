using UnityEngine;
using System.Collections;

public class Dirtmound : Health
{
    public override void Death()
    {
        Destroy(gameObject);
    }
}
