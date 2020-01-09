using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Dialogue
{
    [CreateAssetMenu(fileName = "Speaker", menuName = "Data/Speaker", order = 1)]
    public class Speaker : ScriptableObject
    {
        public string characterName;
        public Sprite avatar;
    }
}