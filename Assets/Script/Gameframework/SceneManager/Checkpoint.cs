using UnityEngine;

using GameFramework;

namespace GameFramework.SceneManager
{

    [RequireComponent(typeof(Collider2D))]
    public class Checkpoint : PlayerSpawnpoint
    {
        private Collider2D _collider;

        // Use this for initialization
        void Start()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                GameEventSystem.OnPlayerPassCheckpoint(this);
            }

        }
    }
}