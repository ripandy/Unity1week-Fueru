using System;
using Cysharp.Threading.Tasks;
using Pyra.VariableSystem;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PathOfThePast.ApplicationStateManagement
{
    public class ApplicationStateManager : MonoBehaviour
    {
        [SerializeField] private FloatVariable loadProgress;
        [SerializeField] private ApplicationStateVariable applicationState;
        
        private ApplicationStateEnum _activeState = ApplicationStateEnum.Boot;

        private void Start()
        {
            applicationState.Subscribe(async state =>
            {
                await UnloadScene(_activeState);
                await LoadScenes(state);
                _activeState = state;
            }).AddTo(this);
        }

        private async UniTask LoadScenes(ApplicationStateEnum state)
        {
            loadProgress.Value = 0;
            if (_activeState == ApplicationStateEnum.Boot)
            {
                await SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            }
            
            var activeScene = "Statics";
            if (state == ApplicationStateEnum.MainMenu)
            {
                await SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive)
                    .AsAsyncOperationObservable(new Progress<float>(progress => loadProgress.Value = progress));
                activeScene = "MainMenu";
            }
            else if (state == ApplicationStateEnum.GamePlay)
            {
                var sceneCount = 1;
                var sceneCountInv = 1f / sceneCount;
                
                // TODO : Load all scenes necessary
                Debug.Log("There's a TODO here!");
                
                await SceneManager.LoadSceneAsync("Stage", LoadSceneMode.Additive)
                    .AsAsyncOperationObservable(new Progress<float>(progress => loadProgress.Value += progress * sceneCountInv));
                await SceneManager.LoadSceneAsync("Character", LoadSceneMode.Additive)
                    .AsAsyncOperationObservable(new Progress<float>(progress => loadProgress.Value += progress * sceneCountInv));

                activeScene = "Stage";
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(activeScene));
        }

        private async UniTask UnloadScene(ApplicationStateEnum state)
        {
            if (state == ApplicationStateEnum.Boot)
            {
                await SceneManager.UnloadSceneAsync("Boot");
                await SceneManager.UnloadSceneAsync("SplashScreen");
            }
            else if (state == ApplicationStateEnum.MainMenu)
            {
                await SceneManager.UnloadSceneAsync("MainMenu");
            }
        }
    }
}