using Nitzz.Game.Cores;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nitzz.Game.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] TMP_Text goldText;
        [SerializeField] TMP_Text waveText;
        [SerializeField] TMP_Text phaseText;
        [SerializeField] TMP_Text coreText;
        [SerializeField] Button startWaveButton;

        [Header("Result")]
        [SerializeField] GameObject resultPanel;
        [SerializeField] TMP_Text resultText;
        [SerializeField] Button restartButton;

        void OnEnable()
        {
            GameEvents.OnGoldChanged += SetGold;
            GameEvents.OnWaveChanged += SetWave;
            GameEvents.OnPhaseChanged += SetPhase;
            GameEvents.OnCoreHealthChanged += SetCore;
        }

        void OnDisable()
        {
            GameEvents.OnGoldChanged -= SetGold;
            GameEvents.OnWaveChanged -= SetWave;
            GameEvents.OnPhaseChanged -= SetPhase;
            GameEvents.OnCoreHealthChanged -= SetCore;
        }

        void Start()
        {
            startWaveButton.onClick.AddListener(() => GameManager.Instance.StartWave());
            restartButton.onClick.AddListener(() =>
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
            resultPanel.SetActive(false);
        }

        void SetGold(int g) => goldText.text = $"Gold: {g}";
        void SetWave(int w) => waveText.text = $"Wave {w}/{GameManager.Instance.TotalWaves}";
        void SetCore(int cur, int max) => coreText.text = $"Core: {cur}/{max}";

        void SetPhase(string p)
        {
            phaseText.text = p;
            startWaveButton.interactable = p == "BUILD";

            bool finished = p == "VICTORY" || p == "DEFEAT";
            resultPanel.SetActive(finished);
            if (finished)
                resultText.text = p == "VICTORY"
                    ? "VICTORY!\nBasis bertahan dari semua wave."
                    : "DEFEAT\nCore hancur.";
        }
    }
}