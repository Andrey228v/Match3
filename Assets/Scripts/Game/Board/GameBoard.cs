using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Board
{
    public class GameBoard : MonoBehaviour
    {

        private readonly List<Tile> _tilesToRefill = new List<Tile>();

        private GridSystem.Grid _grid;


    }
}
