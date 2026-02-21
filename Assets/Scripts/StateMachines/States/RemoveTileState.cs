using Assets.Scripts.Animations;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.StateMachines.States
{
    public class RemoveTileState : IState, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private MapGrid _grid;
        private IStateSwitcher _stateSwitcher;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private ScoreCalculator _scoreCalculator;

        public RemoveTileState(MapGrid grid, IStateSwitcher stateSwitcher, 
            IAnimation animation, MatchFinder matchFinder, ScoreCalculator scoreCalculator)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _scoreCalculator = scoreCalculator;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        public async void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _scoreCalculator.CalcilateScoreToAdd(_matchFinder.CurrentMatchResult.MatchDirection);
            await RemoveTiles(_matchFinder.TilesToRemove, _grid);
            _stateSwitcher.ChangeState<RefillGridState>();
        }

        public void Exit()
        {
            _matchFinder.ClearTilesToRemove();
            _cancellationTokenSource.Cancel();
        }

        private async UniTask RemoveTiles(List<Tile> tilesToRemove, MapGrid grid)
        {
            foreach (var tile in tilesToRemove) 
            {
                var position = grid.WorldToGrid(tile.transform.position);
                grid.SetValue(position.x, position.y, null);
                await _animation.HideTile(tile.gameObject, 0.5f);
            }

            _cancellationTokenSource.Cancel();
        }
    }
}
