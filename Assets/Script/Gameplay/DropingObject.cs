using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DropingObject : MonoBehaviour
    {

        public GameObject objectToDrop;
        public float droppingSpeed;
        public List<Spawnpoint> spawnPoints;

        private int _coroutineId;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        private void OnDestroy()
        {
            StopCoroutine(SpawnLoop());
        }

        void SpawnObject(Spawnpoint point) {
            Instantiate(objectToDrop, point.transform);
        }

        IEnumerator SpawnLoop()
        {
            while (true) {
                foreach (Spawnpoint point in spawnPoints)
                {
                    SpawnObject(point);
                    yield return new WaitForSeconds(droppingSpeed);
                }
            }
        }
    }
}