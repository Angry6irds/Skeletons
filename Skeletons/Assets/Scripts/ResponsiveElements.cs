using NaughtyAttributes;
using UnityEngine;

namespace Alejandro
{
    public class ResponsiveElements : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private RectTransform _rectTransform;

        [Header("Mobile Anchors")] 
        [SerializeField] private Vector2 mobileAnchorMin = new Vector2(0, 0);
        [SerializeField] private Vector2 mobileAnchorsMax = new Vector2(0, 0);

        [Header("Tablet Anchors")] 
        [SerializeField] private Vector2 tabletAnchorsMin = new Vector2(0, 0);
        [SerializeField] private Vector2 tabletAnchorsMax = new Vector2(0, 0);

        ResponsiveManager _responsiveManager;

        void Start()
        {
            _responsiveManager = ResponsiveManager.Instance;
            _responsiveManager.OnScreenSizeChanged.AddListener(UpdateAnchors);
            UpdateAnchors();
        }

        private void UpdateAnchors()
        {
            if (_responsiveManager == null) return;
            if (_responsiveManager.CurrentDeviceType == ResponsiveManager.DeviceType.Mobile)
            {
                SetMobileAnchors();
            }
            else if (_responsiveManager.CurrentDeviceType == ResponsiveManager.DeviceType.Tablet)
            {
                SetTabletAnchors();
            }
        }

        private void SetTabletAnchors()
        {
            _rectTransform.anchorMin = tabletAnchorsMin;
            _rectTransform.anchorMax = tabletAnchorsMax;
        }

        private void SetMobileAnchors()
        {
            _rectTransform.anchorMin = mobileAnchorMin;
            _rectTransform.anchorMax = mobileAnchorsMax;
        }

        [Button]
        private void SaveMobileAnchors()
        {
            Vector2 maxAnchors = _rectTransform.anchorMax;
            Vector2 minAnchors = _rectTransform.anchorMin;

            mobileAnchorMin = minAnchors;
            mobileAnchorsMax = maxAnchors;
        }

        [Button]
        private void SaveTabletAnchors()
        {
            Vector2 maxAnchors = _rectTransform.anchorMax;
            Vector2 minAnchors = _rectTransform.anchorMin;

            tabletAnchorsMax = maxAnchors;
            tabletAnchorsMin = minAnchors;
        }
    }
}