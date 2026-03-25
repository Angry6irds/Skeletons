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
            PopupUI popupUI = UiManager.Instance.GetWindow(WindowsId.PopUpUI) as PopupUI;
            popupUI.SetText("Como llegaste aqui?");
        }
        
        public void HidePopup()
        {
            UiManager.Instance.HideWindow(WindowsId.SettingsUI);
        }
    }
}