using Nitzz.Game.Cores;
using UnityEngine;

namespace Nitzz.Game.Build
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        [SerializeField] int startingGold = 100;
        public int Gold { get; private set; }

        void Awake()
        {
            Instance = this;
            Gold = startingGold;
        }

        void Start() => GameEvents.RaiseGoldChanged(Gold);

        public bool CanAfford(int cost) => Gold >= cost;

        public bool TrySpend(int cost)
        {
            if (!CanAfford(cost)) return false;
            Gold -= cost;
            GameEvents.RaiseGoldChanged(Gold);
            return true;
        }

        public void Add(int amount)
        {
            Gold += amount;
            GameEvents.RaiseGoldChanged(Gold);
        }
    }
}