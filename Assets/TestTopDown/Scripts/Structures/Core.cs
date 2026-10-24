using Nitzz.Game.Core;
using Nitzz.Game.Data;
using UnityEngine;

namespace Nitzz.Game.Structures
{
    public class Core : Structure
    {
        [SerializeField] StructureData coreData;

        void Start() => Initialize(coreData);

        protected override void Die() => GameEvents.RaiseCoreDestroyed();
    }
}