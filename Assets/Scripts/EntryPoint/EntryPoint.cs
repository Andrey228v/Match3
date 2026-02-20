using Assets.Scripts.Game.Board;
using Assets.Scripts.StateMachines;
using UnityEngine;

namespace Assets.Scripts.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;

        private StateMachineGame _stateMachineGame;

        private void Start()
        {
            _stateMachineGame = new StateMachineGame(_gameBoard);
        }

    }
}
