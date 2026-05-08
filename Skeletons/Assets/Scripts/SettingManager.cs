using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;


namespace Alejandro
{
    public class SettingManager : MonoBehaviour
    {
        [Header("Audio Components")]
        [SerializeField] private AudioSource musicSource; 
        
        [Header("Music Tracks")]
        [SerializeField] private AudioClip menuMusic;
        [SerializeField] private AudioClip gameplayMusic;

        [Header("Volume Values")]
        [SerializeField] private float _sfx = 0.5f;
        [SerializeField] private float _music = 0.5f;
        
        public float SFXVolume => _sfx;
        public float MusicVolume => _music;

        public static SettingManager instance{get;private set;}

        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (musicSource != null)
            {
                musicSource.volume = _music;
            }
        }

        public void SetSFXVolume(float value)
        {
            _sfx = value;
            Debug.Log(_sfx);
        }

        public void SetMusicVolume(float value)
        {
            _music = value;
            if (musicSource != null)
            {
                musicSource.volume = _music;
            }
        }

        public void PlayMenuMusic()
        {
            ChangeMusic(menuMusic);
        }

        public void PlayGameplayMusic()
        {
            ChangeMusic(gameplayMusic);
        }

        public void ChangeMusic(AudioClip newClip)
        {
            if (musicSource == null || newClip == null) return;
            
            if (musicSource.clip == newClip) return;

            musicSource.clip = newClip;
            musicSource.Play();
        }
        
    }
}