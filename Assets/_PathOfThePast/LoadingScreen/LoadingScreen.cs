using Pyra.VariableSystem;
using UniRx;
using UnityEngine;

namespace PathOfThePast.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private FloatVariable loadProgress;

        private void Start() => loadProgress.Subscribe(value => Debug.Log($"Load Progress: {value}")).AddTo(this);
    }
}