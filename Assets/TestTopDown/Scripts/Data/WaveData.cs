using Nitzz.Game.Data;
using System;
using UnityEngine;

namespace Nitzz.Game.Data
{
    [Serializable]
    public class SpawnEntry
    {
        public EnemyData enemy;
        public int count;
    }

    [CreateAssetMenu(menuName = "LastBastion/Wave Data", fileName = "NewWave")]
    public class WaveData : ScriptableObject
    {
        public SpawnEntry[] spawns;
        public float spawnInterval = 0.8f;
        public int clearReward = 40;
    }
}
