using UnityEngine;

namespace Mahjong
{
    public class TileSelection : MonoBehaviour
    {
        private Board m_board;
        private Tile m_selectedTile;

        private void Awake()
        {
            m_board = GetComponentInParent<Board>();
        }

        public void SelectTile(Tile _tile)
        {
            if(_tile && !_tile.Selectable) { return; }

            if (_tile == m_selectedTile)
            {
                DiscardSelectedTile();
                return;
            }
            
            m_selectedTile?.Deselect();
            m_selectedTile = _tile;
            m_selectedTile?.Select();
            
            DiscardSelectedTile();
        }

        private void DiscardSelectedTile()
        {
            if(!m_selectedTile) { return; }
            
            m_selectedTile.MoveToDiscard();
            SelectTile(null);

            m_board.Draw();
        }
    }
}