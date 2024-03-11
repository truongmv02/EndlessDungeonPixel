using System.Collections;
using UnityEngine;

public class OptionalSpriteMarket : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
