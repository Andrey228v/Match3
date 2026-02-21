using Assets.Scripts.Animations;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UnityEngine;

namespace Assets.Scripts.StateMachines.States
{
    public class RefillGridState : IState, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private MapGrid _grid;
        private IStateSwitcher _stateSwitcher;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private readonly Transform _parent;
        private List<Vector2Int> _tilesToRefillPosition = new List<Vector2Int>();
        private GameProgress _gameProgress;

        public RefillGridState(MapGrid grid, IStateSwitcher stateSwitcher, 
            IAnimation animation, MatchFinder matchFinder, TilePool tilePool, Transform parent, GameProgress gameProgress)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _parent = parent;
            _gameProgress = gameProgress;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        public async void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await FallTiles();
            await RefillGrid();

            if (_matchFinder.CheckBoardForMatches(_grid))
            {
                _stateSwitcher.ChangeState<RemoveTileState>();
            }
            else
            {
                CheckEndGame();
            }
        }

        public void Exit()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async UniTask FallTiles()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GetValue(x, y)) continue;

                    for (int i = y+1; i < _grid.Height; i++) 
                    {
                        if(_grid.GetValue(x, i) == null) continue;
                        if(_grid.GetValue(x, i).IsIntrectable == false) continue;

                        var tile = _grid.GetValue(x, i);
                        _grid.SetValue(x, y, tile);
                        _animation.MoveTile(tile, _grid.GridToWorld(x, y), Ease.InBack);
                        _grid.SetValue(x, i, null);
                        _tilesToRefillPosition.Add(new Vector2Int(x, i));
                        break;
                    }
                }
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), _cancellationTokenSource.IsCancellationRequested);
            _cancellationTokenSource?.Cancel();
        }

        private async UniTask RefillGrid()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if(_grid.GetValue(x, y) != null) continue;
                    var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), _parent);
                    tile.gameObject.SetActive(true);
                    _grid.SetValue(x, y, tile);
                    _animation.Reveal(tile.gameObject, 0.2f);

                    await UniTask.Delay(TimeSpan.FromSeconds(0.1f), _cancellationTokenSource.IsCancellationRequested);
                }
            }

            _cancellationTokenSource?.Cancel();
        }

        private void CheckEndGame()
        {
            if (_gameProgress.CheckGoalScore())
                _stateSwitcher.ChangeState<WinState>();
            else if(_gameProgress.Moves <= 0) 
                _stateSwitcher.ChangeState<LooseState>();
            else
                _stateSwitcher.ChangeState<PlayerTurnState>();
        }
    }
}
