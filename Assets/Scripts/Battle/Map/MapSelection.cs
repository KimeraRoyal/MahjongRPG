using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    public class MapSelection : MonoBehaviour
    {
        private Map m_map;

        [SerializeField] private Tile m_selectedTile;
        [SerializeField] private Feature m_selectedFeature;

        public UnityEvent<Tile> OnTileSelected;
        public UnityEvent<Feature> OnFeatureSelected;

        private void Awake()
        {
            m_map = GetComponentInParent<Map>();
        }

        public void SelectTile(Vector2Int _position)
        {
            var tile = m_map.GetTileAt(_position);
            tile = m_selectedTile == tile ? null : tile;

            if(!tile) { return; }
            
            var featureAtTile = m_map.GetFeatureAt(_position);
            if (m_selectedFeature && m_selectedTile && m_selectedFeature.MoveTo(_position))
            { 
                m_selectedTile = null;
                featureAtTile = null;
            }

            if (tile != m_selectedTile)
            {
                m_selectedTile = tile;
                OnTileSelected?.Invoke(m_selectedTile);
            }

            if (featureAtTile != m_selectedFeature)
            {
                m_selectedFeature = featureAtTile;
                OnFeatureSelected?.Invoke(m_selectedFeature);
            }
        }
    }
}