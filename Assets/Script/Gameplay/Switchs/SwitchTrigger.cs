using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;

namespace Gameplay.Switch
{
    [RequireComponent(typeof(Collider2D))]
    public class SwitchTrigger : MonoBehaviour
    {

        public string switchId;
        public Animator animator;

        private bool isActive = true;

        private Collider2D _collider;
        // Use this for initialization
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && isActive)
            {
                if (animator != null)
                {
                    animator.SetTrigger("ActivateSwitch");
                }
                isActive = false;
                GameEventSystem.OnPlayerActivateSwitch(switchId);
            }
        }
    }
}