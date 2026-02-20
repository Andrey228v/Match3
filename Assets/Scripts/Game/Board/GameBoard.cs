using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Levels;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private TileConfig _tileConfig;
        [SerializeField] private bool _isDebugging;

        private readonly List<Tile> _tilesToRefill = new List<Tile>();

        private MapGrid _grid;
        private BlankTileSetup _blankTileSetup;
        private TilePool _tilePool;
        private SetupCamera _setupCamera;
        private GameDebug _gameDebug;

        [Inject]
        public void Constructor(MapGrid grid, SetupCamera setupCamera, TilePool tilePool, GameDebug gameDebug, BlankTileSetup blankTileSetup)
        {
            _grid = grid;
            _setupCamera = setupCamera;
            _tilePool = tilePool;
            _gameDebug = gameDebug;
            _blankTileSetup = blankTileSetup;
        }

        private void Start()
        {
            _grid.SetupGrid(_levelConfig.Width, _levelConfig.Height);
            _blankTileSetup.SetupBlanks(_levelConfig);
            CreateBoard();
            _setupCamera.SetCamera(_grid.Width, _grid.Height, false);

            if(_isDebugging)
                _gameDebug.ShowDebug(transform);

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
                    if (_blankTileSetup.Blanks[x, y]) 
                    {
                        if (_grid.GetValue(x, y)) continue;
                        var blankTile = _tilePool.CreateBlankTile(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x, y, blankTile);
                    }
                    else
                    {
                        var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x, y, tile);
                        tile.gameObject.SetActive(true);
                        _tilesToRefill.Add(tile);
                    }   
                }
            }
        }
    }
}
