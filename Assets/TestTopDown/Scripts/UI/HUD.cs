using Nitzz.Game.Build;
using Nitzz.Game.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nitzz.Game.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] TMP_Text goldText;
        [SerializeField] TMP_Text waveText;
        [SerializeField] TMP_Text phaseText;
        [SerializeField] Button startWaveButton;

        void OnEnable()
        {
            GameEvents.OnGoldChanged += SetGold;
            GameEvents.OnWaveChanged += SetWave;
            GameEvents.OnPhaseChanged += SetPhase;
        }

        void OnDisable()
        {
            GameEvents.OnGoldChanged -= SetGold;
            GameEvents.OnWaveChanged -= SetWave;
            GameEvents.OnPhaseChanged -= SetPhase;
        }

        void Start() => startWaveButton.onClick.AddListener(() => GameManager.Instance.StartWave());

        void SetGold(int g) => goldText.text = $"Gold: {g}";
        void SetWave(int w) => waveText.text = $"Wave {w}/{GameManager.Instance.TotalWaves}";

        void SetPhase(string p)
        {
            phaseText.text = p;
            startWaveButton.interactable = p == "BUILD";
        }
    }
}