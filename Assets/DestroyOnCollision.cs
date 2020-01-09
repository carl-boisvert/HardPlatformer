using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;

[RequireComponent(typeof(Collider2D))]
public class DestroyOnCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            GameEventSystem.OnPlayerDied();
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
