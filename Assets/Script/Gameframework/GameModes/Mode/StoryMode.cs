using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Utils;
using GameFramework.SceneManager;
using GameFramework.Entities.Character.Player.Controller;
using GameFramework.Character.Component;
using GameFramework.SceneManager;
using GameFramework.Debug;
using GameFramework.Sound;

using Cinemachine;

namespace GameFramework.GameMode
{
    public class StoryMode : MonoBehaviour
    {
        public static StoryMode _instance = null;

        public GameObject playerPrefabs;
        public GameObject cameraPrefabs;
        public GameObject sceneManagerPrefabs;
        public GameObject soundManagerPrefabs;
        public ObjectColor.color _currentColor;

        private PlayerState playerState;
        private PlayerController _playerController;
        private SoundManager _soundManager;
        private CharacterStateComponent _characterStateComponent;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private void Awake()
        {
            //Check if instance already exists
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        // Start is called before the first frame update
        void Start()
        {
            GameEventSystem.OnPlayerChangeColorEvent += OnPlayerChangeColorEvent;
            GameEventSystem.OnPlayerPassCheckpoint += OnPlayerPassCheckpoint;
            GameEventSystem.OnPlayerDied += OnPlayerDied;
            GameEventSystem.OnPlayerWon += OnPlayerWon;
            GameEventSystem.OnNewSceneLoaded += InitScene;
            GameEventSystem.OnWheelSpawned += OnWheelSpawned;
            GameEventSystem.OnStartDialogue += OnStartDialogue;
            GameEventSystem.OnEndDialogue += OnEndDialogue;

            InitScene();
        }

        private void InitScene() {
            PlayerState = FindObjectOfType<PlayerState>();

            GameObject player = Instantiate(playerPrefabs);
            GameObject camera = Instantiate(cameraPrefabs);
            GameObject sceneManager = Instantiate(sceneManagerPrefabs);
            GameObject soundManager = Instantiate(soundManagerPrefabs);
            //TODO JAMAIS ÇA!!!!
            GameObject levelEdge = GameObject.Find("LevelEdge");

            //Setup Camera
            _cinemachineVirtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
            _cinemachineVirtualCamera.Follow = player.transform;
            CinemachineConfiner _confiner = camera.GetComponent<CinemachineConfiner>();
            _confiner.m_ConfineMode = CinemachineConfiner.Mode.Confine2D;
            _confiner.m_BoundingShape2D = levelEdge.GetComponent<PolygonCollider2D>();

            _playerController = player.GetComponent<PlayerController>();
            _characterStateComponent = player.GetComponent<CharacterStateComponent>();
            _soundManager = soundManager.GetComponent<SoundManager>();

            SpawnPlayer();

            GameEventSystem.OnPlayerChangeColorEvent(_playerController, PlayerState._playerData._currentColor);
        }

        private void OnDestroy()
        {
            GameEventSystem.OnPlayerChangeColorEvent -= OnPlayerChangeColorEvent;
            GameEventSystem.OnPlayerPassCheckpoint -= OnPlayerPassCheckpoint;
            GameEventSystem.OnPlayerDied -= OnPlayerDied;
            GameEventSystem.OnPlayerWon -= OnPlayerWon;
            GameEventSystem.OnNewSceneLoaded -= InitScene;
            GameEventSystem.OnWheelSpawned -= OnWheelSpawned;
        }

        void SpawnPlayer()
        {
            if (PlayerState._playerData.lastCheckpoint == null) {
                PlayerState._playerData.lastCheckpoint = SceneManager.SceneManager.Instance.GetSpawnPoint();
            }
            _playerController.gameObject.transform.position = PlayerState._playerData.lastCheckpoint.gameObject.transform.position;
            _playerController.ChangeColor(PlayerState._playerData._currentColor);
        }


        void OnPlayerChangeColorEvent(PlayerController playerController, ObjectColor.color color) {
            PlayerState._playerData._currentColor = color;
        }

        void OnPlayerPassCheckpoint(Checkpoint checkpointPassed)
        {
            PlayerState._playerData.lastCheckpoint = checkpointPassed;
        }

        void OnPlayerDied()
        {
            _characterStateComponent.CurrentPlayerMovementState = CharacterStateComponent.PlayerMovementState.DEAD;
            _playerController.gameObject.transform.position = PlayerState._playerData.lastCheckpoint.gameObject.transform.position;
        }

        void OnPlayerWon()
        {
            _characterStateComponent.CurrentPlayerMovementState = CharacterStateComponent.PlayerMovementState.WON;
        }

        bool IsGameFinished()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (DebugManager.Instance && DebugManager.Instance.characterState)
            {
                UnityEngine.Debug.Log(_characterStateComponent.CurrentPlayerMovementState);
            }
#endif
            return _characterStateComponent.CurrentPlayerMovementState == CharacterStateComponent.PlayerMovementState.DEAD || _characterStateComponent.CurrentPlayerMovementState == CharacterStateComponent.PlayerMovementState.WON;
        }

        private void OnWheelSpawned()
        {
            GameEventSystem.OnPlayerChangeColorEvent(_playerController, PlayerState._playerData._currentColor);
        }

        private void OnStartDialogue()
        {
            _playerController.CanMove = false;
        }

        private void OnEndDialogue()
        {
            _playerController.CanMove = true;
        }

        public ObjectColor.color CurrentColor
        {
            get
            {
                return _currentColor;
            }
        }

        public PlayerState PlayerState
        {
            get
            {
                return playerState;
            }

            set
            {
                playerState = value;
            }
        }
    }
}