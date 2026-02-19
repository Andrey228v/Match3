using Assets.Scripts.Game.Tiles;
using UnityEngine;

namespace Assets.Scripts.ResoursesLoading
{
    public class GameResoursesLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private GameObject _blankPrefab;
        [SerializeField] private TileConfig _blankConfig;
        [SerializeField] private TileSetConfig _tileSetConfig;

        public GameObject TilePrefab => _tilePrefab;
        public TileSetConfig TileSetConfig => _tileSetConfig;
        
        public GameObject BlankPrefab => _blankPrefab;

        public TileConfig BlankConfig => _blankConfig;

    }
}
