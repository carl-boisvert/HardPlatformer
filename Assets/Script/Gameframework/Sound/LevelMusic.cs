using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Sound
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Data/Sound/LevelsMusic", order = 1)]
    public class LevelMusic : ScriptableObject
    {
        [SerializeField]
        private string _sceneName;
        [SerializeField]
        private List<AudioClip> levelMusics;
        [SerializeField]
        [Range(0, 100)]
        private int volume;

        public string SceneName
        {
            get
            {
                return _sceneName;
            }

            set
            {
                _sceneName = value;
            }
        }

        public List<AudioClip> LevelMusics
        {
            get
            {
                return levelMusics;
            }

            set
            {
                levelMusics = value;
            }
        }

        public float Volume
        {
            get
            {
                return ((float)volume) /100;
            }
        }
    }
}