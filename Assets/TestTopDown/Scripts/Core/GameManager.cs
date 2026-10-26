using Nitzz.Game.State;
using Nitzz.Game.Build;
using Nitzz.Game.Enemies;
using Nitzz.utility.Sounds;
using UnityEngine;
using Nitzz.Game.Data;
using Nitzz.Game.Structures;

namespace Nitzz.Game.Cores
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] WaveData[] waves;
        [SerializeField] EnemySpawner spawner;

        public int TotalWaves => waves.Length;
        public int CurrentWave { get; private set; }
        public bool CanBuild => Fsm.Current == build;
        public bool IsDefending => Fsm.Current == defend;

        public StateMachine Fsm { get; } = new StateMachine();

        BuildState build;
        DefendState defend;
        ResultState win, lose;

        void Awake()
        {
            Instance = this;
            build = new BuildState();
            defend = new DefendState(this);
            win = new ResultState(true);
            lose = new ResultState(false);
            SoundManager.PlayBgm(1);
        }

        void OnEnable() => GameEvents.OnCoreDestroyed += HandleCoreDestroyed;
        void OnDisable() => GameEvents.OnCoreDestroyed -= HandleCoreDestroyed;

        void Start()
        {
            CurrentWave = 1;
            GameEvents.RaiseWaveChanged(CurrentWave);
            Fsm.ChangeState(build);
        }

        void Update() => Fsm.Update();

        public void StartWave()
        {
            if (Fsm.Current == build) Fsm.ChangeState(defend);
        }

        public void BeginWave() => spawner.StartWave(waves[CurrentWave - 1]);

        public void TickDefend()
        {
            if (spawner.IsFinished && Enemy.All.Count == 0) CompleteWave();
        }

        void CompleteWave()
        {
            if (CurrentWave >= TotalWaves)
            {
                Fsm.ChangeState(win);
                return;
            }

            int reward = waves[CurrentWave - 1].clearReward + GeneratorBonus();
            ResourceManager.Instance.Add(reward);

            CurrentWave++;
            GameEvents.RaiseWaveChanged(CurrentWave);
            Fsm.ChangeState(build);
        }

        int GeneratorBonus()
        {
            int bonus = 0;
            foreach (var s in Structure.All)
                if (s.Data != null && s.Data.type == StructureType.Generator)
                    bonus += s.Data.goldPerWave;
            return bonus;
        }

        void HandleCoreDestroyed()
        {
            spawner.Stop();
            Fsm.ChangeState(lose);
        }
    }
}