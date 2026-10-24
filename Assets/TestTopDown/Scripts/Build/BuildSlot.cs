using Nitzz.Game.Structures;
using UnityEngine;

namespace Nitzz.Game.Build
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BuildSlot : MonoBehaviour
    {
        public Structure Occupant { get; private set; }
        public bool IsFree => Occupant == null;

        public void Occupy(Structure s)
        {
            Occupant = s;
            s.Destroyed += OnOccupantDestroyed;
        }

        void OnOccupantDestroyed(Structure s)
        {
            s.Destroyed -= OnOccupantDestroyed;
            Occupant = null;
        }
    }
}