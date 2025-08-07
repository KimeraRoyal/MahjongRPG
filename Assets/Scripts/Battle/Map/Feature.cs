using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    public class Feature : MonoBehaviour
    {
        private Map m_map;
        
        [ShowInInspector] [ReadOnly] private Vector2Int m_position;
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

        public Vector2Int Position => m_position;

        public UnityEvent<Map> OnMapAssigned;
        
        public UnityEvent<Vector2Int, Vector2Int> OnMove;
        public UnityEvent<Vector2Int> OnMoveInstant;

        public bool MoveTo(Vector2Int _position, bool _instant = false)
        {
            if (!m_map.MoveFeature(this, _position)) { return false; }

            var startPosition = m_position;
            m_position = _position;
            
            if (_instant)
            {
                OnMoveInstant?.Invoke(m_position);
            }
            else
            {
                OnMove?.Invoke(startPosition, m_position);
            }
            return true;
        }
    }
}