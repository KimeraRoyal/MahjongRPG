namespace Mahjong
{
    public class Set
    {
        private SetType m_type;
        
        private TileType m_suit;
        private Tile[] m_tiles;

        public SetType Type => m_type;
        
        public TileType Suit => m_suit;
        public Tile[] Tiles => m_tiles;
        
        public Set(SetType _type, TileType _suit, Tile[] _tiles)
        {
            m_type = _type;

            m_suit = _suit;
            m_tiles = _tiles;
        }
    }
}