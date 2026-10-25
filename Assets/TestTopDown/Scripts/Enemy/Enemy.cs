using Nitzz.Game.Cores;
using Nitzz.Game.Data;
using Nitzz.Game.Structures;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public static readonly List<Enemy> All = new List<Enemy>();

        [SerializeField] float attackRange = 0.8f;

        EnemyData data;
        int health;
        float attackTimer;

        void OnEnable() => All.Add(this);
        void OnDisable() => All.Remove(this);

        public void Initialize(EnemyData d)
        {
            data = d;
            health = d.maxHealth;
        }

        void Update()
        {
            if (data == null || !GameManager.Instance.IsDefending) return;

            // Ada struktur dalam jangkauan? Berhenti dan serang.
            Structure target = Structure.FindNearest(transform.position, attackRange);
            if (target != null)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= data.attackInterval)
                {
                    attackTimer = 0f;
                    target.TakeDamage(data.damage);
                }
                return;
            }

            // Kalau tidak, jalan lurus ke Core.
            attackTimer = 0f;
            Vector3 dest = Core.Instance.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, dest, data.moveSpeed * Time.deltaTime);
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0) Destroy(gameObject);
        }

        public static Enemy FindNearest(Vector3 pos, float maxDist)
        {
            Enemy best = null;
            float bestDist = maxDist;
            foreach (var e in All)
            {
                float d = Vector3.Distance(pos, e.transform.position);
                if (d <= bestDist) { bestDist = d; best = e; }
            }
            return best;
        }
    }
}