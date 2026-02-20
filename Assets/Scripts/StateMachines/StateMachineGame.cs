using Assets.Scripts.Game.Board;
using Assets.Scripts.StateMachines.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachines
{
    public class StateMachineGame : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private GameBoard _gameBoard;

        public StateMachineGame(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            _states = new List<IState>()
            {
                new PrepareState(this, _gameBoard)
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void ChangeState<T>() where T : IState
        {
            var state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}
