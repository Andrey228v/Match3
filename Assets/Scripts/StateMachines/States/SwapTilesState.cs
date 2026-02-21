using Assets.Scripts.Animations;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchTiles;
using Assets.Scripts.Game.Tiles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.StateMachines.States
{
    public class SwapTilesState : IState, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private MapGrid _grid;
        private IStateSwitcher _switcher;
        private IAnimation _animation;
        private MatchFinder _matchFinder;

        public SwapTilesState(MapGrid grid,  IStateSwitcher switcher, IAnimation animation, MatchFinder matchFinder)
        {
            _grid = grid;
            _switcher = switcher;
            _animation = animation;
            _matchFinder = matchFinder;
        }


        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }

        public async void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            await SwapTiles(_grid.CurrentPosition, _grid.TargetPosition);

            if(_matchFinder.CheckBoardForMatches(_grid) == false)
            {
                await SwapTiles(_grid.TargetPosition, _grid.CurrentPosition);
                _switcher.ChangeState<PlayerTurnState>();
            }
            else
            {
                _switcher.ChangeState<RemoveTileState>();
            }

            
        }

        public void Exit()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async UniTask SwapTiles(Vector2Int current, Vector2Int target)
        {
            var currentTile = _grid.GetValue(current.x, current.y);
            var targetTile = _grid.GetValue(target.x, target.y);

            MoveAnimation(currentTile, target);
            MoveAnimation(targetTile, current);

            _grid.SetValue(current.x, current.y, targetTile);
            _grid.SetValue(target.x, target.y, currentTile);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cancellationTokenSource.IsCancellationRequested);
        }

        private void MoveAnimation(Tile tileToMove, Vector2Int position)
        {
            _animation.MoveTile(tileToMove, _grid.GridToWorld(position.x, position.y), Ease.OutCubic);
        }

    }
}
