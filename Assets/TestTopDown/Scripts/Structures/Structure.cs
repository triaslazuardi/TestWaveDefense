using Nitzz.Game.Data;
using System;
using UnityEngine;

namespace Nitzz.Game.Structures
{
    public class Structure : MonoBehaviour
    {
        public StructureData Data { get; private set; }
        public int CurrentHealth { get; private set; }

        public event Action<Structure> Destroyed;

        public virtual void Initialize(StructureData data)
        {
            Data = data;
            CurrentHealth = data.maxHealth;
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            if (CurrentHealth <= 0) Die();
        }

        protected virtual void Die()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}