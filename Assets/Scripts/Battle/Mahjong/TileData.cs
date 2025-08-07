using System;
using UnityEngine;

namespace Mahjong
{
    [Serializable]
    public class TileData
    {
        [SerializeField] private int m_id;

        [SerializeField] private string m_name;
        [SerializeField] private TileType m_type;
        [SerializeField] private int m_value;

        public int ID
        {
            get => m_id;
            set
            {
#if UNITY_EDITOR
                m_id = value;
#endif
            }
        }

        public string Name => m_name;
        public TileType Type => m_type;
        public int Value => m_value;
        
        public TileData(int _id, TileType _type, int _value)
        {
            m_id = _id;

            m_name = $"{_type} {_value}";
            m_type = _type;
            m_value = _value;
        }

        public bool IsTerminal()
            => m_type.IsSuited() && m_value is 0 or 8;

        public string GetValueString()
            => m_type switch
            {
                TileType.Manzu or TileType.Souzu or TileType.Pinzu => GetSuitedValueString(),
                TileType.Wind => GetWind(),
                TileType.Dragon => GetDragon(),
                _ => throw new ArgumentOutOfRangeException()
            };

        private string GetSuitedValueString()
            => m_value switch
            {
                0 => "One",
                1 => "Two",
                2 => "Three",
                3 => "Four",
                4 => "Five",
                5 => "Six",
                6 => "Seven",
                7 => "Eight",
                8 => "Nine",
                _ => "INVALID"
            };

        private string GetWind()
            => m_value switch
            {
                0 => "Ton",
                1 => "Nan",
                2 => "Shaa",
                3 => "Pei",
                _ => "INVALID"
            };

        private string GetDragon()
            => m_value switch
            {
                0 => "Haku",
                1 => "Hatsu",
                2 => "Chun",
                _ => "INVALID"
            };
    }
}
