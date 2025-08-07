using DG.Tweening;
using UnityEngine;

namespace Mahjong
{
    public class Hand : MonoBehaviour
    {
        private Board m_board;

        private bool m_dirty;

        private void Awake()
        {
            m_board = GetComponentInParent<Board>();
            m_board.Grip.OnTileAdded.AddListener(OnTileMoved);
            m_board.Grip.OnTileRemoved.AddListener(OnTileMoved);
            m_board.Hand.OnTileAdded.AddListener(OnTileMoved);
            m_board.Hand.OnTileRemoved.AddListener(OnTileMoved);
        }

        private void Update()
        {
            if(!m_dirty) { return; }

            var handWidth = (m_board.Hand.Size - 1) * 0.25f;
            if (!m_board.Grip.Empty)
            {
                handWidth += 0.35f;
            }
            
            for (var i = 0; i < m_board.Hand.Size; i++)
            {
                MoveTile(m_board.Hand.Tiles[i], (i * 0.25f) - (handWidth * 0.5f));
            }
            if (!m_board.Grip.Empty)
            {
                MoveTile(m_board.Grip.Tiles[0], handWidth * 0.5f);
            }
            
            m_dirty = false;
        }

        private void MoveTile(Tile _tile, float _offset)
        {
            _tile.transform.parent = transform;
           var position = transform.position + new Vector3(_offset, 0.0f, 0.0f);
           _tile.transform.DOMove(position, 0.5f).SetEase(Ease.InOutQuart);
           _tile.transform.DORotateQuaternion(transform.rotation, 0.1f);
        }

        private void OnTileMoved(Tile _tile)
        {
            m_dirty = true;
        }
    }
}