using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;

namespace Gameplay
{

    [RequireComponent(typeof(Collider2D))]
    public class Trap : MonoBehaviour
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
                GameEventSystem.OnPlayerDied();
            }

        }
    }
}