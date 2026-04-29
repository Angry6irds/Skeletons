using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;


namespace Alejandro
{
    public class SettingManager : MonoBehaviour
    {
        [ReadOnly, SerializeField]private float _sfx = 0f;
        [ReadOnly, SerializeField]private float _music = 0f;
        
        public static SettingManager instance{get;private set;}

        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
        }

        public void SetSFXVolume(float value)
        {
            _sfx = value;
            Debug.Log(_sfx);
        }

        public void SetMusicVolume(float value)
        {
            _music = value;
            Debug.Log(_music);
        }
        
    }
}