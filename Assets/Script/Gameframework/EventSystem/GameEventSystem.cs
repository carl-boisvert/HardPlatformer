using GameFramework.SceneManager;
using Gameplay;
using GameFramework.Utils;
using GameFramework.GameMode.Objectives;
using GameFramework.Entities.Character.Player.Controller;

namespace GameFramework
{
    public static class GameEventSystem
    {

        public delegate void PlayerPassCheckpointEvent(Checkpoint checkpoint);
        public static PlayerPassCheckpointEvent OnPlayerPassCheckpoint;

        public delegate void PlayerGotObjectiveEvent(Objective objective);
        public static PlayerGotObjectiveEvent OnPlayerGotObjectiveEvent;

        public delegate void PickupEvent(Pickup pickup);
        public static PickupEvent OnPickupEvent;
        public delegate void HitWallEvent(Wall pickup);
        public static HitWallEvent OnHitWallEvent;

        public delegate void PlayerChangeColorEvent(PlayerController playerController, ObjectColor.color color);
        public static PlayerChangeColorEvent OnPlayerChangeColorEvent;

        public delegate void PlayerDiedEvent();
        public static PlayerDiedEvent OnPlayerDied;
        public delegate void PlayerWonEvent();
        public static PlayerWonEvent OnPlayerWon;

        public delegate void PlayerJumpOnWall();
        public static PlayerJumpOnWall OnPlayerJumpOnWall;
        public delegate void PlayerActivateSwitch(string switchId);
        public static PlayerActivateSwitch OnPlayerActivateSwitch;

        public delegate void NewSceneLoaded();
        public static NewSceneLoaded OnNewSceneLoaded;

        public delegate void WheelSpawned();
        public static WheelSpawned OnWheelSpawned;

        //DIALOGUE
        public delegate void StartDialogue();
        public static StartDialogue OnStartDialogue;
        public delegate void EndDialogue();
        public static EndDialogue OnEndDialogue;
        public delegate void NextDialogueLine();
        public static NextDialogueLine OnNextDialogueLine;
    }
}