using DG.Tweening;
using UnityEngine;

namespace Alejandro
{
    public class PopupUI : UIWindow
    {
        [SerializeField] private RectTransform popupRectTransform;
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

        public override void Initialize()
        {
            popupRectTransform.DOScale(Vector3.zero, 0f);

            Debug.Log(popupRectTransform + $"si sirve");
            //popupRectTransform.gameObject.SetActive(hideOnStart);
            //popupRectTransform.localScale = Vector3.zero;
            popupRectTransform.DOMoveY(1500, 0f);
        }
    }
}


