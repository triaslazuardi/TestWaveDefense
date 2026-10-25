using Nitzz.Game.Cores;
using Nitzz.Game.Data;
using UnityEngine;

namespace Nitzz.Game.Structures
{
    public class Core : Structure
    {
        public static Core Instance { get; private set; }

        [SerializeField] StructureData coreData;
        bool dead;

        void Awake() => Instance = this;

        void Start()
        {
            Initialize(coreData);
            GameEvents.RaiseCoreHealthChanged(CurrentHealth, Data.maxHealth);
        }

        protected override void OnDamaged() =>
            GameEvents.RaiseCoreHealthChanged(Mathf.Max(CurrentHealth, 0), Data.maxHealth);

        protected override void Die()
        {
            if (dead) return;
            dead = true;
            GameEvents.RaiseCoreDestroyed();
        }
    }
}