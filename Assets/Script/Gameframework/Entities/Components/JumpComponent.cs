using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR || DEVELOPMENT_BUILD
using GameFramework.Debug;
#endif

namespace GameFramework.Character.Component
{
    //Todo Fix this, component shouldn't have dependencies between them, that's what CharacterState is for
    [RequireComponent(typeof(Rigidbody2D), typeof(CharacterStateComponent), typeof(Animator))]
    public class JumpComponent : MonoBehaviour
    {

        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
        public float verticalSpeed = 20.0f;
        public float verticalAcceleration = 5f;

        private bool _isJumping = false;

        private Rigidbody2D _rb;
        private CharacterStateComponent _characterStateComponent;
        private GripComponent _gripComponent;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _characterStateComponent = GetComponent<CharacterStateComponent>();
            _gripComponent = GetComponent<GripComponent>();
        }

        private void Update()
        {
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

        public void Jump()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (DebugManager.Instance && DebugManager.Instance.isPlayerCanJump) {
                //UnityEngine.Debug.Log("Is Player Able To Jump? " + (CanPlayerJump()));
                if (!CanPlayerJump()) {
                    UnityEngine.Debug.Log("Player Velocity " + _rb.velocity.y);
                    UnityEngine.Debug.Log("Player State " + _characterStateComponent.CurrentPlayerMovementState);
                }
            }
#endif
            if (IsPlayerJumping() && CanPlayerJump())
            {
                if(_characterStateComponent.CurrentPlayerMovementState == CharacterStateComponent.PlayerMovementState.ONWALL) {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    if (DebugManager.Instance.wallJump) {
                        UnityEngine.Debug.Log((Vector2.up + -(_gripComponent.WallPosition)) * (verticalSpeed));
                    }
#endif              
                    _rb.velocity += new Vector2(0, EasingFunction.Linear(_rb.velocity.y, verticalSpeed, verticalAcceleration));
                    //_rb.velocity += (Vector2.up * (verticalSpeed / 8) + -(_gripComponent.WallPosition)) * (verticalSpeed/2);
                }
                else {
                    _rb.velocity += new Vector2(0, EasingFunction.Linear(_rb.velocity.y, verticalSpeed, verticalAcceleration));
                    //_rb.velocity += Vector2.up * verticalSpeed;
                }
                _characterStateComponent.CurrentPlayerMovementState = CharacterStateComponent.PlayerMovementState.JUMPING;
            }
        }

        private bool CanPlayerJump()
        {
            return _characterStateComponent.CurrentPlayerMovementState == CharacterStateComponent.PlayerMovementState.WALKING 
                || _characterStateComponent.CurrentPlayerMovementState == CharacterStateComponent.PlayerMovementState.ONWALL;
        }

        private bool IsPlayerJumping()
        {
            return Input.GetButtonDown("Jump");
        }
    }
}