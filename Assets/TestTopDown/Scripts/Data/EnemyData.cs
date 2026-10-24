using UnityEngine;

namespace Nitzz.Game.Data
{
    [CreateAssetMenu(menuName = "LastBastion/Enemy Data", fileName = "NewEnemy")]
    public class EnemyData : ScriptableObject
    {
        public string displayName;
        public int maxHealth = 20;
        public float moveSpeed = 2f;
        public int damage = 5;
        public float attackInterval = 1f;
        public GameObject prefab;
    }
}