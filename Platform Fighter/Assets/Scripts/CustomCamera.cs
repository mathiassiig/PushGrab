using UnityEngine;
using System.Collections;

public class CustomCamera : MonoBehaviour
{
    public Vector3 CurrentOriginalPosition;
    public float ShakeAmount = 0;

    void Start()
    {
        CurrentOriginalPosition = transform.position;
    }

    void Update()
    {
        //Handle some camera movement
        transform.position = CurrentOriginalPosition;
        if (ShakeAmount >= 0)
            ShakeScreen(ShakeAmount);
    }

    void ShakeScreen(float amount)
    {
        float extraX = Random.Range(-amount, amount);
        float extraY = Random.Range(-amount, amount);
        transform.position = new Vector3(CurrentOriginalPosition.x + extraX, CurrentOriginalPosition.y + extraY, CurrentOriginalPosition.z);
        ShakeAmount -= Time.deltaTime;
    }
}
