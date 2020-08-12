using System.Collections.Generic;
using Pyra.VariableSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PathOfThePast.Gameplay
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private IntVariable playerGrid;
        
        [Title("Stage Size")]
        [SerializeField] private IntVariable width;
        [SerializeField] private IntVariable height;
        
        [Title("View")]
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Color[] gridColors;
        
        private readonly Dungeon _dungeon = new Dungeon();
        private readonly List<SpriteRenderer> _tileSprites = new List<SpriteRenderer>();
        
        private const float GridSize = 2f;

        private void Start()
        {
            _dungeon.GenerateDungeon(width, height);
            InitializeGrids();
        }

        private void InitializeGrids()
        {
            var grids = _dungeon.Grids;
            var gridCount = _dungeon.GridCount;
            var count = Mathf.Max(gridCount, _tileSprites.Count);
            for (var i = 0; i < count; i++)
            {
                var grid = grids[i];
                SpriteRenderer sr;
                GameObject go;
                if (i < _tileSprites.Count)
                {
                    sr = _tileSprites[i];
                    go = sr.gameObject;
                }
                else
                {
                    go = Instantiate(tilePrefab, transform);
                    sr = go.GetComponentInChildren<SpriteRenderer>();
                    if (sr != null)
                        _tileSprites.Add(sr);
                }

                if (i < gridCount)
                {
                    sr.color = gridColors[grid];
                    
                    var tr = go.transform;
                    tr.localPosition = GetWorldPosition(i, width, height);

                    var text = go.GetComponentInChildren<TMP_Text>();
                    if (text != null)
                        text.text = ((GridType) grid).ToString();
                }

                go.SetActive(i < gridCount);

                if (grid == (int) GridType.Entrance)
                    playerGrid.Value = i;
            }
        }

        public static Vector3 GetWorldPosition(int gridIndex, int width, int height)
        {
            var pos = Vector3.zero;
            
            var dx = GridHelper.ToGridX(gridIndex, width, height) * GridSize;
            var dy = GridHelper.ToGridY(gridIndex, width, height) * GridSize;
            var startX = (-width / 2f + (width % 2 == 0 ? 0.5f : 0)) * GridSize;
            var startY = (height / 2f - (height % 2 == 0 ? 0.5f : 0)) * GridSize;
            pos.x = startX + dx;
            pos.y = startY - dy;
            
            return pos;
        }
    }
}