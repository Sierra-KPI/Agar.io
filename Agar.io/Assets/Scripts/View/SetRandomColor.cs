﻿using UnityEngine;

namespace Agario.UnityView
{
    public class SetRandomColor : MonoBehaviour
    {
        void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            Color playerColor = new Color(
                Random.Range(0f, 0.8f),
                Random.Range(0f, 0.8f),
                Random.Range(0f, 0.8f)
            );

            spriteRenderer.color = playerColor;
        }
    }
}
