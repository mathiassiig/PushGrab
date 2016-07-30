using UnityEngine;
using System.Collections;

public class Bunny : Health
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Death()
    {
        Destroy(gameObject);
    }
}
