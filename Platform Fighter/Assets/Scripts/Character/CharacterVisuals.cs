using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterVisuals : MonoBehaviour
{
    float BlinkDelay;
    public Animator animator;
    public GameObject gib_CrushedAbove;
    public GameObject gib_CrushedFromSide;
    public GameObject gib_Blood;
    public List<GameObject> gib_Gores;

    void Update()
    {
        UpdateBlink();
    }

    void UpdateBlink()
    {
        BlinkDelay -= Time.deltaTime;
        if (BlinkDelay <= 0)
        {
            animator.SetTrigger("BlinkNow");
            BlinkDelay = Random.Range(GlobalDefinitions.ANIM_CHARACTER_BLINK_MIN_DELAY, GlobalDefinitions.ANIM_CHARACTER_BLINK_MAX_DELAY);
        }
    }

    void BluntDeath(GlobalDefinitions.DIRECTION dir, float severity)
    {
        Debug.Log(severity);
        if (severity > GlobalDefinitions.VIS_CHARACTER_BLUNT_SEVERITY_LIMIT)
        {
            //SpawnRandomBlood(2);
            //SpawnRandomGore(2);
        }
        else
        {
            SpawnRandomBlood(dir);
            //SpawnRandomGore();
            switch (dir)
            {
                case GlobalDefinitions.DIRECTION.UP:
                case GlobalDefinitions.DIRECTION.DOWN:
                    SpawnGib(gib_CrushedAbove, true);
                    break;
                case GlobalDefinitions.DIRECTION.LEFT:
                case GlobalDefinitions.DIRECTION.RIGHT:
                    SpawnGib(gib_CrushedFromSide, true);
                    break;
            }
        }
    }

    void SpawnRandomBlood(GlobalDefinitions.DIRECTION dir, int multiplier = 1)
    {
        //dir = GlobalDefinitions.OppositeDirection(dir);
        int rand = Random.Range(GlobalDefinitions.VIS_CHARACTER_BLOOD_MIN * multiplier, GlobalDefinitions.VIS_CHARACTER_BLOOD_MAX * multiplier);
        for (int i = 0; i < rand; i++)
        {
            var instance = SpawnGib(gib_Blood, true);
            ApplyRandomForce(instance);
        }
    }

    void ApplyRandomForce(GameObject instance, GlobalDefinitions.DIRECTION dir = GlobalDefinitions.DIRECTION.UNIFORM)
    {
        Vector2 forceX = new Vector2(-1, 1);
        Vector2 forceY = new Vector2(-1, 1);
        switch(dir)
        {
            case GlobalDefinitions.DIRECTION.UP:
                forceY = new Vector2(0.5f, 1);
                break;
            case GlobalDefinitions.DIRECTION.DOWN:
                forceY = new Vector2(-1, -0.5f);
                break;
            case GlobalDefinitions.DIRECTION.LEFT:
                forceX = new Vector2(-1, -0.5f);
                break;
            case GlobalDefinitions.DIRECTION.RIGHT:
                forceX = new Vector2(0.5f, 1);
                break;
        }
        var rb2d = instance.GetComponent<Rigidbody2D>();
        float multiplier = rb2d.mass * 1500;
        Vector2 force = new Vector2(multiplier * Random.Range(forceX.x, forceX.y), multiplier * Random.Range(forceY.x, forceY.y));
        rb2d.AddForce(force);

    }

    void SpawnRandomGore(int multiplier = 4)
    {
        int rand = Random.Range(GlobalDefinitions.VIS_CHARACTER_GORE_MIN * multiplier, GlobalDefinitions.VIS_CHARACTER_GORE_MAX * multiplier);
        for (int i = 0; i < rand; i++)
        {
            SpawnGib(gib_Gores[Random.Range(0, gib_Gores.Count)], true);
        }
    }

    public void VisualDeath(GlobalDefinitions.DMGTYPE type, Vector2 direction, float severity)
    {
        GlobalDefinitions.DIRECTION dir = GlobalDefinitions.GetDirection(direction);
        switch (type)
        {

            case GlobalDefinitions.DMGTYPE.BLUNT:
                BluntDeath(dir, severity);
                break;
            case GlobalDefinitions.DMGTYPE.EXPLOSION:
                break;
            case GlobalDefinitions.DMGTYPE.FIRE:
                break;
            case GlobalDefinitions.DMGTYPE.PIERCING:
                break;
        }
    }

    GameObject SpawnGib(GameObject gibPrefab, bool col)
    {
        var instance = Instantiate(gibPrefab, transform.position, Quaternion.identity) as GameObject;
        if(col)
            instance.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        return instance;
    }
}
