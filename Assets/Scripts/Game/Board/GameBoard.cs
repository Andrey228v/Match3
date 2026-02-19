using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        
        [SerializeField] private TileConfig _tileConfig;

        private readonly List<Tile> _tilesToRefill = new List<Tile>();

        private MapGrid _grid;
        private TilePool _tilePool;
        private SetupCamera _setupCamera;

        [Inject]
        public void Constructor(MapGrid grid, SetupCamera setupCamera, TilePool tilePool)
        {
            _grid = grid;
            _setupCamera = setupCamera;
            _tilePool = tilePool;
        }

        private void Start()
        {
            _grid.SetupGrid(10, 10);
            CreateBoard();
            _setupCamera.SetCamera(_grid.Width, _grid.Height, false);
        }

        public void CreateBoard()
        {
            FillBoard();
        }

        private void FillBoard()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GetValue(x, y)) continue;

                    var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), transform);
                    _grid.SetValue(x, y, tile);
                    tile.gameObject.SetActive(true);
                    _tilesToRefill.Add(tile);
                }
            }
        }
    }
}
