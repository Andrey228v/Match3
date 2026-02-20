using Assets.Scripts.Game.Board;
using UnityEngine;


namespace Assets.Scripts.StateMachines.States
{
    public class PrepareState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private GameBoard _gameBoard;

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard)
        {
            _stateSwitcher = stateSwitcher;
            _gameBoard = gameBoard;
        }

        public void Enter()
        {
            _gameBoard.CreateBoard();
        }

        public void Exit()
        {
            Debug.Log("Game was started");
        }
    }
}
