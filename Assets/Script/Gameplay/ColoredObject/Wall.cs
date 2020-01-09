using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.Utils;
using GameFramework.Entities.Character.Player.Controller;

using GameFramework.GameMode;

namespace Gameplay
{
    public class Wall : ColoredObstacle
    {

        // Use this for initialization
        void Start()
        {
            _spriteRenderer.color = ObjectColor.getColor(objectColor);
        }
    }
}