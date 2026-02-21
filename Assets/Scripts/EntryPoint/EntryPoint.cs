using Assets.Scripts.Animations;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchTiles;
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

        private void Start()
        {
            _stateMachineGame = new StateMachineGame(_gameBoard, _grid, _animation, _matchFinder);
        }

        [Inject]
        private void Constructor(MapGrid grid, IAnimation animation, MatchFinder matchFinder)
        {
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
        }

    }
}
