using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework.Debug;

namespace GameFramework.Character.Component
{
    public class CharacterStateComponent : MonoBehaviour
    {

        private PlayerMovementState _currentPlayerMovementState;

        public enum PlayerMovementState
        {
            JUMPING,
            ONWALL,
            FALLING,
            WALKING,
            DEAD,
            WON
        }

        private void Start()
        {
            _currentPlayerMovementState = PlayerMovementState.WALKING;
        }

        public PlayerMovementState CurrentPlayerMovementState
        {
            get { return _currentPlayerMovementState; }
            set
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                if (DebugManager.Instance && DebugManager.Instance.debugCharacterState) {
                    UnityEngine.Debug.Log("Player Movement State Changed from "+ _currentPlayerMovementState + " to " + value);
                }
#endif
                _currentPlayerMovementState = value;
            }
        }

    }
}