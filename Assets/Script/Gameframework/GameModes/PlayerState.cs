using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameFramework.GameMode {

    public class PlayerState : MonoBehaviour
    {
        public static PlayerState Instance = null;

        public PlayerData _playerData;

        void Awake()
        {
            //Check if instance already exists
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            //Read Player Data
            //_playerData = GlobalControl.Instance.savedPlayerData;
        }
    }
}