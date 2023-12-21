using System;
using OleksiiStepanov.Utils;
using UnityEngine;

namespace OleksiiStepanov.App
{
    public class DeviceManager : SingletonBehaviour<DeviceManager>
    {
        public bool IsInitialized { get; private set; }

        public event Action Initialized;

        private int initialResolutionWidth;
        private int initialResolutionHeight;

        public void Init(Action onSuccess = null, Action onFailure = null)
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // Initialize the random state
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
            IsInitialized = true;
            Initialized?.Invoke();
            
            initialResolutionHeight = Screen.height;
            initialResolutionWidth = Screen.width;
        }

        public void ScaleScreenResolution()
        {
            int width, height;

            float aspectRatio = Screen.height / (float)Screen.width;
            if (aspectRatio < 1.35f)
            {
                width = Math.Min(Screen.width, 768);
            }
            else
            {
                width = Math.Min(Screen.width, 720);
            }

            height = (int)(width * aspectRatio);

            Screen.SetResolution(width, height, true);
        }

        public void ScaleScreenResolution(float scale)
        {
            var width = (int)(Screen.width * scale);
            var height = (int)(Screen.height * scale);

            Screen.SetResolution(width, height, true);
        }

        public void ScaleResolutionForFacebookLogin()
        {
            const int scale = 400;
            float aspectRatio = Screen.height / (float)Screen.width;

            Screen.SetResolution(scale, (int) (scale * aspectRatio), true);
        }

        public void ResetResolutionToDefault()
        {
            Screen.SetResolution(initialResolutionWidth, initialResolutionHeight, true);
        }
    }
}