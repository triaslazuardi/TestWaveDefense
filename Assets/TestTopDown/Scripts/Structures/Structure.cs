using Nitzz.Game.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Game.Structures
{
    public class Structure : MonoBehaviour
    {
        public static readonly List<Structure> All = new List<Structure>();

        public StructureData Data { get; private set; }
        public int CurrentHealth { get; private set; }

        public event Action<Structure> Destroyed;

        SpriteRenderer sr;
        Color baseColor;

        void OnEnable() => All.Add(this);
        void OnDisable() => All.Remove(this);

        public virtual void Initialize(StructureData data)
        {
            Data = data;
            CurrentHealth = data.maxHealth;
            sr = GetComponent<SpriteRenderer>();
            if (sr != null) baseColor = sr.color;
        }

        public void TakeDamage(int amount)
        {
            if (CurrentHealth <= 0) return;

            CurrentHealth -= amount;

            if (sr != null)
            {
                float t = Mathf.Clamp01((float)CurrentHealth / Data.maxHealth);
                sr.color = Color.Lerp(Color.black, baseColor, 0.4f + 0.6f * t);
            }

            OnDamaged();
            if (CurrentHealth <= 0) Die();
        }

        protected virtual void OnDamaged() { }

        protected virtual void Die()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }

        public static Structure FindNearest(Vector3 pos, float maxDist)
        {
            Structure best = null;
            float bestDist = maxDist;
            foreach (var s in All)
            {
                float d = Vector3.Distance(pos, s.transform.position);
                if (d <= bestDist) { bestDist = d; best = s; }
            }
            return best;
        }

    }
}