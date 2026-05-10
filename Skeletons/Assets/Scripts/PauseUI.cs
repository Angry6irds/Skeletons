using UnityEngine;
using UnityEngine.UI;

namespace Alejandro
{
    public class PauseUI : UIWindow
    {
        [Header("Pause References")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitToMenuButton;

        public override void Initialize()
        {
            base.Initialize();
            if (resumeButton != null) resumeButton.onClick.AddListener(OnResumeClicked);
            if (settingsButton != null) settingsButton.onClick.AddListener(OnSettingsClicked);
            if (quitToMenuButton != null) quitToMenuButton.onClick.AddListener(OnQuitClicked);
        }

        public override void Show()
        {
            base.Show();
            Time.timeScale = 0f;
        }

        public override void Hide()
        {
            base.Hide();
            Time.timeScale = 1f;
        }

        private void OnResumeClicked()
        {
            UiManager.Instance.HideWindow(WindowsId.PauseUI);
        }

        private void OnSettingsClicked()
        {
            UiManager.Instance.ShowWindow(WindowsId.SettingsUI);
        }

        private void OnQuitClicked()
        {
            Time.timeScale = 1f;
            UiManager.Instance.HideWindow(WindowsId.PauseUI);
            UiManager.Instance.HideWindow(WindowsId.GameplayUI);
            UiManager.Instance.ShowWindow(WindowsId.MenuUI);
        }

        private void OnDestroy()
        {
            if (resumeButton != null) resumeButton.onClick.RemoveListener(OnResumeClicked);
            if (settingsButton != null) settingsButton.onClick.RemoveListener(OnSettingsClicked);
            if (quitToMenuButton != null) quitToMenuButton.onClick.RemoveListener(OnQuitClicked);
        }
    }
}