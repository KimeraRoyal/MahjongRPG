using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    public class Feature : MapElement
    {
        private Map m_map;
        
        [SerializeField] private Vector2Int m_size = Vector2Int.one;
        
        public Map Map
        {
            get => m_map;
            set
            {
                if(m_map) { return; }
                m_map = value;
                OnMapAssigned?.Invoke(m_map);
            }
        }

        public UnityEvent<Map> OnMapAssigned;
        
        public UnityEvent<Vector2Int, Vector2Int> OnMove;
        public UnityEvent<Vector2Int> OnMoveInstant;

        public bool MoveTo(Vector2Int _position, bool _instant = false)
        {
            var startPosition = Position;
            if (!m_map.MoveFeature(this, startPosition, _position)) { return false; }
            Position = _position;
            
            if (_instant)
            {
                OnMoveInstant?.Invoke(Position);
            }
            else
            {
                OnMove?.Invoke(startPosition, Position);
            }
            return true;
        }
    }
}