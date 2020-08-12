using Pyra.EventSystem;
using Pyra.Interactive;
using Pyra.VariableSystem;
using UniRx;
using UnityEngine;

namespace PathOfThePast.Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private IntVariable playerGrid;
        [SerializeField] private IntVariable stageWidth;
        [SerializeField] private IntVariable stageHeight;
        
        [SerializeField] private GameEvent moveUp;
        [SerializeField] private GameEvent moveDown;
        [SerializeField] private GameEvent moveLeft;
        [SerializeField] private GameEvent moveRight;

        [SerializeField] private Vector3Variable playerPosition;

        private void Start()
        {
            moveUp.OnEventRaise.Subscribe(_ => UpdatePosition(DirectionEnum.Up)).AddTo(this);
            moveDown.OnEventRaise.Subscribe(_ => UpdatePosition(DirectionEnum.Down)).AddTo(this);
            moveLeft.OnEventRaise.Subscribe(_ => UpdatePosition(DirectionEnum.Left)).AddTo(this);
            moveRight.OnEventRaise.Subscribe(_ => UpdatePosition(DirectionEnum.Right)).AddTo(this);

            playerGrid.Subscribe(grid => 
                    playerPosition.Value = Stage.GetWorldPosition(grid, stageWidth, stageHeight))
                .AddTo(this);
        }

        private void UpdatePosition(DirectionEnum direction)
        {
            var gridX = GridHelper.ToGridX(playerGrid, stageWidth, stageHeight);
            var gridY = GridHelper.ToGridY(playerGrid, stageWidth, stageHeight);

            switch (direction)
            {
                case DirectionEnum.Up:
                    gridY--;
                    break;
                case DirectionEnum.Down:
                    gridY++;
                    break;
                case DirectionEnum.Left:
                    gridX--;
                    break;
                case DirectionEnum.Right:
                    gridX++;
                    break;
            }

            gridX = Mathf.Clamp(gridX, 0, stageWidth);
            gridY = Mathf.Clamp(gridY, 0, stageHeight);
            
            playerGrid.Value = GridHelper.ToIndex(gridX, gridY, stageWidth, stageHeight);
        }
    }
}