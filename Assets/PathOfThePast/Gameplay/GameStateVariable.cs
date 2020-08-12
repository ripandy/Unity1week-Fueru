using Pyra.VariableSystem;
using UnityEngine;

namespace PathOfThePast.Gameplay
{
    public enum GameStateEnum
    {
        Initialize,
        GamePlay,
        LevelClear,
        GameOver
    }
    
    [CreateAssetMenu(fileName = "ApplicationState", menuName = "Pyra/Variables/ApplicationState")]
    public class GameStateVariable : VariableSystemBase<GameStateEnum>
    {
    }
}