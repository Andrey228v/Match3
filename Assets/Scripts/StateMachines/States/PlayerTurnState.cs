using Assets.Scripts.Animations;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Input;
using System;
using UnityEngine;

namespace Assets.Scripts.StateMachines.States
{
    public class PlayerTurnState : IState, IDisposable
    {
        private readonly Vector2Int _emptyPosition = Vector2Int.one * -1;
        private readonly InputReader _inputReader;
        private readonly MapGrid _grid;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Camera _camera;
        private readonly IAnimation _animation;

        public PlayerTurnState(MapGrid grid, IStateSwitcher stateSwitcher, IAnimation animation)
        {
            _inputReader = new InputReader();
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _camera = Camera.main;
            _inputReader.OnClick += TileClick;
        }

        public void Dispose()
        {
            _inputReader.OnClick -= TileClick;
        }

        public void Enter()
        {
            _inputReader.EnableInputs(true);
            DeselectTile();
        }

        public void Exit()
        {
            _inputReader.EnableInputs(false);
        }

        private void TileClick()
        {
            var clickPosition = _grid.WorldToGrid(_camera.ScreenToWorldPoint(_inputReader.Position()));

            if (IsValidPosition(clickPosition) == false || IsBlankPosition(clickPosition)) 
            {
                return;
            }

            if(_grid.CurrentPosition == _emptyPosition)
            {
                _grid.SetCurrentPosition(clickPosition);
                _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1.2f);
            }
            else if (_grid.CurrentPosition == clickPosition)
            {
                DeselectTile();
            }
            else if (_grid.CurrentPosition != clickPosition && IsSwappable(_grid.CurrentPosition, clickPosition))
            {
                _grid.SetTargetPosition(clickPosition);
                _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
                _stateSwitcher.ChangeState<SwapTilesState>();
            }
        }

        private void DeselectTile()
        {
            _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
            _grid.SetCurrentPosition(_emptyPosition);
            _grid.SetTargetPosition(_emptyPosition);
        }

        private bool IsValidPosition(Vector2Int gridPosition)
        {
            return gridPosition.x >= 0 && gridPosition.x < _grid.Width && gridPosition.y >= 0 && gridPosition.y < _grid.Height;
        }

        private bool IsBlankPosition(Vector2Int gridPosition)
        {
            return _grid.GetValue(gridPosition.x, gridPosition.y).TileConfig.TileKind == Game.Tiles.TileKind.Blank;
        }

        private bool IsSwappable(Vector2Int currentTilePosition, Vector2Int targetTilePosition)
        {
            return Mathf.Abs(currentTilePosition.x - targetTilePosition.x) + Mathf.Abs(currentTilePosition.y - targetTilePosition.y) == 1; 
        }

    }
}
