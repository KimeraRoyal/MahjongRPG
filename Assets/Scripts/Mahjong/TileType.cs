namespace Mahjong
{
    public enum TileType
    {
        Manzu,
        Souzu,
        Pinzu,
        Wind,
        Dragon
    }

    public static class TileTypeExtensions
    {
        public static bool IsSuited(this TileType _type)
            => _type is TileType.Manzu or TileType.Souzu or TileType.Pinzu;

        public static bool IsHonor(this TileType _type)
            => _type is TileType.Wind or TileType.Dragon;
    }
}