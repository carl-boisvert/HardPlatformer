using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Gameplay;
using GameFramework.GameMode.Objectives;
using GameFramework.Character.Player;

namespace GameFramework
{
    public static class EventSystem
    {

        public delegate void PlayerPassCheckpointEvent(Checkpoint checkpoint);
        public static PlayerPassCheckpointEvent OnPlayerPassCheckpoint;

        public delegate void PlayerGotObjectiveEvent(Objective objective);
        public static PlayerGotObjectiveEvent OnPlayerGotObjectiveEvent;

        public delegate void PickupEvent(Pickup pickup);
        public static PickupEvent OnPickupEvent;
        public delegate void HitWallEvent(Wall pickup);
        public static HitWallEvent OnHitWallEvent;

        public delegate void PlayerChangeColorEvent(PlayerController playerController);
        public static PlayerChangeColorEvent OnPlayerChangeColorEvent;

        public delegate void PlayerDiedEvent();
        public static PlayerDiedEvent OnPlayerDied;
        public delegate void PlayerWonEvent();
        public static PlayerWonEvent OnPlayerWon;

    }
}