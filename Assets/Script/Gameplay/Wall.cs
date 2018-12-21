using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Utils;
using GameFramework.Character.Player;

namespace GameFramework.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Wall : MonoBehaviour
    {

        public ObjectColor.color color;
        private SpriteRenderer _spriteRenderer;

        // Use this for initialization
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = ObjectColor.getColor(color);
            EventSystem.OnPlayerChangeColorEvent += OnPlayerChangeColor;
        }

        void OnPlayerChangeColor(PlayerController playerController)
        {
            if (playerController.playerColor == color)
            {
                Physics2D.IgnoreCollision(playerController.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            }
            else
            {
                Physics2D.IgnoreCollision(playerController.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
            }
        }
    }
}