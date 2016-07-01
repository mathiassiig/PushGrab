using UnityEngine;
using System.Collections;

public class AttackModule : MonoBehaviour
{
    bool CanPush = true;
    bool CanGrab = true;
    public bool CurrentlyGrabbing = false;
    GrabScript CurrentGrabInstance;
    public GameObject PushPrefab;
    public GameObject GrabPrefab;
    public float PushDelay = 0.5f;
    public float GrabDelay = 0.5f;
    public PlatformerCharacter2D Character;

    void Start ()
    {
        Character = GetComponent<PlatformerCharacter2D>();
    }

    public void TryAttack()
    {
        if(CurrentlyGrabbing)
        {
            CurrentGrabInstance.Throw();
        }
        else if(CanPush)
        {
            CanPush = false;
            StartCoroutine(PushDelayer());
            Push();
        }
    }

    public void TryGrab()
    {
        if(CanGrab)
        {
            if (!CurrentlyGrabbing)
            {
                CanGrab = false;
                StartCoroutine(GrabDelayer());
                Grab();
            }
            else
            {
                CanGrab = false;
                CurrentlyGrabbing = false;
                StartCoroutine(GrabDelayer());
                CurrentGrabInstance.Release();
            }
        }
    }

    private void Push()
    {
        int offset = 1;
        if(!Character.m_FacingRight)
            offset = -1;
        Vector2 pos = Character.transform.position;
        pos.x += offset;
        var instance = Instantiate(PushPrefab, pos, Quaternion.identity) as GameObject;
        var pushinstance = instance.GetComponent<DefaultWeapon>();
        pushinstance.Force *= offset;
        pushinstance.Owner = gameObject;
        pushinstance.Enabled = true;
    }

    private void Grab()
    {
        int offset = 1;
        if (!Character.m_FacingRight)
            offset = -1;
        Vector2 pos = Character.transform.position;
        pos.x += offset;
        var instance = Instantiate(GrabPrefab, pos, Quaternion.identity) as GameObject;
        CurrentGrabInstance = instance.GetComponent<GrabScript>();
        CurrentGrabInstance.gameObject.transform.SetParent(gameObject.transform);
        CurrentGrabInstance.Owner = this;
        CurrentGrabInstance.OwnerPlatform = gameObject.GetComponent<PlatformerCharacter2D>();
    }

    IEnumerator PushDelayer()
    {
        yield return new WaitForSeconds(PushDelay);
        CanPush = true;
    }

    IEnumerator GrabDelayer()
    {
        yield return new WaitForSeconds(GrabDelay);
        CanGrab = true;
    }
}
