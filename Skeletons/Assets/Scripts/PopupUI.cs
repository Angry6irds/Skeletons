using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Alejandro
{
    public class PopupUI : UIWindow
    {
        [SerializeField] private TextMeshPro titleText;
        [SerializeField] private RectTransform popupRectTransform;
        [SerializeField] private Button noButton;
        [SerializeField] private Button yesButton;
        
        public override void Initialize()
        {
            popupRectTransform.DOScale(Vector3.zero, 0f);
            Debug.Log(popupRectTransform + $"si sirve");
            //popupRectTransform.gameObject.SetActive(hideOnStart);
            //popupRectTransform.localScale = Vector3.zero;
            popupRectTransform.DOMoveY(1500, 0f);
            noButton.onClick.AddListener(OnNoButtonClicked);
            yesButton.onClick.AddListener(OnYesButtonClicked);
        }
        
        public override void Show()
        {
            //popupRectTransform.gameObject.SetActive(true);
            popupRectTransform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).OnComplete(() =>
                {

                    ;
                    popupRectTransform.DOMoveY(500, .5f);

                }
                );
        }

        public override void Hide()
        {
            popupRectTransform.DOMoveY(1000, .5f).SetEase(Ease.InBack).OnComplete(() =>
                {

                    popupRectTransform.DOScale(Vector3.zero, .5f);
                    

                }
            );
        }

        private void OnYesButtonClicked()
        {
            Debug.Log("Yes button clicked!");
        }

        private void OnNoButtonClicked()
        {
            Debug.Log("No button clicked");
        }

        private void OnDestroy()
        {
            yesButton.onClick.RemoveListener(OnYesButtonClicked);
            noButton.onClick.RemoveListener(OnNoButtonClicked);
        }

        public void SetText(string content)
        {
            titleText.text = content;
        }
    }
}


