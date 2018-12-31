using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Utils;


namespace GameFramework
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class ColorPickup : Pickup
    {

        public ObjectColor.color color;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        // Use this for initialization
        void Start()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = ObjectColor.getColor(color);
        }
    }
}