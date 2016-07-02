﻿using UnityEngine;
using System.Collections;

public class RandomSprite : MonoBehaviour
{

    public Sprite[] sprites;
    public string resourceName;
    public int currentSprite = -1;

    // Use this for initialization
    void Start()
    {
        resourceName = "Art/" + resourceName;
        sprites = Resources.LoadAll<Sprite>(resourceName);

        if (currentSprite == -1)
            currentSprite = Random.Range(0, sprites.Length);
        else if (currentSprite > sprites.Length)
            currentSprite = sprites.Length - 1;

        GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
    }
}