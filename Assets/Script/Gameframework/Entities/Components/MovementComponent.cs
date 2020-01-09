

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Character.Component
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Collider2D))]
    public class MovementComponent : MonoBehaviour
    {

        public float accelerationSpeed = 5f;
        public float maxSpeed = 100f;
        public float decelerationSpeed = 50f;


        private Rigidbody2D _rb;
        private Collider2D _col;
        private Animator _animator;
        private Vector2 _currentScale = new Vector2(1, 1);
        private CharacterStateComponent _characterStateComponent;
        private Color _headRayColor = Color.white;
        private Color _midRayColor = Color.white;
        private Color _footRayColor = Color.white;


        //RayCast Variables
        private float _rayDistance;
        private float _rayDistanceDiagonal;
        private Vector3 _head;
        private Vector3 _middle;
        private Vector3 _foot;
        Vector3 upVector;
        Vector3 downVector;
        Vector3 leftVector;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _col = GetComponent<Collider2D>();
            _characterStateComponent = GetComponent<CharacterStateComponent>();
            _rayDistance = _col.bounds.extents.x;
            _rayDistanceDiagonal = _col.bounds.extents.x + _col.bounds.extents.y;
        }

        public void Move()
        {
            //Update variable for Raycast
            _middle = _col.bounds.center;
            _head = _col.bounds.max;
            _foot = _col.bounds.min;
            upVector = Vector2.up * _col.bounds.extents.y;
            downVector = Vector2.down * _col.bounds.extents.y;
            leftVector = Vector2.left * _col.bounds.extents.x;

            if (Input.GetAxis("Horizontal") < 0)
            {
                _currentScale = new Vector2(1, 1);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                _currentScale = new Vector2(-1, 1);
            }
            
            gameObject.transform.localScale = _currentScale;
            if (CanMove())
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    _animator.SetBool("IsRunning", true);
                    
                    //Thank you Thegosu2
                    _rb.velocity = new Vector2(Mathf.Sign(Input.GetAxis("Horizontal")) * EasingFunction.EaseOutExpo(_rb.velocity.x, maxSpeed, accelerationSpeed), _rb.velocity.y);                        
                }
                else
                {
                    if (_characterStateComponent.CurrentPlayerMovementState == CharacterStateComponent.PlayerMovementState.WALKING)
                    {
                        _rb.velocity = new Vector2(EasingFunction.EaseOutQuart(_rb.velocity.x, 0, decelerationSpeed), _rb.velocity.y);
                    }
                    else {
                        _rb.velocity = new Vector2(EasingFunction.EaseOutExpo(_rb.velocity.x, 0, accelerationSpeed), _rb.velocity.y);
                    }
                    
                    _animator.SetBool("IsRunning", false);
                }
            }
            else
            {
                _animator.SetBool("IsRunning", false);
            }
        }

        private bool CanMove() {
            RaycastHit2D hitMiddle = Physics2D.Raycast(_middle, leftVector * _currentScale, 1.1f, (LayerMask.GetMask("Wall") | LayerMask.GetMask("Floor")));
            RaycastHit2D hitHead = Physics2D.Raycast(_middle, (upVector + leftVector) * _currentScale, 1.1f, (LayerMask.GetMask("Wall") | LayerMask.GetMask("Floor")));
            RaycastHit2D hitFoot = Physics2D.Raycast(_middle, (downVector + leftVector) * _currentScale, 1.1f, (LayerMask.GetMask("Wall") | LayerMask.GetMask("Floor")));
            RaycastHit2D hitFloor = Physics2D.Raycast(_middle, downVector, 1.1f, LayerMask.GetMask("Floor"));

            // Does the ray intersect any objects excluding the player layer
            if (hitMiddle.collider != null || hitHead.collider != null || (hitFoot.collider != null && hitFloor.collider == null))
            {
                if (hitMiddle.collider != null)
                {
                    _midRayColor = Color.red;
                }
                if (hitHead.collider != null)
                {
                    _headRayColor = Color.red;
                }
                if (hitFoot.collider != null)
                {
                    _footRayColor = Color.red;
                }

                return false;
            }
            _midRayColor = Color.white;
            _headRayColor = Color.white;
            _footRayColor = Color.white;
            return true;
        }

        private void OnDrawGizmos()
        {
            DrawDebugLine();
        }

        private void DrawDebugLine(){
            UnityEngine.Debug.DrawRay(_middle, leftVector * _currentScale, _midRayColor, 0.01f);
            UnityEngine.Debug.DrawRay(_middle, (upVector + leftVector) * _currentScale, _headRayColor, 0.01f);
            UnityEngine.Debug.DrawRay(_middle, (downVector + leftVector) * _currentScale, _footRayColor, 0.01f);
        }

    }
}