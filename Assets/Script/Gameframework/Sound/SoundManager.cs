using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GameFramework.Sound
{
    public class SoundManager : MonoBehaviour
    {

        public static SoundManager Instance = null;

        [SerializeField]
        private List<LevelMusic> levelMusics;
        [SerializeField]
        private GameObject _audioSourcePrefab;

        private LevelMusic levelMusic;
        private AudioSource _levelMusic;
        private AudioSource _levelMusicNext;
        private PoolObject _poolObject;
        private AudioClip _currentMusic;
        private AudioClip _currentSfx;

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

            Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            levelMusic = levelMusics.Find(levelMusicModel => levelMusicModel.SceneName == currentScene.name);
            _currentMusic = levelMusic.LevelMusics[0];

            _poolObject = new PoolObject(_audioSourcePrefab);

            GameObject levelMusicGameObject = _poolObject.GetNewObject();
            _levelMusic = levelMusicGameObject.GetComponent<AudioSource>();
            GameObject _levelMusicNextGameObject = _poolObject.GetNewObject();
            _levelMusicNext = _levelMusicNextGameObject.GetComponent<AudioSource>();

            Init();
        }

        private void Init()
        {
            _levelMusic.clip = _currentMusic;
            _levelMusic.volume = levelMusic.Volume;
            _levelMusic.Play();
        }

        void PlayMusic(AudioClip clip, bool fade = false, float fadeOutTime = 0.0f) {
            if (fade) {
                StartCoroutine(FadeOut(_levelMusic, fadeOutTime));
                _levelMusicNext.clip = clip;
                _levelMusicNext.volume = 0;
                StartCoroutine(FadeIn(_levelMusicNext, _levelMusic.volume, fadeOutTime));
            }
        }

        public void PlaySFX(AudioClip clip, float volume) {
            GameObject audioSource = _poolObject.GetNewObject();
            AudioSource sfx = audioSource.GetComponent<AudioSource>();
            sfx.volume = volume;
            sfx.PlayOneShot(clip);
            _poolObject.ReleaseObject(audioSource);
        }


        private AudioSource CreateAudioSource() {
            return gameObject.AddComponent<AudioSource>();
        }

        private IEnumerator FadeOut(AudioSource audioSource, float fadeOutTime) {

            float currentTime = 0.0f;

            // Wait until the asynchronous scene fully loads
            while (currentTime < fadeOutTime)
            {
                audioSource.volume -= audioSource.volume / fadeOutTime;
                yield return new WaitForSeconds(1.0f);
            }
            audioSource.volume = 0;
        }

        private IEnumerator FadeIn(AudioSource audioSource, float volume, float fadeOutTime)
        {

            float currentTime = 0.0f;
            audioSource.Play();
            // Wait until the asynchronous scene fully loads
            while (currentTime < fadeOutTime)
            {
                audioSource.volume += volume / fadeOutTime;
                yield return new WaitForSeconds(1.0f);
            }
            _levelMusic = audioSource;
            _levelMusicNext = null;
        }

    }
}