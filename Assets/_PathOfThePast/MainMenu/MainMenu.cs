using PathOfThePast.ApplicationStateManagement;
using Pyra.EventSystem;
using UniRx;
using UnityEngine;

namespace PathOfThePast.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private ApplicationStateVariable applicationState;
        [SerializeField] private GameEvent gameStartEvent;

        private void Start()
        {
            gameStartEvent.OnEventRaise
                .Subscribe(_ => applicationState.Value = ApplicationStateEnum.GamePlay)
                .AddTo(this);
        }
    }
}