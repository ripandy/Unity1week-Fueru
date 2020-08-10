using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PathOfThePast.Gameplay
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Color[] gridColors;
        
        private readonly Dungeon _dungeon = new Dungeon();
        private readonly List<SpriteRenderer> _tileSprites = new List<SpriteRenderer>();

        private void Start()
        {
            _dungeon.GenerateDungeon();
            InitializeGrids();
        }

        private void InitializeGrids()
        {
            var grids = _dungeon.Grids;
            var gridCount = _dungeon.GridCount;
            var width = _dungeon.Width;
            var height = _dungeon.Height;
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
                    
                    var dx = _dungeon.ToGridX(i) * 2;
                    var dy = _dungeon.ToGridY(i) * 2;
                    var startX = (-width / 2f + (width % 2 == 0 ? 0.5f : 0)) * 2;
                    var startY = (height / 2f - (height % 2 == 0 ? 0.5f : 0)) * 2;
                    var pos = tr.localPosition;
                        pos.x = startX + dx;
                        pos.y = startY - dy;
                    tr.localPosition = pos;

                    var text = go.GetComponentInChildren<TMP_Text>();
                    if (text != null)
                        text.text = ((GridType) grid).ToString();
                }

                go.SetActive(i < gridCount);
            }
        }
    }
}