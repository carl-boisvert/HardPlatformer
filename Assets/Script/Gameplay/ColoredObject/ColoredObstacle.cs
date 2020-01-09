using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Utils;
using GameFramework.Entities.Character.Player.Controller;

namespace Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class ColoredObstacle : MonoBehaviour
    {

        public ObjectColor.color objectColor;

        protected SpriteRenderer _spriteRenderer;
        protected Rigidbody2D _rb;

        protected bool isActive = true;

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        protected void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GameEventSystem.OnPlayerChangeColorEvent += OnPlayerChangeColor;
            GameEventSystem.OnPlayerDied += OnPlayerDied;
        }

        protected void OnPlayerDied() {
            if (gameObject.tag != "Wall" && gameObject.tag != "WallWalking") {
                Destroy(gameObject);
            }
        }

        protected void OnDestroy()
        {
            GameEventSystem.OnPlayerChangeColorEvent -= OnPlayerChangeColor;
            GameEventSystem.OnPlayerDied -= OnPlayerDied;
        }

        protected virtual void OnPlayerChangeColor(PlayerController playerController, ObjectColor.color color)
        {
            if (objectColor == color)
            {
                Physics2D.IgnoreCollision(playerController.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
                isActive = false;
            }
            else
            {
                Physics2D.IgnoreCollision(playerController.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
                isActive = true;
            }
        }


    }
}