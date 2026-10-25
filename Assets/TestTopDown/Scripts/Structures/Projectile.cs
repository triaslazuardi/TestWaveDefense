using Nitzz.Game.Enemies;
using UnityEngine;

namespace Nitzz.Game.Structures
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 12f;

        Enemy target;
        int damage;

        public void Launch(Enemy t, int dmg)
        {
            target = t;
            damage = dmg;
        }

        void Update()
        {
            if (target == null) { Destroy(gameObject); return; }

            Vector3 to = target.transform.position - transform.position;
            float step = speed * Time.deltaTime;

            if (to.magnitude <= step)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }

            transform.position += to.normalized * step;
        }
    }
}