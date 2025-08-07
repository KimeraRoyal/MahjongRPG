using System;
using UnityEngine;

namespace Mahjong
{
    [CreateAssetMenu(fileName = "Tiles", menuName = "Mahjong/Tiles")]
    public class Tiles : ScriptableObject
    {
        public const int c_tileCount = 9 * 3 + 7;
        public static int[] c_tileTypeCounts = { 9, 9, 9, 4, 3 };
        public static int[] c_tileTypeOffsets = { 0, 9, 18, 27, 31 };
        
        [SerializeField] private TileData[] m_tiles;

        public TileData[] All => m_tiles;

        public TileData[] GetTilesOfType(TileType _type)
        {
            var index = (int)_type;

            var min = c_tileTypeOffsets[index];
            var max = min + c_tileTypeCounts[index];
            return m_tiles[min..max];
        }
        
        private void InitialiseTiles()
        {
            var id = 0;
            for (var type = TileType.Manzu; type <= TileType.Dragon; type++)
            {
                var index = (int)type;
                var count = type switch
                {
                    TileType.Manzu or TileType.Souzu or TileType.Pinzu => 9,
                    TileType.Wind => 4,
                    TileType.Dragon => 3,
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                for (var value = 0; value < count; value++)
                {
                    m_tiles[id] = new TileData(id, type, value);
                    id++;
                }
            }
        }

        private void OnValidate()
        {
            if(m_tiles.Length == c_tileCount) { return; }

            var previousTiles = m_tiles;
            
            m_tiles = new TileData[c_tileCount];
            InitialiseTiles();

            if (previousTiles.Length < 1) { return; }
            
            for (var i = 0; i < previousTiles.Length && i < m_tiles.Length; i++)
            {
                m_tiles[i] = previousTiles[i];
                m_tiles[i].ID = i;
            }
        }
    }
}