using UnityEngine;

namespace Nitzz.Game.Data
{
    public enum StructureType { Core, Wall, Turret, Generator }

    [CreateAssetMenu(menuName = "LastBastion/Structure Data", fileName = "NewStructure")]
    public class StructureData : ScriptableObject
    {
        public string displayName;
        public StructureType type;
        public int cost;
        public int maxHealth = 50;
        public GameObject prefab;

        [Header("Turret (dipakai hari 2)")]
        public int damage = 8;
        public float range = 2.5f;
        public float fireRate = 1f;

        [Header("Generator (dipakai hari 2)")]
        public int goldPerWave = 15;
    }
}