using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Tiles
{
    public class BlankTileSetup : MonoBehaviour
    {
        [SerializeField] private List<BlankTile> _blankTilesLayout;

        public bool[,] Blanks { get; private set; }

        public void SetupBlanks(int width, int height)
        {
            Blanks = new bool[width, height];

            for (int i = 0; i < _blankTilesLayout.Count; i++) 
            {
                Blanks[_blankTilesLayout[i].XPosition, _blankTilesLayout[i].YPosition] = true;
            }
        }
    }
}
