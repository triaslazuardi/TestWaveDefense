using Nitzz.Game.State;
using Nitzz.Game.Build;
using UnityEngine;

namespace Nitzz.Game.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] int totalWaves = 3;
        [SerializeField] int waveReward = 40;

        public int TotalWaves => totalWaves;
        public int CurrentWave { get; private set; }
        public bool CanBuild => Fsm.Current == build;

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

        public void CompleteWave()
        {
            if (CurrentWave >= totalWaves) { Fsm.ChangeState(win); return; }

            CurrentWave++;
            ResourceManager.Instance.Add(waveReward);
            GameEvents.RaiseWaveChanged(CurrentWave);
            Fsm.ChangeState(build);
        }

        void HandleCoreDestroyed() => Fsm.ChangeState(lose);
    }
}