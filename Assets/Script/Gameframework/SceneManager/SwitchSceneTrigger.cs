using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.SceneManager
{
    public class SwitchSceneTrigger : PlayerSpawnpoint
    {
        public string sceneNameFrom;
        public string sceneNameTo;
        public string checkpointFrom;
        public string checkpointTo;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneManager.Instance.LoadNextScene(this);
            }
        }
    }
}