using Nitzz.utility.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nitzz.Game.UI
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] Toggle bgmToggle;
        [SerializeField] Toggle sfxToggle;
        [SerializeField] TMP_Text bgmLabel;
        [SerializeField] TMP_Text sfxLabel;

        [SerializeField] GameObject settingsPanel;

        void Awake()
        {
            settingsPanel.SetActive(false);
            bgmToggle.onValueChanged.AddListener(OnBgmChanged);
            sfxToggle.onValueChanged.AddListener(OnSfxChanged);
        }

        public void ShowSettings(bool show)
        {
            SoundManager.Play(Sfx.Click);
            settingsPanel.SetActive(show);
        }

        void OnEnable()
        {
            var sm = SoundManager.Instance;
            bool bgm = sm != null ? sm.BgmOn : true;
            bool sfx = sm != null ? sm.SfxOn : true;


            bgmToggle.SetIsOnWithoutNotify(bgm);
            sfxToggle.SetIsOnWithoutNotify(sfx);
            RefreshLabels(bgm, sfx);
        }

        void OnBgmChanged(bool on)
        {
            if (SoundManager.Instance != null) SoundManager.Instance.SetBgm(on);
            SoundManager.Play(Sfx.Click);
            RefreshLabels(on, sfxToggle.isOn);
        }

        void OnSfxChanged(bool on)
        {
            if (SoundManager.Instance != null) SoundManager.Instance.SetSfx(on);
            SoundManager.Play(Sfx.Click);
            RefreshLabels(bgmToggle.isOn, on);
        }

        void RefreshLabels(bool bgm, bool sfx)
        {
            if (bgmLabel != null) bgmLabel.text = bgm ? "ON" : "OFF";
            if (sfxLabel != null) sfxLabel.text = sfx ? "ON" : "OFF";
        }
    }
}