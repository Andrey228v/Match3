using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Tiles
{
    public enum TileKind
    {
        Normal,
        Blank,
        Jelly
    }


    [CreateAssetMenu(fileName = "TileConfig", menuName = "Config/TileConfig")]
    public class TileConfig : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private TileKind _tileKind;
        [SerializeField] private bool _isInterectable;

        public Sprite Sprite { get => _sprite; private set => _sprite = value; }
        public TileKind TileKind { get => _tileKind; private set => _tileKind = value; }
        public bool IsIntrectable { get => _isInterectable; set => _isInterectable = value; }


    }
}
