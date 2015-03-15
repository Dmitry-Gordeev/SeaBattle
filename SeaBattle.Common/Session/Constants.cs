namespace SeaBattle.Common.Session
{
    public class Constants
    {
        public static readonly int LevelWidth = 960;
        public static readonly int LevelHeigh = 600;

        public const float TEXT_TEXTURE_LAYER = 0f;
    }

    public enum GameMode
    {
        Simple,
        Real
    }

    public enum MapSet
    {
        Empty,
        WithStones
    }

    public enum ShipTypes
    {
        Lugger
    }
}
