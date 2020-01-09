using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;

namespace Gameplay
{
    public class DeathFloor : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Player") {
                GameEventSystem.OnPlayerDied();
            }
        }
    }
}