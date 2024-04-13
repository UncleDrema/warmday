using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private GameObject loadScreen;
        
        [SerializeField, InlineProperty]
        private SceneReference level;
        
        private bool _isSceneLoading = false;

        private void Start()
        {
            LoadGameLevel();
        }

        public void LoadGameLevel()
        {
            StartCoroutine(LoadLevel(level));
        }

        private IEnumerator LoadLevel(SceneReference levelScene)
        {
            loadScreen.SetActive(true);
            if (_isSceneLoading)
            {
                yield break;
            }

            _isSceneLoading = true;
            
            if (LevelLoaded)
                yield return UnloadLevel();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelScene.Path, LoadSceneMode.Additive);
            asyncLoad.completed += _ => SceneManager.SetActiveScene(SceneManager.GetSceneByPath(levelScene.Path));

            while (!asyncLoad.isDone)
                yield return null;

            _isSceneLoading = false;
            loadScreen.SetActive(false);
        }

        private IEnumerator UnloadLevel()
        {
            var operations = new List<AsyncOperation>(1);
            operations.Add(SceneManager.UnloadSceneAsync(level.Path));
            
            while (operations.Any(o => !o.isDone))
                yield return null;
        }

        private bool LevelLoaded => SceneManager.sceneCount > 1;
    }
    
    [Serializable]
    public struct SceneReference
    {
        [Scene]
        public string Path;
    }
}