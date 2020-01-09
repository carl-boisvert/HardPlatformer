using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework.GameMode.Objectives;
using GameFramework;

namespace GameFramework.GameMode
{

    public class FindObjectiveMode : MonoBehaviour
    {

        public List<Objective> objectives;

        // Use this for initialization
        void Start()
        {
            GameEventSystem.OnPlayerGotObjectiveEvent += OnPlayerGotObjective;
        }

        private void OnDestroy()
        {
            GameEventSystem.OnPlayerGotObjectiveEvent -= OnPlayerGotObjective;
        }

        void OnPlayerGotObjective(Objective objective)
        {
            objectives.Remove(objective);
            if(objectives.Count == 0){
                GameEventSystem.OnPlayerWon();
            }
        }
    }
}