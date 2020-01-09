using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFramework.SceneManager
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager _instance = null;

        SwitchSceneTrigger lastTrigger = null;
        // Start is called before the first frame update
        void Awake()
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

        public static SceneManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public void LoadNextScene(SwitchSceneTrigger scene) {
            lastTrigger = scene;
            StartCoroutine(LoadScene(scene.sceneNameTo));
        }

        public PlayerSpawnpoint GetSpawnPoint() {
            GameObject checkpoint = GameObject.Find(lastTrigger.checkpointFrom);
            return checkpoint.GetComponent<Checkpoint>();
        }

        private IEnumerator LoadScene(string name)
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            GameEventSystem.OnNewSceneLoaded();
        }
    }
}