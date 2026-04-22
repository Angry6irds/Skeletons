using UnityEngine.UI;


namespace Alejandro
{
    public class SettingsUI : UIWindow
    {
        public Slider sfxVolume;
        public Slider musicVolume;

        public override void Initialize()
        {
            base.Initialize();
            sfxVolume.onValueChanged.AddListener(ChangeVolumeSFX);
        }

        private void ChangeVolumeSFX(float value)
        {
            SettingManager.instance.SetSFXVolume(value);
        }

        private void ChangeVolumeMusic(float value)
        {
            
        }
    }
}