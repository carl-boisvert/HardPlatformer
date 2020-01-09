using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Utils;

namespace Gameplay {
    public class WheelsSpawnpoint : MonoBehaviour
    {
        public Wheel wheelsPrefab;
        public List<ObjectColor.color> wheelsColors;
        public float _spawnDelay = 3f;

        private int _currentIndex = 0;
        private int _numberOfWheels;
        private float _lastSpawn = 0f;

        private void Start()
        {
            _numberOfWheels = wheelsColors.Count-1;
            SpawnWheel();
            GameEventSystem.OnPlayerDied += OnPlayerDied;
        }

        private void Update()
        {
            if (_lastSpawn + Time.deltaTime >= _spawnDelay)
            {
                SpawnWheel();
                _lastSpawn = 0f;
            }
            else {
                _lastSpawn += Time.deltaTime;
            }

        }

        private void SpawnWheel() {
            if (_currentIndex > _numberOfWheels)
            {
                _currentIndex = 0;
            }

            Wheel wheel = Instantiate(wheelsPrefab, gameObject.transform);
            wheel.ChangeColor(wheelsColors[_currentIndex]);
            _currentIndex++;
            if(GameEventSystem.OnWheelSpawned != null)
            {
                GameEventSystem.OnWheelSpawned();
            }
        }

        private void OnPlayerDied() {
            _currentIndex = 0;
        }
    }
}