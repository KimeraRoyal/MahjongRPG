using System;
using UnityEngine;

namespace Mahjong
{
    [CreateAssetMenu(fileName = "Tiles", menuName = "Mahjong/Tiles")]
    public class Tiles : ScriptableObject
    {
        public const int c_tileCount = 9 * 3 + 7;
        
        [SerializeField] private Tile[] m_tiles;

        public void OnValidate()
        {
            if(m_tiles.Length == c_tileCount) { return; }

            m_tiles = new Tile[c_tileCount];
            
            var id = 0;
            for (var type = TileType.Manzu; type <= TileType.Dragon; type++)
            {
                var count = type switch
                {
                    TileType.Manzu or TileType.Souzu or TileType.Pinzu => 9,
                    TileType.Wind => 4,
                    TileType.Dragon => 3,
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                for (var value = 0; value < count; value++)
                {
                    m_tiles[id] = new Tile(id, type, value);
                    id++;
                }
            }
        }
    }
}