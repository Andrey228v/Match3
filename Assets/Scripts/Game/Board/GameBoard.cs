using Assets.Scripts.Animations;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchTiles;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Input;
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
        private IAnimation _animation;
        private MatchFinder _matchFinder;

        [Inject]
        public void Constructor(MapGrid grid, SetupCamera setupCamera, 
            TilePool tilePool, GameDebug gameDebug, 
            BlankTileSetup blankTileSetup, IAnimation animation, MatchFinder matchFinder)
        {
            _grid = grid;
            _setupCamera = setupCamera;
            _tilePool = tilePool;
            _gameDebug = gameDebug;
            _blankTileSetup = blankTileSetup;
            _animation = animation;
            _matchFinder = matchFinder;
        }

        private void Awake()
        {
            _grid.SetupGrid(_levelConfig.Width, _levelConfig.Height);
            _blankTileSetup.SetupBlanks(_levelConfig);
            _setupCamera.SetCamera(_grid.Width, _grid.Height, false);

            if(_isDebugging)
                _gameDebug.ShowDebug(transform);

        }

        public void CreateBoard()
        {
            FillBoard();
            while (_matchFinder.CheckBoardForMatches(_grid))
            {
                ClearBoard();
                FillBoard();
            }
            _matchFinder.ClearTilesToRemove();
            RevealTiles();
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

        private void RevealTiles()
        {
            foreach (var tile in _tilesToRefill)
            {
                var gameObjectTile = tile.gameObject;
                _animation.Reveal(gameObjectTile, 1f);
            }
        }

        private void ClearBoard()
        {
            if(_tilesToRefill == null) return;

            foreach(var tile in _tilesToRefill)
            {
                _grid.SetValue(tile.transform.position, null);
                tile.gameObject.SetActive(false);
            }

            _tilesToRefill.Clear();
        }

    }
}
