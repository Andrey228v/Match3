using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game.GridSystem
{
    public class GameDebug
    {
        private MapGrid _grid;

        public GameDebug(MapGrid grid) 
        {
            _grid = grid;
        }

        public void ShowDebug(Transform parent)
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++) 
                {
                    var text = x + "," + y;
                    CreateDebugText(parent, text, _grid.GridToWorld(x, y));
                }
            }
        }

        private void CreateDebugText(Transform parent, string text, Vector3 position)
        {
            var debugText = new GameObject("DebugText", typeof(TextMeshPro));
            debugText.transform.SetParent(parent);
            debugText.transform.position = position + new Vector3(0, 0 , -1f);
            debugText.transform.forward = Vector3.forward;

            var TMP = debugText.GetComponent<TextMeshPro>();
            TMP.text = text;
            TMP.fontSize = 3f;
            TMP.color = Color.white;
            TMP.alignment = TextAlignmentOptions.Center;
        }
    }
}
