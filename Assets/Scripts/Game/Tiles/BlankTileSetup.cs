using Assets.Scripts.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Tiles
{
    public class BlankTileSetup
    {
        //[SerializeField] private List<BlankTile> _blankTilesLayout;

        public bool[,] Blanks { get; private set; }

        public void SetupBlanks(LevelConfig levelConfig)
        {
            Blanks = new bool[levelConfig.Width, levelConfig.Height];

            for (int i = 0; i < levelConfig.BlankTiles.Count; i++) 
            {
                Blanks[levelConfig.BlankTiles[i].XPosition, levelConfig.BlankTiles[i].YPosition] = true;
            }
        }
    }
}
