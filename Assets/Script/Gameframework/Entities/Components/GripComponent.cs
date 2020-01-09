using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Character.Component
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CharacterStateComponent))]
    public class GripComponent : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private CharacterStateComponent _characterStateComponent;
        private Vector2 _wallPosition;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _characterStateComponent = GetComponent<CharacterStateComponent>();
        }

        public void Grip(Vector2 objectToGrip)
        {
            if (_characterStateComponent.CurrentPlayerMovementState == CharacterStateComponent.PlayerMovementState.JUMPING) {
                if (objectToGrip.x > gameObject.transform.position.x)
                {
                    _wallPosition = Vector2.right;
                }
                else
                {
                    _wallPosition = Vector2.left;
                }
                _characterStateComponent.CurrentPlayerMovementState = CharacterStateComponent.PlayerMovementState.ONWALL;
            }
        }

        public void UnGrip() {
            _rb.drag = 1;
            _characterStateComponent.CurrentPlayerMovementState = CharacterStateComponent.PlayerMovementState.JUMPING;
        }

        public Vector2 WallPosition
        {
            get
            {
                return _wallPosition;
            }
        }
    }
}