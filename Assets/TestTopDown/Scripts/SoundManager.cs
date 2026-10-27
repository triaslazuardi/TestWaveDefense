using System;
using System.Collections.Generic;
using UnityEngine;


namespace Nitzz.utility.Sounds { 

    public enum Sfx { Click, Build, Shoot, EnemyDie, FinishGame }

    [Serializable]
    public class SfxEntry
    {
        public Sfx id;
        public AudioClip clip;
    }

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField] AudioSource bgmSource;
        [SerializeField] AudioSource sfxSource;
        [SerializeField] AudioClip[] bgmClip; //0:main menu/ 1+game
        [SerializeField] SfxEntry[] sfxClips;

        const string BgmKey = "bgm_on";
        const string SfxKey = "sfx_on";

        readonly Dictionary<Sfx, AudioClip> lookup = new Dictionary<Sfx, AudioClip>();

        public bool BgmOn { get; private set; }
        public bool SfxOn { get; private set; }

        void Awake()
        {
       
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (var e in sfxClips) lookup[e.id] = e.clip;

            BgmOn = PlayerPrefs.GetInt(BgmKey, 1) == 1;
            SfxOn = PlayerPrefs.GetInt(SfxKey, 1) == 1;

            bgmSource.mute = !BgmOn;
            sfxSource.mute = !SfxOn;

            PlayBgm(0);
        }

        public static void PlayBgm(int arrbgm) {
            Instance.bgmSource.clip = Instance.bgmClip[arrbgm];
            Instance.bgmSource.loop = true;
            if (Instance.bgmClip != null) Instance.bgmSource.Play();
        }

        public void SetBgm(bool on)
        {
            BgmOn = on;
            bgmSource.mute = !on;
            PlayerPrefs.SetInt(BgmKey, on ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetSfx(bool on)
        {
            SfxOn = on;
            sfxSource.mute = !on;
            PlayerPrefs.SetInt(SfxKey, on ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static void Play(Sfx id)
        {
            if (Instance == null || !Instance.SfxOn) return;
            if (Instance.lookup.TryGetValue(id, out var clip) && clip != null)
                Instance.sfxSource.PlayOneShot(clip);
        }
    }

}
