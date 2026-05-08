using UnityEngine;
using UnityEngine.UI;

namespace Alejandro
{
    public class SettingsUI : UIWindow
    {
        [Header("Settings References")]
        public Slider sfxVolume;
        public Slider musicVolume;
        public Button backButton;

        public override void Initialize()
        {
            base.Initialize();
            
            
            if (sfxVolume != null) sfxVolume.onValueChanged.AddListener(ChangeVolumeSFX);
            if (musicVolume != null) musicVolume.onValueChanged.AddListener(ChangeVolumeMusic);
            if (backButton != null) backButton.onClick.AddListener(OnBackClicked);
            
            UpdateSliders();
        }

        public override void Show()
        {
            base.Show();
            UpdateSliders();
        }

        private void UpdateSliders()
        {
            if (SettingManager.instance == null) return;

            if (sfxVolume != null) sfxVolume.SetValueWithoutNotify(SettingManager.instance.SFXVolume);
            if (musicVolume != null) musicVolume.SetValueWithoutNotify(SettingManager.instance.MusicVolume);
        }

        private void ChangeVolumeSFX(float value)
        {
            if (SettingManager.instance != null)
            {
                SettingManager.instance.SetSFXVolume(value);
            }
        }

        private void ChangeVolumeMusic(float value)
        {
            if (SettingManager.instance != null)
            {
                SettingManager.instance.SetMusicVolume(value);
            }
        }

        private void OnBackClicked()
        {
            UiManager.Instance.HideWindow(WindowId);
            UiManager.Instance.ShowWindow(WindowsId.MenuUI);
        }

        private void OnDestroy()
        {
            if (sfxVolume != null) sfxVolume.onValueChanged.RemoveListener(ChangeVolumeSFX);
            if (musicVolume != null) musicVolume.onValueChanged.RemoveListener(ChangeVolumeMusic);
            if (backButton != null) backButton.onClick.RemoveListener(OnBackClicked);
        }
    }
}