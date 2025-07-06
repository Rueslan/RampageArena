using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        public static float _volume;
        public enum audioClip
        { 
            AmmoPickUp,
            Throw,
            RockHit,
            WoodHit,
            Heartbeat,
            BodyHit,
            Death
        }

        private Dictionary<audioClip, AudioSource> audios = new Dictionary<audioClip, AudioSource>();

        public AudioSource mainSource;
        public AudioClip buttonClick;
        [Space]
        [SerializeField] private AudioSource ammoPickUpSound;
        [SerializeField] private AudioSource throwSound;
        [SerializeField] private AudioSource hitSound;
        [SerializeField] private AudioSource woodSound;
        [SerializeField] private AudioSource rockSound;
        [SerializeField] private AudioSource heartbeatSound;
        [SerializeField] private AudioSource deathSound;

        private void OnValidate()
        {
            SetVolume(_volume);
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this);

            FillDictionary();
            SetVolume(PlayerPrefs.GetFloat("SoundVolume"));
        }

        private void FillDictionary()
        {
            audios.Add(audioClip.AmmoPickUp, ammoPickUpSound);
            audios.Add(audioClip.Throw, throwSound);
            audios.Add(audioClip.BodyHit, hitSound);
            audios.Add(audioClip.WoodHit, woodSound);
            audios.Add(audioClip.RockHit, rockSound);
            audios.Add(audioClip.Heartbeat, heartbeatSound);
            audios.Add(audioClip.Death, deathSound);
        }

        public void PlayAudioClip(AudioClip audioClip)
        {
            mainSource.clip = audioClip;
            mainSource?.Play();
        }

        public void PlaySound(audioClip clip)
        {
            if (!audios[clip].isPlaying)
                audios[clip]?.Play();
        }

        public void StopSound(audioClip clip)
        {
            audios[clip]?.Stop();
        }

        public void PlayButtonClickSound()
        {
            PlayAudioClip(buttonClick);
        }

        public void SetVolume(float volume)
        {
            audios.Select(x => x.Value.volume = volume);
        }
    }
}
