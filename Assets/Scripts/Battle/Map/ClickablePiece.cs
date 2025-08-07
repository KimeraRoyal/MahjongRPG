using UnityEngine;

namespace Map
{
    public class ClickablePiece : MonoBehaviour
    {
        private Piece m_piece;
        
        [SerializeField] private Vector2Int m_finalPosition;

        private void Awake()
        {
            m_piece = GetComponentInParent<Piece>();
        }

        public void Move()
        {
            m_piece.MoveTo(m_finalPosition);
        }
    }
}