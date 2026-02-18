using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Tiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public TileConfig TileConfig {  get; private set; }
        public bool IsIntrectable { get; private set; }
        public bool IsMatched { get; private set; }

        public void SetTileConfig(TileConfig tileConfig)
        {
            TileConfig = tileConfig;
            IsIntrectable = tileConfig.IsIntrectable;
            IsMatched = false;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = tileConfig.Sprite;
        }

        public bool SetMatch(bool value) => IsMatched = value;
    }
}
