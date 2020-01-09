using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using Gameplay;
using GameFramework.GameMode.Objectives;

namespace GameFramework.Character.Component
{

    [RequireComponent(typeof(Collider2D))]
    public class InteractionManager : MonoBehaviour {

        private Collider2D _collider;

    	// Use this for initialization
    	void Start () {
            _collider = GetComponent<Collider2D>();
            //_collider.isTrigger = true;
    	}

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Pickup")
            {
                GameEventSystem.OnPickupEvent(col.gameObject.GetComponent<Pickup>());
            }
            if (col.gameObject.tag == "Wall" || col.gameObject.tag == "WallWalking")
            {
                Wall wall = col.gameObject.GetComponent<Wall>();
                if (wall.IsActive && GameEventSystem.OnHitWallEvent != null)
                {
                    GameEventSystem.OnHitWallEvent(wall);
                }
            }
            if (col.gameObject.tag == "Coin")
            {
                GameEventSystem.OnPlayerGotObjectiveEvent(col.gameObject.GetComponent<Objective>());
            }
            if (col.gameObject.tag == "Wheel")
            {
                Wheel wheel = col.gameObject.GetComponent<Wheel>();
                if (wheel.IsActive)
                {
                    GameEventSystem.OnPlayerDied();
                }
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.tag == "Pickup"){
                GameEventSystem.OnPickupEvent(col.gameObject.GetComponent<Pickup>());
            }
            if(col.gameObject.tag == "Wall" || col.gameObject.tag == "WallWalking")
            {
                Wall wall = col.gameObject.GetComponent<Wall>();
                if(wall.IsActive && GameEventSystem.OnHitWallEvent != null){
                    GameEventSystem.OnHitWallEvent(wall);
                }
            }
            if (col.gameObject.tag == "Coin")
            {
                GameEventSystem.OnPlayerGotObjectiveEvent(col.gameObject.GetComponent<Objective>());
            }
            if (col.gameObject.tag == "Wheel")
            {
                Wheel wheel = col.gameObject.GetComponent<Wheel>();
                if (wheel.IsActive) {
                    GameEventSystem.OnPlayerDied();
                }
            }
        }
    }
}