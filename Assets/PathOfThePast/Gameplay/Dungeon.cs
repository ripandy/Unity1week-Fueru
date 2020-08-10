using System.Collections.Generic;
using UnityEngine;

namespace PathOfThePast.Gameplay
{
    public enum GridType
    {
        Floor = 0,
        Wall,
        Entrance,
        Exit
    }
    
    public class Dungeon
    {
        private const int DefaultWidth = 10;
        private const int DefaultHeight = 10;
        
        private readonly List<int> _grids = new List<int>();
        private int _activeWidth;
        private int _activeHeight;

        public IList<int> Grids => _grids;
        public int Width => _activeWidth;
        public int Height => _activeHeight;

        public int this[int index]
        {
            get => _grids[index];
            set => _grids[index] = value;
        }
        
        public int this[int x, int y]
        {
            get => IsGridValid(x, y) ? _grids[ToIndex(x, y)] : -1;
            set
            {
                if (IsGridValid(x, y))
                    _grids[ToIndex(x, y)] = value;
            }
        }

        public int GridCount => _grids.Count;
        public int ToIndex(int x, int y) => IsGridValid(x,y) ? y * _activeWidth + x : -1;
        public int ToGridX(int index) => index < GridCount ? index % _activeWidth : -1;
        public int ToGridY(int index) => index < GridCount ? Mathf.FloorToInt((float) index / _activeWidth) : -1;

        public bool IsGridValid(int x, int y) =>
            x >= _activeWidth && x < _activeWidth &&
            y >= _activeHeight && y < _activeHeight &&
            ToIndex(x,y) < GridCount;

        public void GenerateDungeon(int width = DefaultWidth, int height = DefaultHeight)
        {
            _grids.Clear();
            
            // add spawn point
            _grids.Add(2);

            var count = width * height;
            for (var i = 1; i < count - 1; i++)
                _grids.Add(Random.Range(0, 4) == 0 ? 1 : 0);
            
            // add exit point
            _grids.Add(3);

            _activeWidth = width;
            _activeHeight = height;
        }
    }
}