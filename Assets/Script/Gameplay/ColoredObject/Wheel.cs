using System.Collections;
using System.Collections.Generic;
using GameFramework.Entities.Character.Player.Controller;
using GameFramework.Utils;
using UnityEngine;

using GameFramework;


namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Wheel : ColoredObstacle
    {
        public float speed;
        public float maxSpeeed;
        public bool isStatic = false;

        private void Update()
        {
            if (!isStatic) {
                _rb.AddTorque(speed);
            }
        }

        public void ChangeColor(ObjectColor.color color) {
            objectColor = color;
            _spriteRenderer.color = ObjectColor.getColor(color);
        }

        
    }
}