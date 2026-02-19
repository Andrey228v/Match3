using Assets.Scripts.ResoursesLoading;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Game.Tiles
{
    public class TilePool
    {
        private List<Tile> _tilePool = new List<Tile>();
        private IObjectResolver _objectResolver;
        private GameResoursesLoader _resoursesLoader;
    
        public TilePool(IObjectResolver objectResolver, GameResoursesLoader resoursesLoader)
        {
            _objectResolver = objectResolver;
            _resoursesLoader = resoursesLoader;
        }

        public Tile GetTile(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _tilePool.Count; i++)
            {
                if (_tilePool[i].gameObject.activeInHierarchy) continue;
                _tilePool[i].SetTileConfig(GetRandomTileConfig());
                _tilePool[i].gameObject.transform.position = position;
                return _tilePool[i];
            }

            var tile = CreateTile(position, parent);

            return tile;
        }

        public Tile CreateTile(Vector3 position, Transform parent)
        {
            var tilePrefab = _objectResolver.Instantiate(_resoursesLoader.TilePrefab, position, Quaternion.identity, parent);
            var tile = tilePrefab.GetComponent<Tile>();
            tile.SetTileConfig(GetRandomTileConfig());
            _tilePool.Add(tile);

            return tile;
        }

        private TileConfig GetRandomTileConfig()
        {
            return _resoursesLoader.TileSetConfig.Set[Random.Range(0, _resoursesLoader.TileSetConfig.Set.Count)];
        }

    }
}
