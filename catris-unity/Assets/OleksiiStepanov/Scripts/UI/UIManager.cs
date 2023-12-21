using UnityEngine;
using System;
using OleksiiStepanov.Utils;

namespace OleksiiStepanov.UI
{
    public class UIManager : SingletonBehaviour<UIManager>
    {
        protected virtual void OnRectTransformDimensionsChange()
        { }

        public event Action CanvasSizeChanged;

        [Header("Camera and Canvas")]
        public Camera UICamera;
        public Canvas MainCanvas;

        [Header("UI Container References")]
        [Space(5)]
        public UILayer SafeAreaView;
        public UILayer FullScreenAreaView;

        [Header("Safe Area and Aspect Ratio")]
        [SerializeField] private RectTransform safeAreaRectTransform;
        [SerializeField] private RectTransform fullScreenRectTransform;
        [SerializeField] private RectTransform mainCanvasRectTransform;
        [SerializeField] private float defaultAspectRatio;

        [Header("Presentation coordinates")]
        [ReadOnly] public Vector3 TopLeftCornerWorldSpace;
        [ReadOnly] public Vector3 TopRightCornerWorldSpace;
        [ReadOnly] public Vector3 BottomLeftCornerWorldSpace;
        [ReadOnly] public Vector3 BottomRightCornerWorldSpace;
        [ReadOnly] public Vector3 SafeAreaTopLeftCornerWorldSpace;
        [ReadOnly] public Vector3 SafeAreaTopRightCornerWorldSpace;
        [ReadOnly] public Vector3 SafeAreaBottomLeftCornerWorldSpace;
        [ReadOnly] public Vector3 SafeAreaBottomRightCornerWorldSpace;
        [ReadOnly] public Rect FullScreenRect;
        [ReadOnly] public float SafeAreaTopOffset;
        [ReadOnly] public float SafeAreaBottomOffset;
        [ReadOnly] public float MainCanvasTransformScale;
        [ReadOnly] public float MainCanvasInverseTransformScale;
        [ReadOnly] public readonly Vector2 ReferenceResolution = new Vector2(1125f, 2436f);
        [ReadOnly] public readonly Vector2 ZFold2ScaledResolution = new Vector2(880f, 2436f);

        public bool IsPhone { get; private set; }
        public float AspectRatio { get; private set; }
        public float ScreenDiagonalInches { get; private set; }

        [Header("Misc")]
        public GameObject InputBlocker;
        private bool inputActive;
        public bool InputActive
        {
            get { return inputActive; }
            set
            {
                inputActive = value;
                InputBlocker.SetActive(!inputActive);
            }
        }

        #region Initialization

        public void Init(Action onSuccess = null)
        {
            SetDeviceProperties();

            InputActive = true;

            CalculateSafeAreaAndAspectRatio();

            onSuccess?.Invoke();
        }

        private void SetDeviceProperties()
        {
            AspectRatio = Mathf.Max(Screen.width, Screen.height) / (float)Mathf.Min(Screen.width, Screen.height);
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;

            ScreenDiagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

#if UNITY_IOS && !UNITY_EDITOR
            IsPhone = !UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
#else
            IsPhone = !(ScreenDiagonalInches > 6.5f && AspectRatio < 2f);
#endif
        }

        #endregion

        [ContextMenu("Calculate Safe Area And Aspect Ratio")]
        public void CalculateSafeAreaAndAspectRatio()
        {
            var safeArea = Screen.safeArea;
            float canvasScaleFactor = 1f;

            var yMinOffset = Mathf.Floor(safeArea.position.y / canvasScaleFactor);

#if UNITY_EDITOR
            var yMaxOffset = Mathf.Ceil(((Application.isPlaying ? Screen.height : UnityEngine.Device.Screen.height) - safeArea.size.y - safeArea.position.y) / canvasScaleFactor);
#else
            var yMaxOffset = Mathf.Ceil((Screen.height - safeArea.size.y - safeArea.position.y) / canvasScaleFactor);
#endif

            float aspectRatio = (float)Screen.width / Screen.height;

            if (aspectRatio > defaultAspectRatio)
            {
                aspectRatio = defaultAspectRatio;
            }

            fullScreenRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullScreenRectTransform.rect.height * aspectRatio);
            FullScreenRect = fullScreenRectTransform.rect;
            SafeAreaTopOffset = yMaxOffset;
            SafeAreaBottomOffset = yMinOffset;
            safeAreaRectTransform.offsetMin = new Vector2(fullScreenRectTransform.offsetMin.x, yMinOffset);
            safeAreaRectTransform.offsetMax = new Vector2(fullScreenRectTransform.offsetMax.x, -yMaxOffset);

            // Viewport corners in world space
            if (UICamera != null)
            {
                TopLeftCornerWorldSpace = UICamera.ViewportToWorldPoint(new Vector3(0f, 1f, 20f));
                TopRightCornerWorldSpace = UICamera.ViewportToWorldPoint(new Vector3(1f, 1f, 20f));
                BottomLeftCornerWorldSpace = UICamera.ViewportToWorldPoint(new Vector3(0f, 0f, 20f));
                BottomRightCornerWorldSpace = UICamera.ViewportToWorldPoint(new Vector3(1f, 0f, 20f));
            }

            // Safe area corners in world space
            var safeAreaCorners = new Vector3[4];
            safeAreaRectTransform.GetWorldCorners(safeAreaCorners);
            SafeAreaTopLeftCornerWorldSpace = safeAreaCorners[1];
            SafeAreaTopRightCornerWorldSpace = safeAreaCorners[2];
            SafeAreaBottomLeftCornerWorldSpace = safeAreaCorners[0];
            SafeAreaBottomRightCornerWorldSpace = safeAreaCorners[3];

            // Canvas is scaled uniformly on all axes so no need to cache the vector3
            MainCanvasTransformScale = MainCanvas.GetComponent<Transform>().localScale.y;
            MainCanvasInverseTransformScale = 1f / MainCanvasTransformScale;

            CanvasSizeChanged?.Invoke();
        }
    }
}


