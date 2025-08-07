using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Tile m_tilePrefab;
        
        [SerializeField] private Vector2Int m_size = new Vector2Int(10, 10);
        [ShowInInspector] [ReadOnly] private Vector2 m_offset;
        private Tile[,] m_tiles;
        private Dictionary<int, Feature> m_features;

        public Vector2Int Size => m_size;
        public Vector2 Offset => m_offset;
        public Vector3 Offset3D => new Vector3(m_offset.x, 0, m_offset.y);

        private void Awake()
        {
            InitialiseMap();
        }

        public bool AddFeature(Feature _feature, Vector2Int _position)
        {
            if(HasFeatureAt(_position)) { return false; }
            _feature.Map = this;
            _feature.transform.parent = transform;
            _feature.MoveTo(_position, true);
            return true;
        }

        public bool MoveFeature(Feature _feature, Vector2Int _startPosition, Vector2Int _position)
        {
            if(HasFeatureAt(_position)) { return false; }
            m_features.Remove(GetTileFeatureIndex(_startPosition));
            m_features.Add(GetTileFeatureIndex(_position), _feature);
            return true;
        }

        private void InitialiseMap()
        {
            m_tiles = new Tile[m_size.x, m_size.y];
            m_features = new Dictionary<int, Feature>();
            m_offset = -(Vector2)(m_size - Vector2Int.one) / 2;

            var position = new Vector2Int(0, 0);
            for (position.y = 0; position.y < m_size.y; position.y++)
            {
                for (position.x = 0; position.x < m_size.x; position.x++)
                {
                    // TODO: Factory method???
                    // I mean to be honest this is all going to be rewritten anyway
                    m_tiles[position.x, position.y] = Instantiate(m_tilePrefab, TileToWorldPosition(position), Quaternion.identity, transform);
                    m_tiles[position.x, position.y].Position = position;
                }
            }
        }

        public Vector3 TileToWorldPosition(Vector2Int _position)
            => new Vector3(_position.x, 0, _position.y) + Offset3D;

        public Tile GetTileAt(Vector2Int _position)
        {
            if (_position.x < 0 || _position.x >= m_size.x || _position.y < 0 || _position.y >= m_size.y) { return null; }
            return m_tiles[_position.x, _position.y];
        }

        public Feature GetFeatureAt(Vector2Int _position)
        {
            if (_position.x < 0 || _position.x >= m_size.x || _position.y < 0 || _position.y >= m_size.y) { return null; }
            m_features.TryGetValue(GetTileFeatureIndex(_position), out var feature);
            return feature;
        }

        public bool HasFeatureAt(Vector2Int _position)
            => GetFeatureAt(_position);

        private int GetTileFeatureIndex(Vector2Int _position)
            => m_size.y * _position.y + _position.x;
    }
}
