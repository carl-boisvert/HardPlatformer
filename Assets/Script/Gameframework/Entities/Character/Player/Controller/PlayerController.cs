using UnityEngine;

using System.Collections.Generic;

using GameFramework.Utils;
using GameFramework.Character.Component;
using GameFramework.Debug;
using GameFramework.SceneManager;
using GameFramework.GameMode;
using GameFramework.Sound;

using Anima2D;

namespace GameFramework.Entities.Character.Player.Controller
{

    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public SpriteMeshInstance topHair;
        public SpriteMeshInstance ponyTail;
        public List<AudioClip> footstepAudioClips;

        private StoryMode gameMode;
        private Rigidbody2D _rigidbody;
        private JumpComponent _jumpComponent;
        private MovementComponent _movementComponent;
        private GripComponent _gripComponent;
        private CharacterStateComponent _characterStateComponent;
        private Collider2D _col;
        private bool _canMove = true;

        // Use this for initialization
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _characterStateComponent = GetComponent<CharacterStateComponent>();
            _gripComponent = GetComponent<GripComponent>();
            _col = GetComponent<Collider2D>();  

            if (GetComponent<JumpComponent>()) {
                _jumpComponent = GetComponent<JumpComponent>();
            }

            if (GetComponent<MovementComponent>()) {
                _movementComponent = GetComponent<MovementComponent>();
            }

            GameEventSystem.OnPickupEvent += OnPickup;
        }


        // Update is called once per frame
        void Update()
        {
            //Have a way to get gamemode IsGameFinish() method
            if (!_canMove)
            {
                return;
            }

            SetCharacterMovementState();

            if(_jumpComponent != null) {
                _jumpComponent.Jump();
            }

            if (_movementComponent != null)
            {
                _movementComponent.Move();
            }
        }

        private void OnDestroy()
        {
            GameEventSystem.OnPickupEvent -= OnPickup;
        }

        private void SetCharacterMovementState() {

            RaycastHit2D raycast = Physics2D.Raycast(_col.bounds.center, Vector2.down * _col.bounds.extents.y, 1.1f, LayerMask.GetMask("Floor"));
            if (raycast.collider != null) {
                _characterStateComponent.CurrentPlayerMovementState = CharacterStateComponent.PlayerMovementState.WALKING;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "JumpingWall"){
                _gripComponent.Grip(collision.contacts[0].point);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "JumpingWall")
            {
                _gripComponent.UnGrip();
            }
        }

        void OnPickup(Pickup pickup)
        {
            if (pickup as ColorPickup)
            {
                var pickupColor = pickup as ColorPickup;
                ChangeColor(pickupColor.color);
                GameEventSystem.OnPlayerChangeColorEvent(this, pickupColor.color);
            }
        }

        public void ChangeColor(ObjectColor.color color) {
            topHair.color = ObjectColor.getColor(color);
            ponyTail.color = ObjectColor.getColor(color);
        }

        public bool CanMove
        {
            set
            {
                _canMove = value;
            }
        }

        public void FootstepSound() {
            if (_characterStateComponent.CurrentPlayerMovementState == CharacterStateComponent.PlayerMovementState.WALKING)
            {
                int index = Random.Range(0, footstepAudioClips.Count);
                SoundManager.Instance.PlaySFX(footstepAudioClips[index], 0.1f);
            }
        }
    }
}