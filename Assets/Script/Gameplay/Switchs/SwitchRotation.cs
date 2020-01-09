using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Switch
{
    [CreateAssetMenu(menuName = "Data/Switch/SwitchRotator")]
    public class SwitchRotation : ScriptableObject
    {
        public float rotationAngle = 90.0f;
        public string switchId;
    }
}