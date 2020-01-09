using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Debug
{
    public class DebugManager : MonoBehaviour
    {
        public static DebugManager _instance = null;
        public bool characterState = false;
        public bool wallJump = false;
        public bool isPlayerCanJump = false;
        public bool debugCharacterState = false;

        void Awake()
        {
            //Check if instance already exists
            if (_instance == null)
            {
                _instance = this;
            } else if (_instance != this) {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public static DebugManager Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}