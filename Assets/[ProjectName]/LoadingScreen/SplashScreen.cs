using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Pyra.Components;
using Pyra.EventSystem;
using UnityEngine;

namespace ProjectName.LoadingScreen
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] private List<Fadeable> fadeables;
        [SerializeField] private GameEvent splashDone;

        private async void Start()
        {
            List<UniTask> tasks = new List<UniTask>();
            fadeables.ForEach(fadeable => tasks.Add(fadeable.FadeInOut(1f, 3f, 1f)));
            await tasks;

            splashDone.RaiseEvent();
        }
    }
}
