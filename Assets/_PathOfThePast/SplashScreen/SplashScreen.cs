using System.Collections.Generic;
using PathOfThePast.ApplicationStateManagement;
using Cysharp.Threading.Tasks;
using Pyra.Components;
using UnityEngine;

namespace PathOfThePast.LoadingScreen
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] private List<Fadeable> fadeables;
        [SerializeField] private ApplicationStateVariable applicationState;

        private async void Start()
        {
            List<UniTask> tasks = new List<UniTask>();
            fadeables.ForEach(fadeable => tasks.Add(fadeable.FadeInOut(1f, 3f, 1f)));
            await tasks;

            applicationState.Value = ApplicationStateEnum.MainMenu;
        }
    }
}
