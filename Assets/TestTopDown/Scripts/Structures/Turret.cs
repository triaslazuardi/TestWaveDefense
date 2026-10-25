using Nitzz.Game.Cores;
using Nitzz.Game.Enemies;
using Nitzz.Game.Structures;
using UnityEngine;

namespace Nitzz.Game.Structures
{
    public class Turret : Structure
    {
        [SerializeField] Projectile projectilePrefab;

        float cooldown;

        void Update()
        {
            if (Data == null || !GameManager.Instance.IsDefending) return;

            cooldown -= Time.deltaTime;
            if (cooldown > 0f) return;

            Enemy target = Enemy.FindNearest(transform.position, Data.range);
            if (target == null) return;

            var p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            p.Launch(target, Data.damage);
            cooldown = 1f / Data.fireRate;
        }
    }
}