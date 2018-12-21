using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Gameplay;
using GameFramework.GameMode.Objectives;

namespace GameFramework.Character.Player
{

    [RequireComponent(typeof(Collider2D))]
    public class InteractionManager : MonoBehaviour {

    private Collider2D _collider;

	// Use this for initialization
	void Start () {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Contains("Pickup")){
            EventSystem.OnPickupEvent(col.gameObject.GetComponent<Pickup>());
        }
        if(col.gameObject.tag.Contains("Wall") || col.gameObject.tag.Contains("WallWalking"))
        {
            EventSystem.OnHitWallEvent(col.gameObject.GetComponent<Wall>());
        }
        if (col.gameObject.tag.Contains("Coin"))
        {
                EventSystem.OnPlayerGotObjectiveEvent(col.gameObject.GetComponent<Objective>());
        }
    }
}
}