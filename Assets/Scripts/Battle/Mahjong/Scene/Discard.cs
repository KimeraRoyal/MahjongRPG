using DG.Tweening;
using UnityEngine;

public class Discard : MonoBehaviour
{
    private Board m_board;

    [SerializeField] private int m_tilesPerRow = 6;
    
    private bool m_dirty;

    private void Awake()
    {
        m_board = GetComponentInParent<Board>();
        m_board.Discard.OnTileAdded.AddListener(OnTileMoved);
        m_board.Hand.OnTileRemoved.AddListener(OnTileMoved);
    }

    private void Update()
    {
        if(!m_dirty) { return; }

        var count = m_board.Discard.Size;
        for (var i = 0; i < count; i++)
        {
            var x = i % m_tilesPerRow;
            var y = i / m_tilesPerRow;
            
            var tile = m_board.Discard.Tiles[i];
            tile.transform.parent = transform;
            var position = transform.position + new Vector3(x * 0.25f - (m_tilesPerRow - 1) * 0.125f, 0.0f, y * -0.3f);
            tile.transform.DOMove(position, 0.5f).SetEase(Ease.InOutQuart);
            tile.transform.DORotateQuaternion(transform.rotation, 0.1f);
        }
            
        m_dirty = false;
    }

    private void OnTileMoved(Tile _tile)
    {
        m_dirty = true;
    }
}
