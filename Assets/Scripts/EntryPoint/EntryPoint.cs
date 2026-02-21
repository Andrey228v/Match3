using Assets.Scripts.Animations;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Levels;
using Assets.Scripts.StateMachines;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;
        
        private StateMachineGame _stateMachineGame;
        private MapGrid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;

        private void Start()
        {
            _stateMachineGame = new StateMachineGame(_gameBoard, _grid, _animation, _matchFinder, _tilePool, _gameProgress, _scoreCalculator);
            _gameProgress.LoadLevelConfig(_gameBoard.LevelConfig.GoalScore, _gameBoard.LevelConfig.Moves);
        }

        [Inject]
        private void Constructor(MapGrid grid, IAnimation animation, 
            MatchFinder matchFinder, TilePool tilePool, GameProgress gameProgress, ScoreCalculator scoreCalculator)
        {
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;
        }

    }
}
