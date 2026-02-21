using Assets.Scripts.Animations;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.StateMachines.States;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


namespace Assets.Scripts.StateMachines
{
    public class StateMachineGame : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private GameBoard _gameBoard;
        private MapGrid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;

        public StateMachineGame(GameBoard gameBoard, MapGrid grid, 
            IAnimation animation, MatchFinder matchFinder, TilePool tilePool,
            GameProgress gameProgress, ScoreCalculator scoreCalculator)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;

            _states = new List<IState>()
            {
                new PrepareState(this, _gameBoard),
                new PlayerTurnState(_grid, this, _animation),
                new SwapTilesState(_grid, this, _animation, _matchFinder, _gameProgress),
                new RemoveTileState(_grid, this, _animation, _matchFinder, _scoreCalculator),
                new RefillGridState(_grid, this, _animation, _matchFinder, _tilePool, _gameBoard.transform, _gameProgress),
                new WinState(),
                new LooseState(),
            };

            _currentState = _states[0];
            _currentState.Enter();
            _scoreCalculator = scoreCalculator;
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
