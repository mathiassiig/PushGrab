using UnityEngine;
using System.Collections;

public class JumpButton : MonoBehaviour
{
    public bool JumpNow = false;

    public void OnClick()
    {
        JumpNow = true;
    }
}
