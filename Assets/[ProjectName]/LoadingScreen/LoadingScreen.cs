using Pyra.Utilities;
using Pyra.VariableSystem;
using UniRx;
using UnityEngine;

namespace ProjectName.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private FloatVariable progress;

        private void Start()
        {
            progress.Subscribe(value => progress.PrintThis("Progress value")).AddTo(this);
        }
    }
}