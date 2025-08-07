using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    public class MapElement : MonoBehaviour
    {
        [ShowInInspector] [ReadOnly] private Vector2Int m_position;

        public Vector2Int Position
        {
            get => m_position;
            set => m_position = value;
        }
    }
}