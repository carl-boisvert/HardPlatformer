using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using GameFramework.Utils;
using GameFramework.SceneManager;

namespace GameFramework.GameMode {

    //TUTORIAL
    [Serializable]
    public class PlayerData
    {
        public ObjectColor.color _currentColor;
        public PlayerSpawnpoint lastCheckpoint;
    }
}