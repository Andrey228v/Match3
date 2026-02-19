using System;
using UnityEngine;

namespace Assets.Scripts.Game.Tiles
{
    [Serializable]
    public class BlankTile
    {
        [SerializeField] private int _xPosition;
        [SerializeField] private int _yPosition;

        public int XPosition => _xPosition;
        public int YPosition => _yPosition;


    }
}
