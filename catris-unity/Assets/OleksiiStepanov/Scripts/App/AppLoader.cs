using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using OleksiiStepanov.Game;
using OleksiiStepanov.Utils;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace OleksiiStepanov.Loading
{
    public class AppLoader : SingletonBehaviour<AppLoader>
    {
        public string Platform;
        public string Version;
        public string BuildNumber;

        public GameObject LoaderCanvas = null;

        public void Start()
        {
            SetAppDetails();

            LoaderCanvas.SetActive(true);

            if (!SceneManager.GetSceneByName(Constants.MAIN_SCENE_NAME).isLoaded)
            {
                SceneManager.LoadSceneAsync(Constants.MAIN_SCENE_NAME, LoadSceneMode.Additive).completed += (obj) =>
                {
                };
            }

            DebugManager.instance.enableRuntimeUI = false;
        }

        private void SetAppDetails()
        {
            Platform = Application.platform.ToString();
            Version = Application.version;
#if UNITY_EDITOR
#if UNITY_ANDROID
            BuildNumber = PlayerSettings.Android.bundleVersionCode.ToString();
#elif UNITY_IOS
            BuildNumber = PlayerSettings.iOS.buildNumber;
#endif
#endif
        }
    }
}