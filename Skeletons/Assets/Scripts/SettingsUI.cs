using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
            canvas.gameObject.SetActive(true);
            UpdateSliders();
            
            rectTransform.localScale = Vector3.zero;
            rectTransform.localRotation = Quaternion.Euler(0, 0, 15f);
            canvasGroup.alpha = 0f;

            canvasGroup.DOFade(1f, 0.4f);
            rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            rectTransform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.OutBack);
        }

        public override void Hide()
        {
            canvasGroup.DOFade(0f, 0.4f);
            rectTransform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack);
            rectTransform.DORotate(new Vector3(0, 0, -15f), 0.4f).SetEase(Ease.InBack).OnComplete(() => canvas.gameObject.SetActive(false));
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