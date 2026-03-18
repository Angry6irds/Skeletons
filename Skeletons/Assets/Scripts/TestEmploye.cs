using UnityEngine;
using NaughtyAttributes;

namespace Alejandro
{
    public class TestEmploye : MonoBehaviour
    {
        [Button]
        public void OnStart()
        {
            ShowPopup();
        }
        public void ShowPopup()
        {
            UiManager.Instance.ShowWindow(WindowsId.PopUpUI);
        }
        
        public void HidePopup()
        {
            UiManager.Instance.HideWindow(WindowsId.SettingsUI);
        }
    }
}