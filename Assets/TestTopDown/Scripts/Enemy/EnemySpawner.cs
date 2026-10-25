using Nitzz.Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Game.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] Transform[] spawnPoints;

        public bool IsFinished { get; private set; } = true;
        Coroutine routine;

        public void StartWave(WaveData wave)
        {
            Stop();
            routine = StartCoroutine(SpawnRoutine(wave));
        }

        public void Stop()
        {
            if (routine != null) StopCoroutine(routine);
            routine = null;
            IsFinished = true;
        }

        IEnumerator SpawnRoutine(WaveData wave)
        {
            IsFinished = false;

            var queue = new List<EnemyData>();
            foreach (var entry in wave.spawns)
                for (int i = 0; i < entry.count; i++) queue.Add(entry.enemy);

            // Acak urutan supaya Runner dan Brute bercampur.
            for (int i = queue.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (queue[i], queue[j]) = (queue[j], queue[i]);
            }

            foreach (var data in queue)
            {
                var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
                var go = Instantiate(data.prefab, point.position, Quaternion.identity);
                go.GetComponent<Enemy>().Initialize(data);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            IsFinished = true;
        }
    }
}