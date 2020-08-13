using UnityEngine;

namespace PathOfThePast.Gameplay
{
    public static class GridHelper
    {
        public static int GridCount(int width, int height) => width * height;

        public static int ToIndex(int x, int y, int width, int height) =>
            IsGridValid(x, y, width, height) ? y * width + x : -1;

        public static int ToGridX(int index, int width, int height) =>
            index < GridCount(width, height) ? index % width : -1;

        public static int ToGridY(int index, int width, int height) =>
            index < GridCount(width, height) ? Mathf.FloorToInt((float) index / width) : -1;
        
        public static bool IsGridValid(int x, int y, int width, int height) =>
            x >= 0 && x < width &&
            y >= 0 && y < height &&
            y * width + x < GridCount(width, height);
    }
}