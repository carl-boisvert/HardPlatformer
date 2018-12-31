using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Utils;
using GameFramework.Character;
using GameFramework.Gameplay;
using GameFramework.GameMode.Objectives;


namespace GameFramework.Character.Player
{

    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {

        public float horizontalSpeed = 5.0f;
        public float verticalSpeed = 40.0f;
        public float jumpHeight = 3.0f;
        public float deadZone = 0.2f;
        public ObjectColor.color playerColor;
        public SpriteRenderer playerSprite;
        public Checkpoint checkpoint;


        private enum PlayerMovementState
        {
            JUMPING,
            FALLING,
            WALKING,
            DEAD,
            WON
        }

        private PlayerMovementState _playerMovementState = PlayerMovementState.FALLING;
        private float _currentJumpHeight;
        private Rigidbody2D _rigidbody;

        // Use this for initialization
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            playerSprite.color = ObjectColor.getColor(playerColor);

            if (checkpoint != null)
            {
                OnPlayerDied();
            }

            EventSystem.OnPlayerDied += OnPlayerDied;
            EventSystem.OnPickupEvent += OnPickup;
            EventSystem.OnPlayerDied += OnPlayerDied;
            EventSystem.OnPlayerPassCheckpoint += OnPlayerPassCheckpoint;
            EventSystem.OnPlayerChangeColorEvent(this);
            EventSystem.OnPlayerWon += OnPlayerWon;
        }


        // Update is called once per frame
        void Update()
        {
            if (!IsGameFinished())
            {
                if (CanPlayerJump() && IsPlayerJumping())
                {
                    _currentJumpHeight = jumpHeight + gameObject.transform.position.y;
                    _playerMovementState = PlayerMovementState.JUMPING;
                }

                if(_playerMovementState == PlayerMovementState.JUMPING && !IsPlayerJumping()){
                     _playerMovementState = PlayerMovementState.FALLING;
                }

                if (_playerMovementState == PlayerMovementState.JUMPING)
                {

                    MoveWithJump();
                    if (IsMaxHeight())
                    {
                        _playerMovementState = PlayerMovementState.FALLING;
                    }
                }
                else
                {
                    Move();
                }
            }
         }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //TODO Ajouté la détection sur le coté du cube pour le mettre à Falling
            if (collision.gameObject.tag == "Walking" || collision.gameObject.tag == "WallWalking")
            {
                if (collision.gameObject.tag.Contains("Roof"))
                {
                    _playerMovementState = PlayerMovementState.FALLING;
                }
                else
                {
                    _playerMovementState = PlayerMovementState.WALKING;
                    _currentJumpHeight = gameObject.transform.position.y;
                }

            }
        }

        void OnPickup(Pickup pickup)
        {
            if (pickup as ColorPickup)
            {
                var pickupColor = pickup as ColorPickup;

                playerSprite.color = ObjectColor.getColor(pickupColor.color);
                playerColor = pickupColor.color;
                EventSystem.OnPlayerChangeColorEvent(this);
            }
        }

        void OnPlayerDied()
        {
            _playerMovementState = PlayerMovementState.DEAD;
            _rigidbody.velocity = Vector2.zero;
            gameObject.transform.position = checkpoint.gameObject.transform.position;
        }

        void OnPlayerPassCheckpoint(Checkpoint checkpointPassed)
        {
            checkpoint = checkpointPassed;
        }

        void OnPlayerWon()
        {
            _playerMovementState = PlayerMovementState.WON;
        }

        void MoveWithJump()
        {
            gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime);
        }

        void Move()
        {
            gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, 0);
        }

        bool IsMaxHeight()
        {
            return gameObject.transform.position.y >= _currentJumpHeight - deadZone;
        }

        bool IsGameFinished()
        {
            return _playerMovementState == PlayerMovementState.DEAD || _playerMovementState == PlayerMovementState.WON;
        }

        bool IsPlayerWalking()
        {
            return _playerMovementState == PlayerMovementState.WALKING;
        }

        bool IsPlayerJumping()
        {
            return Input.GetButton("Jump");
        }

        bool CanPlayerJump()
        {
            return IsPlayerWalking();
        }
    }
}