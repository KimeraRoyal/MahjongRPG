using System;
using UnityEngine;

namespace Mahjong
{
    [Serializable]
    public class Tile
    {
        private int m_id;

        [SerializeField] private string m_name;
        [SerializeField] private TileType m_type;
        [SerializeField] private int m_value;

        public int ID => m_id;

        public string Name => m_name;
        public TileType Type => m_type;
        public int Value => m_value;
        
        public Tile(int _id, TileType _type, int _value)
        {
            m_id = _id;

            m_name = $"{_type} {_value}";
            m_type = _type;
            m_value = _value;
        }

        public bool IsTerminal()
            => m_type.IsSuited() && m_value is 1 or 9;
    }
}
