using UnityEngine;
using System.Collections;

public class RespawnPoint : MonoBehaviour
{
    public bool Pristine = true;

    public void Respawn(PlatformerCharacter2D player)
    {
        player.transform.position = transform.position;
        RespawnEffects();
        player.gameObject.SetActive(true);
        player.GetComponent<CharacterHealth>().m_Stunned = false;
        Pristine = false;
    }

    private void RespawnEffects()
    {

    }

    private IEnumerator PristineDelay()
    {
        yield return new WaitForSeconds(GlobalDefinitions.RESPAWN_PRISTINE_DELAY);
        Pristine = true;
    }
}
