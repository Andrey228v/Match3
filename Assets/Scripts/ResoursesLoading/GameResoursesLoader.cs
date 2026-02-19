using Assets.Scripts.Game.Tiles;
using UnityEngine;

namespace Assets.Scripts.ResoursesLoading
{
    public class GameResoursesLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private TileSetConfig _tileSetConfig;

        public GameObject TilePrefab => _tilePrefab;
        public TileSetConfig TileSetConfig => _tileSetConfig;


    }
}
