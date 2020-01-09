using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;

namespace Gameplay.Switch
{
    public class SwitchListener : MonoBehaviour
    {

        public List<SwitchRotation> switchsRotation;
        // Use this for initialization
        void Start()
        {
            GameEventSystem.OnPlayerActivateSwitch += RotateWalls;
        }

        private void OnDestroy()
        {
            GameEventSystem.OnPlayerActivateSwitch -= RotateWalls;
        }

        private void RotateWalls(string switchIdTriggered)
        {
            SwitchRotation switchObject = switchsRotation.Find(switchRotation => switchRotation.switchId == switchIdTriggered);
            if (switchObject)
            {
                gameObject.transform.Rotate(0f, 0f, switchObject.rotationAngle);
            }
        }
    }
}