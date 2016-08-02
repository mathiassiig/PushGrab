using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovementAxis : MonoBehaviour
{
    public Slider slider;
    public float Movement;

    public void Update()
    {
        Movement = slider.value;
    }

    public void OnTouch()
    {
        Movement = slider.value;
    }

    public void OnRelease()
    {
        slider.value = 0;
    }
}
