using UnityEngine;

namespace Mahjong
{
    public class TilePool : MonoBehaviour
    {
        [SerializeField] private Tiles m_tiles;
        [SerializeField] private Tile m_tilePrefab;
        
        [SerializeField] private int m_tileCount = 4;

        public void Populate(Zone _zone)
        {
            for (var type = TileType.Manzu; type <= TileType.Dragon; type++)
            {
                foreach (var data in m_tiles.GetTilesOfType(type))
                {
                    for (var i = 0; i < m_tileCount; i++)
                    {
                        var tile = Instantiate(m_tilePrefab, transform);
                        tile.gameObject.name = data.Name;
                        tile.Data = data;
                        tile.CurrentZone = _zone;
                    }
                }
            }
        }
    }
}